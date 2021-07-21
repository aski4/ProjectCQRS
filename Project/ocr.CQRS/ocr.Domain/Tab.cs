using ocr.Domain.Base;
using ocr.Domain.Events;
using OCR;
using System;

namespace ocr.Domain
{
    public class Tab : IAggregate
    {
        public Tab() {}
        public Tab(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public string InitiatorName { get; set; }
        public Document Document { get; set; }
        public bool IsOpen { get; set; }


        public void Apply(TabOpened @event)
        {
            IsOpen = true;
            InitiatorName = @event.InitiatorName;
        }
        
        public void Apply(TabClosed @event)
        {
            IsOpen = false;
        }

        public void Apply(DocumentToProcess @event)
        {
            Document = @event.Document;
        }

        public void Apply(DocumentProcessed @event)
        {
            var ocr = new CoreOCR(LanguageEnum.Russian);
            var result = ocr.GetTextFromBytes(@event.Document.fileBites);

            Document.Text = result.Text;   
        }

        public DocumentToProcess PostDocument(Document document) =>
            new DocumentToProcess
            {
                TabId = Id,
                Document = document
            };

        public DocumentProcessed ProcessDocument() =>
            new DocumentProcessed
            {
                TabId = Id,
                Document = Document
            };

        public TabOpened OpenTab(string initiatorName) =>
            new TabOpened
            {
                Id = Id,
                InitiatorName = initiatorName
            };

        public TabClosed CloseTab(string finalText) =>
            new TabClosed
            {
                Id = Id,
                FinalText = finalText
            };
    }
}
