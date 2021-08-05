﻿using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Core.Services;
using Tacx.Activities.Infrastructure.CosmosDb;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;
using Tacx.Activities.Infrastructure.Repository;

namespace Tacx.Activities.Api.DependencyConfigurator
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, CosmosDbSettings cosmosDbSettings)
        {
            services.AddScoped<IActivitiesRepository, ActivitiesRepository>();
            services.AddScoped<IStorageService, FilesStorageService>();

            var cosmosDbClientFactory = ConfigureCosmosDbAsync(cosmosDbSettings)
                .GetAwaiter()
                .GetResult();

            services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);
            return services;
        }

        private static async Task<CosmosDbContainerFactory> ConfigureCosmosDbAsync(CosmosDbSettings cosmosDbSettings)
        {
            var client = new CosmosClient(cosmosDbSettings.EndpointUrl, cosmosDbSettings.PrimaryKey);
            await client.CreateDatabaseIfNotExistsAsync(cosmosDbSettings.DatabaseName);
            var database = client.GetDatabase(cosmosDbSettings.DatabaseName)!;

            foreach (var container in cosmosDbSettings.Containers)
            {
                await database.DefineContainer(container.Name, container.PartitionKey)
                    .WithIndexingPolicy()
                    .WithAutomaticIndexing(true)
                    .WithIndexingMode(IndexingMode.Consistent)
                    .Attach()
                    .CreateIfNotExistsAsync();
            }

            return new CosmosDbContainerFactory(client, cosmosDbSettings.DatabaseName, cosmosDbSettings.Containers);
        }
    }
}