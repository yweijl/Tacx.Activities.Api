using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await EnsureDbCreatedAsync(host);
            await host.RunAsync();
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
