using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Infrastructure.AzureStorage.Interfaces
{
    public interface IBlobContainer
    {
        public Task<TEntity?> GetAsync<TEntity>(string id) where TEntity : EntityBase, new();

        Task<bool> AddAsync<TEntity>(string id, TEntity content) where TEntity : EntityBase, new();
        Task<bool> DeleteAsync<TEntity>(string id) where TEntity : EntityBase, new();
    }
}