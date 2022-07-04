using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class KimlikIptalMalzemeGetirResponse:ResponseOrtak
    {
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zsernr { get; set; }
    }
}