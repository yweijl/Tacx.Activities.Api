using Microsoft.Azure.Cosmos;
using System;
using System.Net;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Enums;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.Repository
{
    public class ActivitiesRepository : IActivitiesRepository
    {
        private static CosmosDbContainerType ContainerType => CosmosDbContainerType.Activity;
        private readonly Container _container;

        public ActivitiesRepository(ICosmosDbContainerFactory factory)
        {
            _container = factory.GetContainer(ContainerType).Container;
        }

        public async Task<bool> InsertAsync(Activity activity)
        {
            try
            {
                await _container.CreateItemAsync(activity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }

        public async Task<Activity?> GetByIdAsync(long id)
        {
            try
            {
                var activity = await _container.ReadItemAsync<Activity>(id.ToString(), new PartitionKey(id.ToString()));
                return activity;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
