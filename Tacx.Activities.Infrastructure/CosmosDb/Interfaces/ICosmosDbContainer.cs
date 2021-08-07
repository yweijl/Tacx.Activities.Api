using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Infrastructure.CosmosDb.Interfaces
{
    public interface ICosmosDbContainer<TEntity> where TEntity : EntityBase, new()
    {
        public Task<TEntity?> ReadItemAsync(string id);
        public Task<bool> CreateAsync(TEntity entity);
        public Task<bool> DeleteAsync(string id);
        Task<bool> UpsertAsync(TEntity entity);
    }
}
