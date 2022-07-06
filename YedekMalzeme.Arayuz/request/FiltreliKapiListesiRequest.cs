using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class FiltreliKapiListesiRequest:OrtakRequest
    {
        public string zilktarih { get; set; }
        public string zsontarih { get; set; }

        //  public string zkimliktarih { get; set; }
        public string zaufnr { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zgecisizni { get; set; }
        public string zalarm { get; set; }

    }
}