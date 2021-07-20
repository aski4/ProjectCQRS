using MediatR;
using ocr.Domain;
using Optional;

namespace ocr.CQRS.Commands
{
    public interface ICommandHandler<in TCommand> :
        IRequestHandler<TCommand, Option<Unit, Error>>
        where TCommand : IRequest<Option<Unit, Error>>
    {
    }
}
