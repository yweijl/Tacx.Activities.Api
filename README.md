# Development environment

## Setup CosmosDB

- Download, install and run the [Azure Cosmos Emulator](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator?tabs=cli%2Cssl-netstd21)
- Open the [local data explorer](https://localhost:8081/_explorer/index.html) and copy the `Primary Connection String` use this as the ConnectionString as the value for the connectionString in the Setup Secrets step

## Setup Azure Storage Queue

- Download, install and run the [Azurite](https://docs.microsoft.com/en-us/azure/storage/common/storage-use-azurite?tabs=visual-studio)

## Setup Secrets

Add some secrets to `user-secrets` by running this in the folder `/Tacx.Activities.Api/Tacx.Activities.Api`

- `dotnet user-secrets set "ConnectionStrings:NLOControlCenter" "AccountEndpoint=https://localhost:8081/;`
- `dotnet user-secrets set "CosmosDbSettings:PrimaryKey" "YOUR_COSMOS_PRIMARY_KEY"`
- `dotnet user-secrets set "AzureStorageSettings:ConnectionString" "AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;"`
