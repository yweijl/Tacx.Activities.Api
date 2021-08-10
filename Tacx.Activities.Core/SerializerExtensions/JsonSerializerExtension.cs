using System.IO;
using Tacx.Activities.Core.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Tacx.Activities.Core.SerializerExtensions
{
    public static class JsonSerializerExtension
    {
        public static Stream EntityToStream<TEntity>(this TEntity entity) where TEntity : EntityBase, new()
        {
            var data = JsonSerializer.SerializeToUtf8Bytes(entity);
            return new MemoryStream(data);
        }

        public static TEntity? StreamToEntity<TEntity>(this Stream stream) where TEntity : EntityBase, new()
        {
            var streamReader = new StreamReader(stream);
            var entity = JsonSerializer.Deserialize<TEntity>(streamReader.ReadToEnd());
            return entity;
        }
    }
}