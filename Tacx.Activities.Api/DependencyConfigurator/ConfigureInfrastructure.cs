using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Core.Services;
using Tacx.Activities.Infrastructure.CosmosDb;
using Tacx.Activities.Infrastructure.CosmosDb.ConfigModels;
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

            var cosmosDbClient = GetCosmosDbClient(cosmosDbSettings);

            services.AddSingleton<ICosmosDbClient>(cosmosDbClient);

            services.AddScoped<ICosmosDbContainer<Activity>, CosmosDbContainer<Activity>>();
            return services;
        }

        private static CosmosDbClient GetCosmosDbClient(CosmosDbSettings cosmosDbSettings)
        {
            var client = new CosmosClient(cosmosDbSettings.EndpointUrl, cosmosDbSettings.PrimaryKey);
            return new CosmosDbClient(client, cosmosDbSettings.DatabaseName, cosmosDbSettings.Containers);
        }
    }
}
