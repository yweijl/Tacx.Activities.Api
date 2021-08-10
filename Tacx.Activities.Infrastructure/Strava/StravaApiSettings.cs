namespace Tacx.Activities.Infrastructure.Strava
{
    public class StravaApiSettings
    {
        public string ClientId { get; set; } = default!;
        public string ClientSecret { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string GrantTypeAuth { get; set; } = default!;
        public string GrantTypeRefresh { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;

    }
}