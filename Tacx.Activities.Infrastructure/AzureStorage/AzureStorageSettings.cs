using System.Collections.Generic;

namespace Tacx.Activities.Infrastructure.AzureStorage
{
    public class AzureStorageSettings
    {
        public string ConnectionString { get; set; } = default!;
        public IEnumerable<string> Containers { get; set; } = default!;
    }
}
