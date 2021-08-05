using Microsoft.Azure.Cosmos;

namespace Tacx.Activities.Infrastructure.CosmosDb.Interfaces
{
    public interface ICosmosDbContainer
    {
        Container Container { get; }
    }
}
