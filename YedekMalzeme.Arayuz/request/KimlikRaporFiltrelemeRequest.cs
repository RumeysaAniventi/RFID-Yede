using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class KimlikRaporFiltrelemeRequest:OrtakRequest
    {
        public string zilktarih { get; set; }
        public string zsontarih { get; set; }
        
      //  public string zkimliktarih { get; set; }
        public string zkimliklendiren { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }

    }
}