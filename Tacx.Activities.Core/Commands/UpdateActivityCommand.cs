using MediatR;
using Tacx.Activities.Core.Dtos;

namespace Tacx.Activities.Core.Commands
{
    public record UpdateActivityCommand(ActivityDto Activity) : IRequest<bool>;
}
    