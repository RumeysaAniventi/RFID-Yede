using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class KisiGetirResponse:ResponseOrtak
    {
        public string zadi { get; set; }
        public string zsoyadi { get; set; }
    }
}