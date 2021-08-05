using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using Tacx.Activities.Core.Enums;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.CosmosDb
{
    public class CosmosDbContainerFactory : ICosmosDbContainerFactory
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly List<ContainerInfo> _containers;

        public CosmosDbContainerFactory(CosmosClient cosmosClient,
            string databaseName,
            List<ContainerInfo> containers)
        {
            _databaseName = databaseName;
            _containers = containers;
            _cosmosClient = cosmosClient;
        }

        public ICosmosDbContainer GetContainer(CosmosDbContainerType containerType)
        {
            if (_containers.Where(x => x.Name == containerType.ToString()) == null)
            {
                throw new ArgumentException($"Unable to find container: {containerType}");
            }

            return new CosmosDbContainer(_cosmosClient, _databaseName, containerType);
        }
    }
}
