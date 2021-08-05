using Newtonsoft.Json;

namespace Tacx.Activities.Core.Entities
{
    public class Activity
    {
        [JsonProperty("id")] 
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double AvgSpeed { get; set; }
    }
}
