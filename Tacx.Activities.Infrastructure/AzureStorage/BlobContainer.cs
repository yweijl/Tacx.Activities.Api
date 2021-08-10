using System;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.SerializerExtensions;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;

namespace Tacx.Activities.Infrastructure.AzureStorage
{
    public class BlobContainer : IBlobContainer
    {
        private readonly IBlobStorageClient _client;

        public BlobContainer(IBlobStorageClient client)
        {
            _client = client;
        }

        public async Task<TEntity?> GetAsync<TEntity>(string id) where TEntity : EntityBase, new()
        {
            try
            {
                var container = _client.GetBlobContainer<TEntity>();
                var client = container.GetBlobClient(id);
                var blob = await client.DownloadAsync();
                return blob.Value.Content.StreamToEntity<TEntity>();  
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> AddAsync<TEntity>(string id, TEntity entity) where TEntity : EntityBase, new()
        {
            try
            {
                await using var content = entity.EntityToStream();
                await _client.GetBlobContainer<TEntity>().UploadBlobAsync(id, content);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<bool> DeleteAsync<TEntity>(string id) where TEntity : EntityBase, new()
        {
            try
            {
                await _client.GetBlobContainer<TEntity>().DeleteBlobAsync(id);
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
