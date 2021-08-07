using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Core.Mappers;
using Tacx.Activities.Core.Queries;

namespace Tacx.Activities.Core.CommandHandlers
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand, bool>
    {
        private readonly IRepository<Activity> _repository;

        public UpdateActivityCommandHandler(IRepository<Activity> repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
           return _repository.UpsertAsync(request.Activity.ToEntity());
        }
    }
}
