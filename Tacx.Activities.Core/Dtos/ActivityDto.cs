using System;

namespace Tacx.Activities.Core.Dtos
{
    public class ActivityDto : IEquatable<ActivityDto>
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Distance { get; set; }
        public int Duration { get; set; }
        public double AvgSpeed { get; set; }
        public int AvgRpm { get; set; }
        public int AvgBpm { get; set; }
        public int AvgWatt { get; set; }

        public bool Equals(ActivityDto? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Description == other.Description &&
                   Distance.Equals(other.Distance) && Duration == other.Duration && AvgSpeed.Equals(other.AvgSpeed) &&
                   AvgRpm == other.AvgRpm && AvgBpm == other.AvgBpm && AvgWatt == other.AvgWatt;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ActivityDto)obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Name);
            hashCode.Add(Description);
            hashCode.Add(Distance);
            hashCode.Add(Duration);
            hashCode.Add(AvgSpeed);
            hashCode.Add(AvgRpm);
            hashCode.Add(AvgBpm);
            hashCode.Add(AvgWatt);
            return hashCode.ToHashCode();
        }
    }
}
