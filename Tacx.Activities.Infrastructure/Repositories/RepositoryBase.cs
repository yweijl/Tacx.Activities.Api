using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        private readonly ICosmosDbContainer<TEntity> _container;

        protected RepositoryBase(ICosmosDbContainer<TEntity> container)
        {
            _container = container;
        }

        public virtual Task<bool> CreateAsync(TEntity entity) 
        {
            return _container.CreateAsync(entity);
        }

        public virtual Task<TEntity?> GetByIdAsync(string id)
        {
            return _container.ReadItemAsync(id);
        }

        public Task<bool> DeleteAsync(string id)
        {
            return _container.DeleteAsync(id);
        }
    }
}
