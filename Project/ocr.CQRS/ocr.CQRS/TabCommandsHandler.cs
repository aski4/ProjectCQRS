using Marten;
using MediatR;
using ocr.CQRS.Commands;
using ocr.Data;
using ocr.Domain;
using ocr.Domain.Events.Base;
using ocr.Domain.ValidationErrors;
using Optional;
using Optional.Async;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ocr.CQRS
{
    public class TabCommandsHandler :
        ICommandHandler<OpenTab>,
        ICommandHandler<CloseTab>,
        ICommandHandler<PostDocument>,
        ICommandHandler<ProcessDocument>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IEventBus _eventBus;
        private readonly IDocumentSession _session;

        public TabCommandsHandler(
            IDocumentSession session,
            IEventBus eventBus,
            ApplicationDbContext dbContext)
        {
            _session = session;
            _eventBus = eventBus;
            _dbContext = dbContext;
        }

        public Task<Option<Unit, Error>> Handle(OpenTab request, CancellationToken cancellationToken) =>
            ValidateCommandIsNotEmpty(request)
            .Filter(r => !string.IsNullOrEmpty(r.InitiatorName), ValidationErrors.Tab.InvalidInitiatorName).FlatMapAsync(command =>
            TabShouldNotExist(command.TabId, cancellationToken).MapAsync(tab =>
            PublishEvents(tab.Id, tab.OpenTab(command.InitiatorName))));


        public Task<Option<Unit, Error>> Handle(CloseTab request, CancellationToken cancellationToken) =>
            ValidateCommandIsNotEmpty(request).FlatMapAsync(command =>
            GetTabIfNotClosed(command.TabId, cancellationToken)
            .FilterAsync(async tab => string.IsNullOrEmpty(command.FinalText), ValidationErrors.Tab.EmptyFinalText)
            .MapAsync(tab =>
            PublishEvents(tab.Id, tab.CloseTab(tab.Document.Text))));

        public Task<Option<Unit, Error>> Handle(PostDocument request, CancellationToken cancellationToken) =>
            ValidateCommandIsNotEmpty(request).FlatMapAsync(command =>
            GetTabIfNotClosed(request.TabId, cancellationToken).FlatMapAsync(tab =>
            ValidateDocument(command.Document, cancellationToken)
            .MapAsync(document =>
            PublishEvents(tab.Id, tab.PostDocument(document)))));


        public Task<Option<Unit, Error>> Handle(ProcessDocument request, CancellationToken cancellationToken) =>
            ValidateCommandIsNotEmpty(request).FlatMapAsync(command =>
            GetTabIfNotClosed(command.TabId, cancellationToken).FlatMapAsync(tab =>
            ValidateDocument(command.Document, cancellationToken)
            .MapAsync(document =>
            PublishEvents(tab.Id, tab.ProcessDocument()))));

        private async Task<Unit> PublishEvents(Guid tabId, params IEvent[] events)
        {
            _session.Events.Append(tabId, events);
            await _session.SaveChangesAsync();
            await _eventBus.Publish(events);

            return Unit.Value;
        }

        private Task<Tab> GetTabFromStore(Guid id, CancellationToken cancellationToken) =>
            _session.LoadAsync<Tab>(id, cancellationToken);

        private Task<Option<Tab, Error>> GetTabIfExists(Guid id, CancellationToken cancellationToken) =>
            GetTabFromStore(id, cancellationToken)
                .SomeNotNull<Tab, Error>(ValidationErrors.Tab.NotFound(id));

        private Task<Option<Tab, Error>> TabShouldNotExist(Guid id, CancellationToken cancellationToken) =>
            GetTabFromStore(id, cancellationToken)
            .SomeWhen<Tab, Error>(t => t == null, ValidationErrors.Tab.AlreadyExists(id))
            .MapAsync(async _ => new Tab(id));

        private static Option<TCommand, Error> ValidateCommandIsNotEmpty<TCommand>(TCommand command) where TCommand : TabCommand =>
            command
                .SomeNotNull<TCommand, Error>(ValidationErrors.Generic.NullCommand)
                .Filter(r => r.TabId != Guid.Empty, ValidationErrors.Tab.InvalidId);

        private Task<Option<Tab, Error>> GetTabIfNotClosed(Guid id, CancellationToken cancellationToken) =>
            GetTabIfExists(id, cancellationToken)
                .FilterAsync(async tab => tab.IsOpen, ValidationErrors.Tab.NotOpen(id));

        private async Task<Option<Document, Error>> ValidateDocument(Document document, CancellationToken cancellationToken)
        {
            return document?.fileBites?.Length == 0 ?
                Option.None<Document, Error>(ValidationErrors.Document.CorruptedFile) :
                Option.Some<Document, Error>(document);
        }
            

    }
}
