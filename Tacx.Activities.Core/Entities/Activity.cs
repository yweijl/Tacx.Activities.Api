namespace Tacx.Activities.Core.Entities
{
    public class Activity : EntityBase
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double AvgSpeed { get; set; }
    }
}
