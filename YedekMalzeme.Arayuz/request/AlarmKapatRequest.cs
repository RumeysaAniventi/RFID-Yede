using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class AlarmKapatRequest : OrtakRequest
    {
        public string zid { get; set; }
        public string zaciklama { get; set; }
    }
}