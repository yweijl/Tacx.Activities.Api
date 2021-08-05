using MediatR;
using Tacx.Activities.Core.Dtos;

namespace Tacx.Activities.Core.Commands
{
    //TODO test if record works
    public class CreateActivityCommand : IRequest<ActivityDto>
    {
        public readonly ActivityDto Activity;

        public CreateActivityCommand(ActivityDto activity)
        {
            Activity = activity;
        }
    }
}
