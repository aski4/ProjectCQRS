using ocr.Domain;

namespace ocr.CQRS.Commands
{
    public class PostDocument : TabCommand
    {
        public Document Document { get; set; }
    }
}
