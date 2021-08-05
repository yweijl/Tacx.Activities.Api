using System.IO;
using System.Threading.Tasks;
using Tacx.Activities.Core.Entities;
using Tacx.Activities.Core.Interfaces;

namespace Tacx.Activities.Core.Services
{
    public class FilesStorageService : IStorageService
    {
        public FilesStorageService()
        {

        }

        public bool SaveFile(Stream file, string fileName, bool overwrite = false)
        {
            return true;
        }

        public Task PersistActivityAsync(Activity requestActivity)
        {
            throw new System.NotImplementedException();
        }
    }
}
