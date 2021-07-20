using System;
using System.Collections.Generic;
using System.Text;

namespace ocr.CQRS.Commands
{
    public class TabCommand : ICommand
    {
        public Guid TabId { get; set; }
    }
}
