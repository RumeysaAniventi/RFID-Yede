using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class KimlikMalzemeIptalRequest:OrtakRequest
    {
        public string zepc { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zsernr { get; set; }
    }
}