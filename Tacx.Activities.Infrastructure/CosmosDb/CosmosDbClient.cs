using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Infrastructure.CosmosDb.ConfigModels;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.CosmosDb
{
    public class CosmosDbClient : ICosmosDbClient
    {
        private readonly CosmosClient _cosmosClient;
        private readonly string _databaseName;
        private readonly List<ContainerInfo> _containers;

        public CosmosDbClient(CosmosClient cosmosClient,
            string databaseName,
            List<ContainerInfo> containers)
        {
            _databaseName = databaseName;
            _containers = containers;
            _cosmosClient = cosmosClient;
        }

        public Container GetContainer<TEntity>() where TEntity : EntityBase, new()
        {
            var containerName = typeof(TEntity).Name;

            if (_containers.All(x => x.Name != containerName))
            {
                throw new ArgumentException($"Unable to find container: {containerName}");
            }

            return _cosmosClient.GetContainer(_databaseName, containerName);
        }

        public async Task CreateIfNotExistsAsync()
        {
            await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);
            var database = _cosmosClient.GetDatabase(_databaseName)!;

            foreach (var container in _containers)
            {
                await database.DefineContainer(container.Name, container.PartitionKey)
                    .WithIndexingPolicy()
                    .WithAutomaticIndexing(true)
                    .WithIndexingMode(IndexingMode.Consistent)
                    .Attach()
                    .CreateIfNotExistsAsync();
            }
        }
    }
}
