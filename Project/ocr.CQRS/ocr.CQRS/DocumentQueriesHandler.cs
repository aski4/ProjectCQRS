using Microsoft.EntityFrameworkCore;
using ocr.CQRS.Queries;
using ocr.Data;
using ocr.Domain;
using ocr.Domain.ValidationErrors;
using ocr.Domain.View;
using Optional;
using Optional.Async;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ocr.CQRS
{
    public class DocumentQueriesHandler :
        IQueryHandler<GetSavedDocuments, IEnumerable<DocumentView>>
    {
        private ApplicationDbContext _dbContext;

        public DocumentQueriesHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Option<IEnumerable<DocumentView>, Error>> Handle(GetSavedDocuments request, CancellationToken cancellationToken) =>
             request.
                SomeNotNull<GetSavedDocuments, Error>(ValidationErrors.Generic.NullQuery)
                .MapAsync(async _ =>
                {
                    var documents = await _dbContext
                        .Documents
                        .ToListAsync(cancellationToken);

                    return documents
                            .Select(d => new DocumentView
                            {
                                Name = d.Name,
                                Text = d.Text
                            });
                });
    }
}
