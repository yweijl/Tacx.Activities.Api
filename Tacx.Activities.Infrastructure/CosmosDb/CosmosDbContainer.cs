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
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            { 
                await _container.DeleteItemAsync<TEntity>(id, new PartitionKey(id));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
