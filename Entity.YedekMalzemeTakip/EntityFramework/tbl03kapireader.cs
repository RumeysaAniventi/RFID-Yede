using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tbl03kapireader")]
    public class tbl03kapireader : TabloObject
    {
        public tbl03kapireader(Session session) : base(session) { }

        string _gelenepc = "";
        [Persistent("gelenepc")]
        [Size(150)]
        public string gelenepc
        {
            get { return _gelenepc; }
            set { SetPropertyValue<string>("gelenepc", ref _gelenepc, value); }
        }

        int _okumabittimi = 0;
        [Persistent("okumabittimi")]
        public int okumabittimi
        {
            get { return _okumabittimi; }
            set { SetPropertyValue<int>("okumabittimi", ref _okumabittimi, value); }
        }

        DateTime _okumabaslama;
        [Persistent("okumabaslama")]
        public DateTime okumabaslama
        {
            get { return _okumabaslama; }
            set { SetPropertyValue<DateTime>("okumabaslama", ref _okumabaslama, value); }
        }

        DateTime _okumabitis;
        [Persistent("okumabitis")]
        public DateTime okumabitis
        {
            get { return _okumabitis; }
            set { SetPropertyValue<DateTime>("okumabitis", ref _okumabitis, value); }
        }
       
        int _aktarim;
        [Persistent("aktarim")]
        [Size(150)]
        public int aktarim
        {
            get { return _aktarim; }
            set { SetPropertyValue<int>("aktarim", ref _aktarim, value); }
        }

       

    }
}
