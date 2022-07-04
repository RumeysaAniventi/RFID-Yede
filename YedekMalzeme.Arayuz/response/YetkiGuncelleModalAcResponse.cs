using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class YetkiGuncelleModalAcResponse:ResponseOrtak
    {
        public string zid { get; set; }
        public string zAdi { get; set; }
        public string zSoyAdi { get; set; }
        public string zKullaniciAdi { get; set; }
        public string zYetki { get; set; }
    }
}