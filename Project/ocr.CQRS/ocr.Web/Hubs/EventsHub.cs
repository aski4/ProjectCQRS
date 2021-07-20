using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ocr.Web.Hubs
{
    public class EventsHub : Hub
    {
        public Task NewEventRegistered(object @event) =>
            Clients.All.SendAsync(nameof(NewEventRegistered), @event);
    }
}
