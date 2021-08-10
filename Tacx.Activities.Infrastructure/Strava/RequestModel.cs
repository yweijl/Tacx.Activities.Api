using System;
using System.Text.Json.Serialization;

namespace Tacx.Activities.Infrastructure.Strava
{
    public class RequestModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = default!;
        [JsonPropertyName("type")]
        public string Type { get; set; } = default!;
        [JsonPropertyName("start_date_local")]
        public DateTime StartDateLocal { get; set; } = default!;
        [JsonPropertyName("elapsed_time")]
        public int ElapsedTime { get; set; } = default!;
        [JsonPropertyName("description")]
        public string Description { get; set; } = default!;
        [JsonPropertyName("distance")] 
        public float Distance { get; set; } = default!;
        [JsonPropertyName("trainer")]
        public int Trainer { get; set; } = 1;
        [JsonPropertyName("commute")]
        public int Commute { get; set; } = 0;
    }
}
