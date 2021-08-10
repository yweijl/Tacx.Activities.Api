using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Infrastructure.Strava
{
    public class StravaApi : IStravaApi
    {
        private readonly RestClient _restClient;
        private readonly StravaApiSettings _apiSettings;

        public StravaApi(StravaApiSettings apiSettings)
        {
            _restClient = new RestClient("https://www.strava.com");
            _apiSettings = apiSettings;
        }

        public async Task<bool> PostAsync(Activity activity)
        {
            try
            {
                var model = new RequestModel
                {
                    Name = activity.Name,
                    Description = activity.Description ?? string.Empty,
                    Distance = (float)activity.Distance,
                    StartDateLocal = DateTime.Now.ToUniversalTime(),
                    ElapsedTime = activity.Duration,
                    Type = "Ride"
                };

                var json = JsonSerializer.Serialize(model);

                var accessToken = await RefreshAccessToken();
                _restClient.Authenticator = new JwtAuthenticator(accessToken);
                
                var request = new RestRequest("api/v3/activities", Method.POST).AddJsonBody(json);
                var response = await _restClient.ExecuteAsync(request);

                return response.StatusCode == HttpStatusCode.Created;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private async Task<string> RefreshAccessToken()
        {
            try
            {
                var request = new RestRequest("oauth/token")
                    .AddQueryParameter("client_id", _apiSettings.ClientId)
                    .AddQueryParameter("client_secret", _apiSettings.ClientSecret)
                    .AddQueryParameter("refresh_token", _apiSettings.RefreshToken)
                    .AddQueryParameter("grant_type", _apiSettings.GrantTypeRefresh);

                var response = await _restClient.PostAsync<string>(request);
                var jObject = JObject.Parse(response);
                return jObject["access_token"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}