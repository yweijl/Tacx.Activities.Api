using Azure.Storage.Blobs;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Infrastructure.AzureStorage.Interfaces
{
    public interface IBlobStorageClient
    {
        BlobContainerClient GetBlobContainer<TEntity>() where TEntity : EntityBase, new();

        Task CreateIfNotExistsAsync();
    }
}