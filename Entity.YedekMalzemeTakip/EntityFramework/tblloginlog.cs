using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblloginlog")]
    public class tblloginlog : TabloObject
    {
        public tblloginlog(Session session) : base(session) { }

        string _zkullaniciadi = "";
        [Persistent("zkullaniciadi")]
        [Size(150)]
        public string zkullaniciadi
        {
            get { return _zkullaniciadi; }
            set { SetPropertyValue<string>("zkullaniciadi", ref _zkullaniciadi, value); }
        }

        string _zipadresi = "";
        [Persistent("zipadresi")]
        [Size(150)]
        public string zipadresi
        {
            get { return _zipadresi; }
            set { SetPropertyValue<string>("zipadresi", ref _zipadresi, value); }
        }

        int _zsonuc = 0;
        [Persistent("zsonuc")]
        public int zsonuc
        {
            get { return _zsonuc; }
            set { SetPropertyValue<int>("zsonuc", ref _zsonuc, value); }
        }
    }
}
