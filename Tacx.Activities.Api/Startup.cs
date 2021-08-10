using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tacx.Activities.Api.DependencyConfigurator;
using Tacx.Activities.Infrastructure.AzureStorage;
using Tacx.Activities.Infrastructure.CosmosDb.ConfigModels;
using Tacx.Activities.Infrastructure.Strava;

namespace Tacx.Activities.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cosmosDbSettings = Configuration.GetSection(nameof(CosmosDbSettings)).Get<CosmosDbSettings>();
            var azureStorageSettings = Configuration.GetSection(nameof(AzureStorageSettings)).Get<AzureStorageSettings>();
            var stravaApiSettings = Configuration.GetSection(nameof(StravaApiSettings)).Get<StravaApiSettings>();
            services
                .RegisterApi()
                .RegisterCore()
                .RegisterInfrastructure(cosmosDbSettings, azureStorageSettings, stravaApiSettings);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
