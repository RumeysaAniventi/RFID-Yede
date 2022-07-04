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

        int _gecisturu = 0;
        [Persistent("gecisturu")]
        public int gecisturu
        {
            get { return _gecisturu; }
            set { SetPropertyValue<int>("gecisturu", ref _gecisturu, value); }
        }

        int _alarm = 0;
        [Persistent("alarm")]
        public int alarm
        {
            get { return _alarm; }
            set { SetPropertyValue<int>("alarm", ref _alarm, value); }
        }

        DateTime _alarmkapatmatarihi;
        [Persistent("alarmkapatmatarihi")]
        public DateTime alarmkapatmatarihi
        {
            get { return _alarmkapatmatarihi; }
            set { SetPropertyValue<DateTime>("alarmkapatmatarihi", ref _alarmkapatmatarihi, value); }
        }

        string _alarmkapatankullanici;
        [Persistent("alarmkapatankullanici")]
        [Size(150)]
        public string alarmkapatankullanici
        {
            get { return _alarmkapatankullanici; }
            set { SetPropertyValue<string>("alarmkapatankullanici", ref _alarmkapatankullanici, value); }
        }

        string _alarmkapatmaaciklama;
        [Persistent("alarmkapatmaaciklama")]
        [Size(150)]
        public string alarmkapatmaaciklama
        {
            get { return _alarmkapatmaaciklama; }
            set { SetPropertyValue<string>("alarmkapatmaaciklama", ref _alarmkapatmaaciklama, value); }
        }

    }
}
