using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tacx.Activities.Core.CommandHandlers;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;

namespace Tacx.Activities.Api.DependencyConfigurator
{
    public static class ConfigureCore
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateActivityCommand, ActivityDto>, CreateActivityCommandHandler>();
            return services;
        }
    }
}
