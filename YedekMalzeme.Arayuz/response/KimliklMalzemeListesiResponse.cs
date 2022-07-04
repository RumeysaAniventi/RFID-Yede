using System.Collections.Generic;

namespace YedekMalzeme.Arayuz.response
{
    public class KimliklMalzemeListesiResponse : ResponseOrtak
    {
        public List<ViewMalzeme> zDizi { get; set; }
    }

    public class ViewMalzeme
    {
        public string zmaktx { get; set; }
        public string zmatnr { get; set; }
    }
}