using MediatR;
using Microsoft.EntityFrameworkCore;
using ocr.CQRS.Commands;
using ocr.Data;
using ocr.Domain;
using ocr.Domain.ValidationErrors;
using ocr.Domain.View;
using Optional;
using Optional.Async;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ocr.CQRS
{
    public class DocumentCommandsHandler :
        ICommandHandler<SaveDocument>
    {
        private readonly ApplicationDbContext _dbContext;

        public DocumentCommandsHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Option<Unit, Error>> Handle(SaveDocument request, CancellationToken cancellationToken)
        {
            return ValidateRequest().FlatMapAsync(command => PersistDocument(command));


            Option<SaveDocument, Error> ValidateRequest() =>
                request.SomeWhen<SaveDocument, Error>(
                    r => r.Document != null,
                    ValidationErrors.Document.MustAddAtLeastOneDocument);
        }

        private async Task<Option<Unit, Error>> PersistDocument(SaveDocument request)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                Name = request.Document.Name,
                Text = request.Document.Text
            };

            await _dbContext.Documents.AddAsync(document);
            await _dbContext.SaveChangesAsync();

            return Unit.Value.Some<Unit, Error>();
        }
    }
}
