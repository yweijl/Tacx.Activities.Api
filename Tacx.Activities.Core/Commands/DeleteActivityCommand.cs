using MediatR;

namespace Tacx.Activities.Core.Commands
{
    public record DeleteActivityCommand(string Id) : IRequest<bool>;
}
