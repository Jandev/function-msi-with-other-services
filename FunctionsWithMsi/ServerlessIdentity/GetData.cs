using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ServerlessIdentity
{
    public static class GetData
    {
        [FunctionName(nameof(GetData))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req, 
            TraceWriter log)
        {
            log.Info($"C# HTTP trigger `{nameof(GetData)}` processed a request.");

            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            // You can use the `KeyVaultTokenCallback` for the `KeyVaultClient`
            var keyvaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            
            var secretValue = await keyvaultClient.GetSecretAsync("https://functions-msi-vault.vault.azure.net/", "MyFunctionSecret");
            
            return req.CreateResponse(HttpStatusCode.OK, $"Hello World! This is my secret value: `{secretValue.Value}`.");
        }
    }
}
