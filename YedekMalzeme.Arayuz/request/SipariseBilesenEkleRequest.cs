using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class SipariseBilesenEkleRequest:OrtakRequest
    {
        public string zid { get; set; }
        public string zaufnr { get; set; }
    }
}