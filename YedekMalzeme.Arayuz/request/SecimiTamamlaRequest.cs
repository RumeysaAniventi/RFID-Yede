using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class SecimiTamamlaRequest:OrtakRequest
    {
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
    }
}