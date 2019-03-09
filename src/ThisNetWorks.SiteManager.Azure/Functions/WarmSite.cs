using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ThisNetWorks.SiteManager.Azure.Dtos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ThisNetWorks.SiteManager.Azure.Functions
{
    public static class WarmSite
    {

        [FunctionName("WarmSite")]
        public static async Task<SiteWarmerResultDto> RunWarmSite(
            [OrchestrationTrigger] DurableOrchestrationContext context
            , ILogger log)
        {
            var manageSiteDto = context.GetInput<ManageSiteDto>();

            var siteWarmerDto = await context.CallActivityAsync<SiteWarmerDto>("GetSiteDetails"
                , manageSiteDto);

            if (siteWarmerDto == null)
            {
                throw new Exception("Site warmer details null, aborting");
            }

            var siteWarmerResultDto = new SiteWarmerResultDto();

            log.LogInformation($"Warming site: Endpoints to hit {siteWarmerDto.EndpointsToHit.Count}, Urls to hit {siteWarmerDto.RelativeUrls.Count}");

            foreach (var endpoint in siteWarmerDto.EndpointsToHit)
            {
                log.LogDebug($"Starting hits on endpoint {endpoint.Host}");
                foreach (var url in siteWarmerDto.RelativeUrls)
                {
                    var urlResult = await context.CallActivityAsync<UrlResultDto>("HitUrl"
                        , new UrlRequestDto() { Scheme = endpoint.Scheme, Host = endpoint.Host, Path = url });

                    siteWarmerResultDto.UrlResultDtos.Add(urlResult);
                }
            }

            return siteWarmerResultDto;
        }


    }
}