using ocr.Domain.View;
using System.Collections.Generic;

namespace ocr.CQRS.Queries
{
    public class GetSavedDocuments : IQuery<IEnumerable<DocumentView>>
    {
    }
}
