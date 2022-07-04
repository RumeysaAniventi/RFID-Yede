using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblsistemkullanici")]
    public class tblsistemkullanici : TabloObject
    {
        public tblsistemkullanici(Session session) : base(session) { }

        string _kullaniciadi = "";
        [Persistent("kullaniciadi")]
        [Size(150)]
        public string kullaniciadi
        {
            get { return _kullaniciadi; }
            set { SetPropertyValue<string>("kullaniciadi", ref _kullaniciadi, value); }
        }

        string _sifre = "";
        [Persistent("sifre")]
        [Size(150)]
        public string sifre
        {
            get { return _sifre; }
            set { SetPropertyValue<string>("sifre", ref _sifre, value); }
        }
    }
}
