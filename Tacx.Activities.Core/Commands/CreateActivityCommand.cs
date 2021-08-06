using MediatR;
using Tacx.Activities.Core.Dtos;

namespace Tacx.Activities.Core.Commands
{
    public record CreateActivityCommand(ActivityDto Activity) : IRequest<ActivityDto>;
}
