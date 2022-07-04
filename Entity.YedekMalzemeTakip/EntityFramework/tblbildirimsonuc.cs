using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblbildirimsonuc")]
    public class tblbildirimsonuc : TabloObject
    {
        public tblbildirimsonuc(Session session) : base(session) { }

        string _aciklama = "";
        [Persistent("aciklama")]
        [Size(150)]
        public string aciklama
        {
            get { return _aciklama; }
            set { SetPropertyValue<string>("aciklama", ref _aciklama, value); }
        }

        string _exmessage = "";
        [Persistent("exmessage")]
        [Size(150)]
        public string exmessage
        {
            get { return _exmessage; }
            set { SetPropertyValue<string>("exmessage", ref _exmessage, value); }
        }

        int _hatakodu = 0;
        [Persistent("hatakodu")]
        public int hatakodu
        {
            get { return _hatakodu; }
            set { SetPropertyValue<int>("hatakodu", ref _hatakodu, value); }
        }

    }

}
