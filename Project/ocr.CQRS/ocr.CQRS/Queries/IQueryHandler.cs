using MediatR;
using ocr.Domain;
using Optional;

namespace ocr.CQRS.Queries
{
    public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Option<TResponse, Error>>
           where TQuery : IQuery<TResponse>
    {
    }
}
    