using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblgelenyedekmalzemelog")]
    public class tblgelenyedekmalzemelog : TabloObject
    {
        public tblgelenyedekmalzemelog(Session session) : base(session) { }

        string _ip = "";
        [Persistent("ip")]
        [Size(150)]
        public string ip
        {
            get { return _ip; }
            set { SetPropertyValue<string>("ip", ref _ip, value); }
        }

        string _hataciklama = "";
        [Persistent("hataciklama")]
        [Size(4000)]
        public string hataciklama
        {
            get { return _hataciklama; }
            set { SetPropertyValue<string>("hataciklama", ref _hataciklama, value); }
        }

        string _aciklama = "";
        [Persistent("aciklama")]
        [Size(150)]
        public string aciklama
        {
            get { return _aciklama; }
            set { SetPropertyValue<string>("aciklama", ref _aciklama, value); }
        }

        int _sonuc;
        [Persistent("sonuc")]
        public int sonuc
        {
            get { return _sonuc; }
            set { SetPropertyValue<int>("sonuc", ref _sonuc, value); }
        }
    }
}
