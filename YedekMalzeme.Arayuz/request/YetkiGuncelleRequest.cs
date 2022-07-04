using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class YetkiGuncelleRequest:OrtakRequest
    {
        public string zid { get; set; }
        public string zad { get; set; }
        public string zsoyad { get; set; }
        public string zkullaniciadi { get; set; }
        public string zyetki { get; set; }


       
    }
}