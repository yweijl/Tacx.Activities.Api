using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Infrastructure.AzureStorage.Interfaces;

namespace Tacx.Activities.Infrastructure.AzureStorage
{
    public class BlobStorageClient : IBlobStorageClient
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly IEnumerable<string> _containers;

        public BlobStorageClient(BlobServiceClient blobServiceClient, IEnumerable<string> containers)
        {
            _blobServiceClient = blobServiceClient;
            _containers = containers;
        }

        public BlobContainerClient GetBlobContainer<TEntity>() where TEntity : EntityBase, new()
        {
            var containerName = typeof(TEntity).Name.ToLower();
            if (_containers.All(x => x != containerName))
            {
                throw new ArgumentOutOfRangeException(containerName);
            }

            return _blobServiceClient.GetBlobContainerClient(containerName);
        }

        public async Task CreateIfNotExistsAsync()
        {
            foreach (var containerName in _containers)
            {
                var container = _blobServiceClient.GetBlobContainerClient(containerName.ToLower());
                await container.CreateIfNotExistsAsync();
            }
        }
    }
}