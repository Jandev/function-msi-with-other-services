using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace ServerlessIdentity
{
    public static class GetData
    {
        [FunctionName(nameof(GetData))]
        public static HttpResponseMessage Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]HttpRequestMessage req, 
            TraceWriter log)
        {
            log.Info($"C# HTTP trigger `{nameof(GetData)}` processed a request.");

            return req.CreateResponse(HttpStatusCode.OK, "Hello World");
        }
    }
}
