using System;
using System.Collections.Generic;
using System.Text;

namespace ocr.CQRS.Commands
{
    public class CloseTab : TabCommand
    {
        public string FinalText { get; set; }
    }
}
