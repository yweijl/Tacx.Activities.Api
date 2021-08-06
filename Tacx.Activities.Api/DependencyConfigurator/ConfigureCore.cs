using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tacx.Activities.Core.CommandHandlers;
using Tacx.Activities.Core.Commands;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Queries;
using Tacx.Activities.Core.QueryHandlers;

namespace Tacx.Activities.Api.DependencyConfigurator
{
    public static class ConfigureCore
    {
        public static IServiceCollection RegisterCore(this IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CreateActivityCommand, ActivityDto>, CreateActivityCommandHandler>();
            services.AddScoped<IRequestHandler<GetActivityQuery, ActivityDto?>, GetActivityQueryHandler>();
            return services;
        }
    }
}
