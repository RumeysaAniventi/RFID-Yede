using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class RdrKimliklendirmeParametreDegerResponse:ResponseOrtak
    {
        public string zReaderIp { get; set; }
        public string  zReaderPower { get; set; }
        public string  zRfidId{ get; set; }
    }
}