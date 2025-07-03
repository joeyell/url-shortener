param appServicePlanName string = 'api'
param location string = resourceGroup().location
param appName string
param keyVaultName string
param appSettings array = []

resource appServicePlan 'Microsoft.Web/serverfarms@2024-11-01' = {
  kind: 'linux'
  location: location
  name: appServicePlanName
  properties: {
    reserved: true
  }
  sku: {
    name: 'B1'
  }
}

resource webApp 'Microsoft.Web/sites@2024-11-01' = {
  name: appName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      linuxFxVersion: 'DOTNETCORE|8.0'
      appSettings: concat( 
        [
          {
            name: 'keyVaultName'
            value: keyVaultName
          }
        ], 
        appSettings
      )
    }
  }
}

resource webAppConfig 'Microsoft.Web/sites/config@2024-11-01' = {
  parent: webApp
  name: 'web'
  properties: {
    scmType: 'GitHub'
  }
}

output appServiceId string = webApp.id
output principalId string = webApp.identity.principalId
