using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class ElTerminaliResponse:ResponseOrtak
    {

        public string zid { get; set; }
        public string zepc { get; set; }        
        public string zaufnr { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zdurumMesaj { get; set; }



    }
}