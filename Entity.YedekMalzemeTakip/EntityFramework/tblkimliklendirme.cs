using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblkimliklendirme")]
    public class tblkimliklendirme : TabloObject
    {
        public tblkimliklendirme(Session session) : base(session) { }

        string _kullaniciadi = "";
        [Persistent("kullaniciadi")]
        [Size(150)]
        public string kullaniciadi
        {
            get { return _kullaniciadi; }
            set { SetPropertyValue<string>("kullaniciadi", ref _kullaniciadi, value); }
        }

        string _gelenepc = "";
        [Persistent("gelenepc")]
        [Size(150)]
        public string gelenepc
        {
            get { return _gelenepc; }
            set { SetPropertyValue<string>("gelenepc", ref _gelenepc, value); }
        }

        string _mantnr = "";
        [Persistent("matnr")]
        [Size(250)]
        public string matnr
        {
            get { return _mantnr; }
            set { SetPropertyValue<string>("matnr", ref _mantnr, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(250)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
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
