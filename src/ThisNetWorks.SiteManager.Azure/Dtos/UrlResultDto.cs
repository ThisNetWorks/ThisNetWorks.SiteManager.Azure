using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ThisNetWorks.SiteManager.Azure.Dtos
{
    public class UrlResultDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Url { get; set; }
    }
}
