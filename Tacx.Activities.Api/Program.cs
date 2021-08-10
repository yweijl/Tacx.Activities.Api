using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await EnsureDbCreatedAsync(host);
            await EnsureBlobContainersCreatedAsync(host);
            await host.RunAsync();
        }

        private static async Task EnsureBlobContainersCreatedAsync(IHost host)
        {
            var blobStorageClient = host.Services.GetService<IBlobStorageClient>();
            await blobStorageClient!.CreateIfNotExistsAsync();
        }

        private static async Task EnsureDbCreatedAsync(IHost host)
        {
            var cosmosDbClient = host.Services.GetService<ICosmosDbClient>();
            await cosmosDbClient!.CreateIfNotExistsAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
