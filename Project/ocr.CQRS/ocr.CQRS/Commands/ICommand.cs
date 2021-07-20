using MediatR;
using ocr.Domain;
using Optional;

namespace ocr.CQRS.Commands
{
    public interface ICommand : IRequest<Option<Unit, Error>>
    {
    }
}
