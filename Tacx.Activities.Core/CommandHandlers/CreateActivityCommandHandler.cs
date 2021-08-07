using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Core.Mappers;

namespace Tacx.Activities.Core.CommandHandlers
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
    {
        private readonly IRepository<Activity> _repository;

        public CreateActivityCommandHandler(IRepository<Activity> repository)
        {
            _repository = repository;
        }

        public async Task<ActivityDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = request.Activity.ToEntity();

            await _repository.CreateAsync(activity);

            return request.Activity;
        }
    }
}
