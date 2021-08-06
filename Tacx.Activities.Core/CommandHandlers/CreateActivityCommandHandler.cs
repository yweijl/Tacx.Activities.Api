using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;

namespace Tacx.Activities.Core.CommandHandlers
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, ActivityDto>
    {
        private readonly IStorageService _storageService;
        private readonly IActivitiesRepository _activitiesRepository;

        public CreateActivityCommandHandler(IActivitiesRepository activitiesRepository, IStorageService storageService)
        {
            _activitiesRepository = activitiesRepository;
            _storageService = storageService;
        }

        public async Task<ActivityDto> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = ToEntity(request.Activity);

            await _activitiesRepository.CreateAsync(activity);
            await _storageService.PersistActivityAsync(activity);

            return request.Activity;
        }

        private static Activity ToEntity(ActivityDto dto)
            => new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                Duration = dto.Duration,
                Distance = dto.Distance,
                AvgSpeed = dto.AvgSpeed,
            };
    }
}
