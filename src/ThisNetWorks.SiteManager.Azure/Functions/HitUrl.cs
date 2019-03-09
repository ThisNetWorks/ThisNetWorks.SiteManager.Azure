using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using ThisNetWorks.SiteManager.Azure.Dtos;

namespace ThisNetWorks.SiteManager.Azure.Functions
{
    public class HitUrl
    {
        [FunctionName("HitUrl")]
        public static async Task<UrlResultDto> RunHitUrl([ActivityTrigger] UrlRequestDto requestDto, ILogger log)
        {
            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = requestDto.Scheme;
            uriBuilder.Path = requestDto.Path;
            uriBuilder.Host = requestDto.Host;
            var uri = uriBuilder.ToString();
            log.LogDebug($"Hitting url {uri}");
            var result = await GetSiteDetails.HttpClient.GetAsync(uri);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                log.LogError($"Error hitting {uri}, status code {result.StatusCode}");
            }
            else
            {
                log.LogDebug($"Hit url ok {uri}");
            }

            return new UrlResultDto()
            {
                StatusCode = result.StatusCode,
                Url = result.RequestMessage.RequestUri.ToString()
            };
        }
    }
}
