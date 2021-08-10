using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;
using Tacx.Activities.Infrastructure.Strava;

namespace Tacx.Activities.Infrastructure.Repositories
{
    public class ActivitiesRepository : RepositoryBase<Activity>
    {
        private readonly IBlobContainer _blobContainer;
        private readonly IStravaApi _stravaApi;

        public ActivitiesRepository(ICosmosDbContainer<Activity> container, IBlobContainer blobContainer, IStravaApi stravaApi) : base(container)
        {
            _blobContainer = blobContainer;
            _stravaApi = stravaApi;
        }

        public override async Task<bool> CreateAsync(Activity activity)
        {
            var created = await base.CreateAsync(activity);
            
            if (created)
            {
                await _blobContainer.AddAsync(activity);
                await _stravaApi.PostAsync(activity);
            }

            return created;
        }
    }
}
