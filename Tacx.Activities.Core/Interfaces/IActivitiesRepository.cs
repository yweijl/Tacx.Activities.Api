using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Core.Interfaces
{
    public interface IActivitiesRepository
    {
        Task<bool> InsertAsync(Activity activity);
        Task<Activity?> GetByIdAsync(long id);
    }
}
