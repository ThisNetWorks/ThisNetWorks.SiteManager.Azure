using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ThisNetWorks.SiteManager.Azure.Dtos
{
    public class UrlRequestDto
    {
        public string Scheme { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
    }
}
