using Azure.Storage.Blobs;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Infrastructure.AzureStorage;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb;
using Tacx.Activities.Infrastructure.CosmosDb.ConfigModels;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;
using Tacx.Activities.Infrastructure.Repositories;

namespace Tacx.Activities.Api.DependencyConfigurator
{
    public static class ConfigureInfrastructure
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, CosmosDbSettings cosmosDbSettings, AzureStorageSettings azureStorageSettings)
        {
            services.AddScoped<IRepository<Activity>, ActivitiesRepository>();

            var blobStorageClient = GetBlobStorageClient(azureStorageSettings);
            services.AddSingleton(blobStorageClient);

            services.AddScoped<IBlobContainer, BlobContainer>();

            var cosmosDbClient = GetCosmosDbClient(cosmosDbSettings);
            services.AddSingleton(cosmosDbClient);

            services.AddScoped<ICosmosDbContainer<Activity>, CosmosDbContainer<Activity>>();

            return services;
        }

        private static ICosmosDbClient GetCosmosDbClient(CosmosDbSettings cosmosDbSettings)
        {
            var client = new CosmosClient(cosmosDbSettings.EndpointUrl, cosmosDbSettings.PrimaryKey);
            return new CosmosDbClient(client, cosmosDbSettings.DatabaseName, cosmosDbSettings.Containers);
        }

        private static IBlobStorageClient GetBlobStorageClient(AzureStorageSettings azureStorageSettings)
        {
            var blobServiceClient = new BlobServiceClient(azureStorageSettings.ConnectionString);
            return new BlobStorageClient(blobServiceClient, azureStorageSettings.Containers);
        }
    }
}
