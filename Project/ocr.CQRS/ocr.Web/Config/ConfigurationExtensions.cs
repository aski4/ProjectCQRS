using Marten;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ocr.CQRS;
using ocr.CQRS.Commands;
using ocr.CQRS.Queries;
using ocr.Domain;
using ocr.Domain.Events;
using ocr.Domain.Events.Base;
using ocr.Domain.View;
using ocr.Web.Events;
using Optional;

namespace ocr.Web.Config
{
    public static class ConfigurationExtensions
    {
        public static void AddCqrs(this IServiceCollection services)
        {
            services.AddScoped<IEventBus, EventBus>();

            services.AddScoped<IRequestHandler<OpenTab, Option<Unit, Error>>, TabCommandsHandler>();
            services.AddScoped<IRequestHandler<PostDocument, Option<Unit, Error>>, TabCommandsHandler>();
            services.AddScoped<IRequestHandler<ProcessDocument, Option<Unit, Error>>, TabCommandsHandler>();
            services.AddScoped<IRequestHandler<CloseTab, Option<Unit, Error>>, TabCommandsHandler>();

            services.AddScoped<IRequestHandler<GetTabView, Option<TabView, Error>>, TabQueriesHandler>();
        }

        public static void AddMarten(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(_ =>
            {
                var documentStore = DocumentStore.For(options =>
                {
                    var config = configuration.GetSection("EventStore");
                    var connectionString = config.GetValue<string>("ConnectionString");
                    var schemaName = config.GetValue<string>("Schema");

                    options.Connection(connectionString);
                    options.AutoCreateSchemaObjects = AutoCreate.All;
                    options.Events.DatabaseSchemaName = schemaName;
                    options.DatabaseSchemaName = schemaName;

                    options.Events.InlineProjections.AggregateStreamsWith<Tab>();
                    options.Events.InlineProjections.Add(new TabViewProjection());

                    options.Events.AddEventType(typeof(TabOpened));
                    options.Events.AddEventType(typeof(TabClosed));
                    options.Events.AddEventType(typeof(DocumentProcessed));
                    options.Events.AddEventType(typeof(DocumentToProcess));
                });

                return documentStore.OpenSession();
            });
        }
    }
}
