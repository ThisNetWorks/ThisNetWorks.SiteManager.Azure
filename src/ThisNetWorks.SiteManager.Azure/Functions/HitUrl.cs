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
        log.LogDebug($"Hitting url {requestDto.Endpoint} {requestDto.Url}");


            var uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Path = requestDto.Url;
            uriBuilder.Host = requestDto.Endpoint;

            var result = await GetSiteDetails.HttpClient.GetAsync(uriBuilder.ToString());

            if (result.StatusCode != HttpStatusCode.OK)
            {
                log.LogError($"Error hitting {requestDto.Endpoint} {requestDto.Url}, status code {result.StatusCode}");
            }
            else
            {
                log.LogDebug($"Hit url ok {requestDto.Endpoint} {requestDto.Url}");
            }

            return new UrlResultDto()
            {
                StatusCode = result.StatusCode,
                Url = result.RequestMessage.RequestUri.ToString()
            };
        }
    }
}
