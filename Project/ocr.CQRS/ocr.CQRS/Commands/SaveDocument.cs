using ocr.Domain;

namespace ocr.CQRS.Commands
{
    public class SaveDocument : ICommand
    {
        public Document Document { get; set; }
    }
}
