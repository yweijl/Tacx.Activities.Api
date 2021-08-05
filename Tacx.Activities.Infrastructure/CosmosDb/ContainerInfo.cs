namespace Tacx.Activities.Infrastructure.CosmosDb
{
    public class ContainerInfo
    {
        public string Name { get; set; } = default!;
        public string PartitionKey { get; set; } = default!;
    }
}
