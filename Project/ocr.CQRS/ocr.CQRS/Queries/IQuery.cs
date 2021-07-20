using MediatR;
using ocr.Domain;
using Optional;

namespace ocr.CQRS.Queries
{
    public interface IQuery<TResponse> : IRequest<Option<TResponse, Error>>
    {
    }
}
