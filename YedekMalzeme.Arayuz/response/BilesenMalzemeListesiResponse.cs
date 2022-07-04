using System.Collections.Generic;

namespace YedekMalzeme.Arayuz.response
{
    public class BilesenMalzemeListesiResponse:ResponseOrtak
    {
        public List<BilesenMalzemeListesiView> zDizi { get; set; }
    }

    public class BilesenMalzemeListesiView
    {
        public string zkullanici { get; set; }
        public string zmatnr { get; set; }
        public string zmaktx { get; set; }
        public string zsernr { get; set; }
    }
}