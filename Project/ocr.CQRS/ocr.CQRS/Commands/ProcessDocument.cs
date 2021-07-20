using ocr.Domain;

namespace ocr.CQRS.Commands
{
    public class ProcessDocument : TabCommand
    {
        public Document Document { get; set; }
    }
}
