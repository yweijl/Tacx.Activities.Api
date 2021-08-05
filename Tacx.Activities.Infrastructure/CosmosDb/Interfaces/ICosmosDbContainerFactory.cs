using Tacx.Activities.Core.Enums;

namespace Tacx.Activities.Infrastructure.CosmosDb.Interfaces
{
    public interface ICosmosDbContainerFactory
    {
        ICosmosDbContainer GetContainer(CosmosDbContainerType containerType);
    }
}