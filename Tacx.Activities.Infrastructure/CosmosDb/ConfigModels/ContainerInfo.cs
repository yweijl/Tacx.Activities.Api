namespace Tacx.Activities.Infrastructure.CosmosDb.ConfigModels
{
    public class ContainerInfo
    {
        public string Name { get; set; } = default!;
        public string PartitionKey { get; set; } = default!;
    }
}
