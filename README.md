# url-shortener

## Infrastructure

### Download Azure CLI
https://learn.microsoft.com/en-us/cli/azure/

### Log in to Azure
```bash
az login
```

### Create Resource Group
```bash
az group create --name urlshortener-dev --location uksouth
```

### Deploy API
```bash
az deployment group create --resource-group urlshortener-dev --template-file {path/to/main.bicep}
```

### Create User for GitHub actions
```bash
az ad sp create-for-rbac --name "GitHub-Actions-SP" \
                         --role  contributor \
                         --scopes /subscriptions/\${ subscriptionId } \
                         --sdk-auth
```

### Configure a federated credential on an app

https://learn.microsoft.com/en-gb/entra/workload-id/workload-identity-federation-create-trust-user-assigned-managed-identity?pivots=identity-wif-mi-methods-azp#configure-a-federated-identity-credential-on-a-user-assigned-managed-identity