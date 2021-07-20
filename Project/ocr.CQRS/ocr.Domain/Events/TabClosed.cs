using ocr.Domain.Events.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace ocr.Domain.Events
{
    public class TabClosed : IEvent
    {
        public Guid Id { get; set; }
        public string FinalText { get; set; }
    }
}
