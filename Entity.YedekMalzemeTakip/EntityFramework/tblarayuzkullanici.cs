using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblarayuzkullanici")]
    public class tblarayuzkullanici : TabloObject
    {
        public tblarayuzkullanici(Session session) : base(session) { }

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

        string _yetki = "";
        [Persistent("yetki")]
        [Size(150)]
        public string yetki
        {
            get { return _yetki; }
            set { SetPropertyValue<string>("yetki", ref _yetki, value); }
        }
        string _soyadi = "";
        [Persistent("soyadi")]
        [Size(150)]
        public string soyadi
        {
            get { return _soyadi; }
            set { SetPropertyValue<string>("soyadi", ref _soyadi, value); }
        }

        string _adi = "";
        [Persistent("adi")]
        [Size(150)]
        public string adi
        {
            get { return _adi; }
            set { SetPropertyValue<string>("adi", ref _adi, value); }
        }

    }
}
