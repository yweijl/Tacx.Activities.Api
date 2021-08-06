using Newtonsoft.Json;

namespace Tacx.Activities.Core.Entities
{
    public abstract class EntityBase
    {
        [JsonProperty("id")]
        public string Id { get; set; } = default!;
    }
}