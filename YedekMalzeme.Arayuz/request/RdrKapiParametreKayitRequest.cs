using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class RdrKapiParametreKayitRequest:OrtakRequest
    {
        public string zReaderIp { get; set; }
        public string zReaderPower { get; set; }
        public string zRfidId { get; set; }
    }
}