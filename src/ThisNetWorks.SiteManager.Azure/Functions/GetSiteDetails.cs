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
        [FunctionName("GetSiteDetails")]
        public static async Task<SiteWarmerDto> RunGetSiteDetails([ActivityTrigger] ManageSiteDto manageSiteDto, ILogger log)
        {
            log.LogDebug($"Getting site warmer list from endpoint {manageSiteDto.SiteWarmerEndPoint}");

            SiteWarmerDto siteWarmerDto = null;
            using (var siteClient = new HttpClient())
            {
                var res = await siteClient.GetAsync(manageSiteDto.SiteWarmerEndPoint);
                siteWarmerDto = await res.Content.ReadAsAsync<SiteWarmerDto>();
            }

            return siteWarmerDto;

        }
    }
}
