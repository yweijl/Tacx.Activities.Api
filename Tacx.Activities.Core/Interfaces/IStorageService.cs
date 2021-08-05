using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;

namespace Tacx.Activities.Core.Interfaces
{
    public interface IStorageService
    {
        Task PersistActivityAsync(Activity requestActivity);
    }
}
