using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblreaderkimliklendirme")]
    public class tblreaderkimliklendirme : TabloObject
    {
        public tblreaderkimliklendirme(Session session) : base(session) { }

        string _gelenepc = "";
        [Persistent("gelenepc")]
        [Size(150)]
        public string gelenepc
        {
            get { return _gelenepc; }
            set { SetPropertyValue<string>("gelenepc", ref _gelenepc, value); }
        }

        private DateTime _ilkokuma;
        [Persistent("ilkokuma")]
        public DateTime ilkokuma
        {
            get { return _ilkokuma; }
            set { SetPropertyValue<DateTime>("ilkokuma", ref _ilkokuma, value); }
        }

        private DateTime _sonokuma;
        [Persistent("sonokuma")]
        public DateTime sonokuma
        {
            get { return _sonokuma; }
            set { SetPropertyValue<DateTime>("sonokuma", ref _sonokuma, value); }
        }


    }
}
