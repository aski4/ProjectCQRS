
using ocr.Domain.Events.Base;
using System;
using System.Collections.Generic;

namespace ocr.Domain.Base
{
    public class Aggregate 
    {
        public Guid Id { get; protected set; }

        public Queue<IEvent> PendingEvents { get; private set; } = new Queue<IEvent>();
    }
}
