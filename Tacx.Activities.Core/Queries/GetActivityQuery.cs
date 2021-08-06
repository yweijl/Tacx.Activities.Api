using MediatR;
using Tacx.Activities.Core.Dtos;

namespace Tacx.Activities.Core.Queries
{
    public record GetActivityQuery(string Id) : IRequest<ActivityDto?>;
}
