using MediatR;
using Microsoft.AspNetCore.SignalR;
using ocr.Domain.Events.Base;
using ocr.Web.Hubs;
using System.Threading.Tasks;

namespace ocr.Web.Events
{
    public class EventBus : IEventBus
    {
        private readonly IHubContext<EventsHub> _eventsHub;
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator, IHubContext<EventsHub> eventsHub)
        {
            _mediator = mediator;
            _eventsHub = eventsHub;
        }

        public async Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
                await _eventsHub.Clients.All.SendAsync(nameof(EventsHub.NewEventRegistered), new { Name = @event.GetType().Name, Data = @event });
            }
        }
    }
}
