using ThisNetWorks.SiteManager.Azure.Dtos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ThisNetWorks.SiteManager.Azure.Functions
{
    public static class ManageSite
    {
        [FunctionName("ManageSite")]
        public static async Task<HttpResponseMessage> RunManageSite
            (
            [HttpTrigger(AuthorizationLevel.Function, "post")]HttpRequestMessage req,
            [OrchestrationClient]DurableOrchestrationClient starter,
            ILogger log)
        {
            var manageSiteDto = await req.Content.ReadAsAsync<ManageSiteDto>();

            //it sends the recieved object to the warmsite
            string instanceId = await starter.StartNewAsync("WarmSite", manageSiteDto);

            log.LogInformation($"Started Site Warmer with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}
