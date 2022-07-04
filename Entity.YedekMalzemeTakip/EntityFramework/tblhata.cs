using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblhata")]
    public class tblhata : TabloObject
    {
        public tblhata(Session session) : base(session) { }

        string _hatakodu = "";
        [Persistent("hatakodu")]
        [Size(1500)]
        public string hatakodu
        {
            get { return _hatakodu; }
            set { SetPropertyValue<string>("hatakodu", ref _hatakodu, value); }
        }

        string _ipadresi = "";
        [Persistent("ipadresi")]
        [Size(150)]
        public string ipadresi
        {
            get { return _ipadresi; }
            set { SetPropertyValue<string>("ipadresi", ref _ipadresi, value); }
        }
    }
}
