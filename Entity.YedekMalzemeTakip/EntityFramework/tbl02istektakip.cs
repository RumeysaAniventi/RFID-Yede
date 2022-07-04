using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tbl02istektakip")]
    public class tbl02istektakip : TabloObject
    {
        public tbl02istektakip(Session session) : base(session) { }

        string _islemkodu = "";
        [Persistent("islemkodu")]
        [Size(500)]
        public string islemkodu
        {
            get { return _islemkodu; }
            set { SetPropertyValue<string>("islemkodu", ref _islemkodu, value); }
        }

        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(500)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _mantnr = "";
        [Persistent("matnr")]
        [Size(250)]
        public string matnr
        {
            get { return _mantnr; }
            set { SetPropertyValue<string>("matnr", ref _mantnr, value); }
        }

        string _kullanici = "";
        [Persistent("kullanici")]
        [Size(250)]
        public string kullanici
        {
            get { return _kullanici; }
            set { SetPropertyValue<string>("kullanici", ref _kullanici, value); }
        }

        string _islemturu = "";
        [Persistent("islemturu")]
        [Size(250)]
        public string islemturu
        {
            get { return _islemturu; }
            set { SetPropertyValue<string>("islemturu", ref _islemturu, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(250)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
        }

        string _gelenepc = "";
        [Persistent("gelenepc")]
        [Size(150)]
        public string gelenepc
        {
            get { return _gelenepc; }
            set { SetPropertyValue<string>("gelenepc", ref _gelenepc, value); }
        }

        string _sernr = "";
        [Persistent("sernr")]
        [Size(250)]
        public string sernr
        {
            get { return _sernr; }
            set { SetPropertyValue<string>("sernr", ref _sernr, value); }
        }
    }
}
