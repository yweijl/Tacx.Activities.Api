using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.Repositories
{
    public class ActivitiesRepository : RepositoryBase<Activity>
    {
        private readonly IBlobContainer _blobContainer;

        public ActivitiesRepository(ICosmosDbContainer<Activity> container, IBlobContainer blobContainer) : base(container)
        {
            _blobContainer = blobContainer;
        }

        public override async Task<bool> CreateAsync(Activity activity)
        {
            var created = await base.CreateAsync(activity);
            
            if (created)
            {
                //var data = JsonSerializer.SerializeToUtf8Bytes(activity);
                //await using var stream = new MemoryStream(data);
                await _blobContainer.AddAsync(activity.Id, activity);
            }

            return created;
        }
    }
}
