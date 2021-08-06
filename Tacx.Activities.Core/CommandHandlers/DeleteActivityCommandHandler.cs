using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;

namespace Tacx.Activities.Core.CommandHandlers
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, bool>
    {
        private readonly IRepository<Activity> _repository;

        public DeleteActivityCommandHandler(IRepository<Activity> repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            return _repository.DeleteAsync(request.Id);
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
