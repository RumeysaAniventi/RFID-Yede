using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class AlarmGoruntuleRequest : OrtakRequest
    {
        public string zeps { get; set; }
        public string zaufnr { get; set; }
        public string zmatnr { get; set; }
    }
}