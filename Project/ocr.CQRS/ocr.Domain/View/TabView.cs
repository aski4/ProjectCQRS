using ocr.Domain.Events;
using OCR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ocr.Domain.View
{
    public class TabView
    {
        public Guid Id { get; set; }
        public string InitiatorName { get; set; }

        public bool IsOpen { get; set; }
        public string FinalText { get; set; }
        public Document Document { get; set; }


        public void ApplyEvent(TabOpened @event)
        {
            IsOpen = true;
            InitiatorName = @event.InitiatorName;
        }

        public void ApplyEvent(TabClosed @event)
        {
            IsOpen = false;
            FinalText = @event.FinalText;
        }

        public void ApplyEvent(DocumentProcessed @event)
        {
            var ocr = new CoreOCR(LanguageEnum.Russian);
            var result = ocr.GetTextFromBytes(@event.Document.fileBites);

            Document.Text = result.Text;
        }

        public void ApplyEvent(DocumentToProcess @event)
        {
            Document = @event.Document;
        }
    }
}
