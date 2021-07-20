using Marten.Events.Projections;
using ocr.Domain.Events;
using System;

namespace ocr.Domain.View
{
    public class TabViewProjection : ViewProjection<TabView, Guid>
    {
        public TabViewProjection()
        {
            ProjectEvent<TabOpened>((ev) => ev.Id, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<TabClosed>((ev) => ev.Id, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<DocumentToProcess>((ev) => ev.TabId, (view, @event) => view.ApplyEvent(@event));
            ProjectEvent<DocumentProcessed>((ev) => ev.TabId, (view, @event) => view.ApplyEvent(@event));
        }
    }
}
