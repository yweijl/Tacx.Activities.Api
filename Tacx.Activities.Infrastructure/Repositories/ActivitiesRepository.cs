using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.Repositories
{
    public class ActivitiesRepository : RepositoryBase<Activity>
    {
        private readonly IStorageService _storageService;
        public ActivitiesRepository(ICosmosDbContainer<Activity> container, IStorageService storageService) : base(container)
        {
            _storageService = storageService;
        }

        public override async Task<bool> CreateAsync(Activity activity)
        {
            await _storageService.PersistActivityAsync(activity);
            return await base.CreateAsync(activity);
        }
    }
}
