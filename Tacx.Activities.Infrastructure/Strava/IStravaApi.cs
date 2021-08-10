using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Infrastructure.Strava
{
    public interface IStravaApi
    {
        Task<bool> PostAsync(Activity activity);
    }
}