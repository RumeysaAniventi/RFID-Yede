using System.Collections.Generic;

namespace YedekMalzeme.Arayuz.response
{
    public class KimlikSeriNoListeResponse : ResponseOrtak
    {
        public List<ViewSeriNo> zDizi { get; set; }
    }

    public class ViewSeriNo
    {
        public string zsernrkod { get; set; }
        public string zsernrad { get; set; }
    }
}