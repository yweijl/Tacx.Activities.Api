namespace Tacx.Activities.Core.Dtos.Extensions
{
    public static class ActivityDtoExtensions
    {
        public static bool IsEmpty(this ActivityDto activity)
            => string.IsNullOrWhiteSpace(activity.Id) &&
               string.IsNullOrWhiteSpace(activity.Name) &&
               string.IsNullOrWhiteSpace(activity.Description) &&
               activity.Distance == 0 &&
               activity.AvgSpeed == 0 &&
               activity.Duration == 0;
    }
}
