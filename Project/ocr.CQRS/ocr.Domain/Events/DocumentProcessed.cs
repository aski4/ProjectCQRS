using ocr.Domain.Events.Base;
using System;

namespace ocr.Domain.Events
{
    public class DocumentProcessed : IEvent
    {
        public Guid TabId { get; set; }

        public Document Document { get; set; }
    }
}
