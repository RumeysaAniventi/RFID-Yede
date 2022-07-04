using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;

namespace Entity.YedekMalzemeTakip.EntityFramework
{

    [Persistent("tblbildirim")]
    public class tblbildirim : TabloObject
    {
        public tblbildirim(Session session) : base(session) { }

        string _bildirimturu = "";
        [Persistent("bildirimturu")]
        [Size(150)]
        public string bildirimturu
        {
            get { return _bildirimturu; }
            set { SetPropertyValue<string>("bildirimturu", ref _bildirimturu, value); }
        }

        private DateTime _bildirimzamani;
        [Persistent("bildirimzamani")]
        public DateTime bildirimzamani
        {
            get { return _bildirimzamani; }
            set { SetPropertyValue<DateTime>("bildirimzamani", ref _bildirimzamani, value); }
        }

    }

}
