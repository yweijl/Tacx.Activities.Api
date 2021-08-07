using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Tacx.Activities.Api.DependencyConfigurator
{
    public static class ConfigureApi
    {
        public static IServiceCollection RegisterApi(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddMediatR(typeof(Startup));
            services.AddSwaggerGen();
            return services;
        }
    }
}
