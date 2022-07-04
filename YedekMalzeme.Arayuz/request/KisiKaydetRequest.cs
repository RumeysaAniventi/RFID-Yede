using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class KisiKaydetRequest:OrtakRequest
    {
        public string zkullaniciadi { get; set; }
        public string zadi { get; set; }
        public string zsoyadi { get; set; }
        public string zsifre { get; set; }
        public string zyetki { get; set; }
    }
}