using Microsoft.Azure.Cosmos;
using Tacx.Activities.Core.Enums;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.CosmosDb
{
    public class CosmosDbContainer : ICosmosDbContainer
    {
        public Container Container { get; }

        public CosmosDbContainer(CosmosClient cosmosClient,
            string databaseName,
            CosmosDbContainerType containerType)
        {

            this.Container = cosmosClient.GetContainer(databaseName, containerType.ToString());
        }
    }
}
