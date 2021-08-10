namespace Tacx.Activities.Core.Entities
{
    public class Activity : EntityBase
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Distance { get; set; }
        public int Duration { get; set; }
        public double AvgSpeed { get; set; }
        public int AvgRpm { get; set; }
        public int AvgBpm { get; set; }
        public int AvgWatt { get; set; }
    }
}
