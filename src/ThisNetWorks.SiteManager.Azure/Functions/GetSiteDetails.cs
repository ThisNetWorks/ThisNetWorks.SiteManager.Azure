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
    public class GetSiteDetails
    {
        public static HttpClient HttpClient = new HttpClient();

        [FunctionName("GetSiteDetails")]
        public static async Task<SiteWarmerDto> RunGetSiteDetails([ActivityTrigger] ManageSiteDto manageSiteDto, ILogger log)
        {
            log.LogDebug($"Getting site warmer list from endpoint {manageSiteDto.SiteWarmerPath}");

            SiteWarmerDto siteWarmerDto = null;

            var uriBuilder = new UriBuilder()
            {
                Scheme = manageSiteDto.SiteWarmerScheme,
                Path = manageSiteDto.SiteWarmerPath,
                Host = manageSiteDto.SiteWarmerHost
            };

            var httpRequestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uriBuilder.ToString()),
                Headers = {
                            { HttpRequestHeader.Accept.ToString(), "application/json" },
                            { "x-auth-key", manageSiteDto.SiteWarmerAuthKey }
                        }
            };


            var res = await HttpClient.SendAsync(httpRequestMessage);
            siteWarmerDto = await res.Content.ReadAsAsync<SiteWarmerDto>();

            return siteWarmerDto;

        }
    }
}
