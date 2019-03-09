using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.SiteManager.Azure.Dtos
{
    public class SiteWarmerDto
    {
        public IList<EndpointDto> EndpointsToHit { get; set; }

        public IList<string> RelativeUrls { get; set; }
    }
}
