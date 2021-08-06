using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Infrastructure.CosmosDb.Interfaces;

namespace Tacx.Activities.Infrastructure.CosmosDb
{
    public class CosmosDbContainer<TEntity> : ICosmosDbContainer<TEntity> where TEntity : EntityBase, new()
    {
        private readonly Container _container;

        public CosmosDbContainer(ICosmosDbClient client)
        {
            _container = client.GetContainer<TEntity>();
        }

        public async Task<TEntity?> ReadItemAsync(string id) 
        {
            try
            {
                var entity = await _container.ReadItemAsync<TEntity>(id, new PartitionKey(id));
                return entity;
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            try
            {
                await _container.CreateItemAsync(entity, new PartitionKey(entity.Id));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }

            return true;
        }
    }
}
