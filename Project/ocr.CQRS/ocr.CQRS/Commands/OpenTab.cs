using System;
using System.Collections.Generic;
using System.Text;

namespace ocr.CQRS.Commands
{
    public class OpenTab : TabCommand
    {
        public string InitiatorName { get; set; }
    }
}
