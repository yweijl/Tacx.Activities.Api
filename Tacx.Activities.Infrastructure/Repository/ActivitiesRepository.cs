using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.Repository
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private readonly ICosmosDbContainer<Activity> _container;

        public ActivitiesRepository(ICosmosDbContainer<Activity> container)
        {
            _container = container;
        }

        public Task<bool> CreateAsync(Activity activity)
        {
            return _container.CreateAsync(activity);
        }

        public Task<Activity?> GetByIdAsync(string id)
        {
            return _container.ReadItemAsync(id);
        }
    }
}
