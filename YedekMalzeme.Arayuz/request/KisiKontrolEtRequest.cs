using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.request
{
    public class KisiKontrolEtRequest:OrtakRequest
    {
        public string zad { get; set; }
        public string zsoyad { get; set; }
        public string zkullanici { get; set; }
        public string zsifre { get; set; }
       

    }
}