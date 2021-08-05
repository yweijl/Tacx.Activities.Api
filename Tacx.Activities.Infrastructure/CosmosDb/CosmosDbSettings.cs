using System.Collections.Generic;

namespace Tacx.Activities.Infrastructure.CosmosDb
{
    public class CosmosDbSettings
    {
        public string EndpointUrl { get; set; } = default!;
        public string PrimaryKey { get; set; } = default!;
        public string DatabaseName { get; set; } = default!;
        public List<ContainerInfo> Containers { get; set; } = default!;
    }
}
