using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class AlarmKapatRequest : OrtakRequest
    {
        public string zeps { get; set; }
        public string zaciklama { get; set; }
    }
}