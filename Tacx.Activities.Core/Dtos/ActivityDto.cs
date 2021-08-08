using System;

namespace Tacx.Activities.Core.Dtos
{
    public class ActivityDto : IEquatable<ActivityDto>
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Distance { get; set; }
        public double Duration { get; set; }
        public double AvgSpeed { get; set; }

        public bool Equals(ActivityDto? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Description == other.Description &&
                   Distance.Equals(other.Distance) && Duration.Equals(other.Duration) &&
                   AvgSpeed.Equals(other.AvgSpeed);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ActivityDto) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Description, Distance, Duration, AvgSpeed);
        }
    }
}
