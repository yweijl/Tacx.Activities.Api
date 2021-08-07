using System.Threading.Tasks;
using Tacx.Activities.Core.Dtos;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : EntityBase, new()
    {
        Task<bool> CreateAsync(TEntity activity);
        Task<TEntity?> GetByIdAsync(string id);
        Task<bool> DeleteAsync(string requestId);
        Task<bool> UpsertAsync(TEntity entity);
    }
}
