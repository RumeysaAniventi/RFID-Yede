using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class StokSayResponse:ResponseOrtak
    {
        public string zdepo { get; set; }
        public string zkoltukdepo { get; set; }
        public string ztuketim { get; set; }
    }
}