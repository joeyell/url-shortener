param location string = resourceGroup().location
param vaultName string

resource keyvault 'Microsoft.KeyVault/vaults@2024-11-01' = {
  name: vaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: subscription().tenantId
    enableRbacAuthorization: true
  }
}

output name string = keyvault.name
output id string = keyvault.id
