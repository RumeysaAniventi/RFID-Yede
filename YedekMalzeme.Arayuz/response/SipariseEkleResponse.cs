using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class SipariseEkleResponse:ResponseOrtak
    {
        public string zaufnrlistesi { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zsern { get; set; }
        public string zepc { get; set; }

    }
}