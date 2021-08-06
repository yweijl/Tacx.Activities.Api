using Microsoft.Azure.Cosmos;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Infrastructure.CosmosDb.Interfaces
{
    public interface ICosmosDbClient
    {
        Container GetContainer<TEntity>() where TEntity : EntityBase, new();
    }
}