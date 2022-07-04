using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblbildirimarsiv")]
    public class tblbildirimarsiv : TabloObject
    {
        public tblbildirimarsiv(Session session) : base(session) { }
           
        string _bildirimturu = "";
        [Persistent("bildirimturu")]
        [Size(150)]
        public string bildirimturu
        {
            get { return _bildirimturu; }
            set { SetPropertyValue<string>("bildirimturu", ref _bildirimturu, value); }
        }

        string _ip = "";
        [Persistent("ip")]
        [Size(150)]
        public string ip
        {
            get { return _ip; }
            set { SetPropertyValue<string>("ip", ref _ip, value); }
        }
    }
}

