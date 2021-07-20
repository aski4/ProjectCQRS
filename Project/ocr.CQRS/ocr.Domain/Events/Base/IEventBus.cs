using System.Threading.Tasks;

namespace ocr.Domain.Events.Base
{
    public interface IEventBus
    {
        Task Publish<TEvent>(params TEvent[] events) where TEvent : IEvent;
    }
}
