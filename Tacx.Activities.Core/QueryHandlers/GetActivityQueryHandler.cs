using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Core.Queries;

namespace Tacx.Activities.Core.QueryHandlers
{
    public class GetActivityQueryHandler : IRequestHandler<GetActivityQuery, ActivityDto?>
    {
        private readonly IRepository<Activity> _repository;

        public GetActivityQueryHandler(IRepository<Activity> repository)
        {
            _repository = repository;
        }

        public async Task<ActivityDto?> Handle(GetActivityQuery request, CancellationToken cancellationToken)
        {
            var activity = await _repository.GetByIdAsync(request.Id);
            
            return activity == null 
                ? null : 
                ToDto(activity);
        }

        private static ActivityDto ToDto(Activity activity)
            => new()
            {
                Id = activity.Id,
                Name = activity.Name,
                Description = activity.Description,
                Duration = activity.Duration,
                Distance = activity.Distance,
                AvgSpeed = activity.AvgSpeed,
            };
    }
}
