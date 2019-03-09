using System;
using System.Collections.Generic;
using System.Text;

namespace ThisNetWorks.SiteManager.Azure.Dtos
{
    public class ManageSiteDto
    {
        public string SiteWarmerScheme { get; set; }
        public string SiteWarmerHost { get; set; }
        public string SiteWarmerPath { get; set; }
        public string SiteWarmerAuthKey { get; set; }
    }
}
