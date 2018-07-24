# Azure Functions :heart: MSI :heart: Azure

An Azure Function App using a Managed Service Identity to connect to other Azure services

# What is this?

What's over here is an Azure Function using the new MSI (Managed Service Identity) functionality provided in Azure to access the Azure Key Vault.

This piece of code is handling the retrieval of a secret.

    var azureServiceTokenProvider = new AzureServiceTokenProvider();
    var keyvaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                
    var secretValue = await keyvaultClient.GetSecretAsync("https://functions-msi-vault.vault.azure.net/", "MyFunctionSecret");

## How have you set this up?

Well using ARM templates, of course!

The ARM template can be found in the `Deployment` folder over here.
You need to add the following to the App Service resource:

    "identity": {
        "type": "SystemAssigned"
      },

When this is succesfull, you can access the `tenantId` and `objectId` for this application in the ARM template using the following expressions
    
	"tenantId": "[reference(concat('Microsoft.Web/sites/', parameters('webSiteName')), '2018-02-01', 'Full').identity.tenantId]",
    "objectId": "[reference(concat('Microsoft.Web/sites/', parameters('webSiteName')), '2018-02-01', 'Full').identity.principalId]",

