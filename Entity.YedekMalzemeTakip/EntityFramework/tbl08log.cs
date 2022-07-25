using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tbl08log")]
    public class tbl08log: TabloObject
    {
        public tbl08log(Session session) : base(session) { }

        string _islemyapan = "";
        [Persistent("islemyapan")]
        [Size(100)]
        public string islemyapan
        {
            get { return _islemyapan; }
            set { SetPropertyValue<string>("islemyapan", ref _islemyapan, value); }
        }

        string _islemturu = "";
        [Persistent("islemturu")]
        [Size(100)]
        public string islemturu
        {
            get { return _islemturu; }
            set { SetPropertyValue<string>("islemturu", ref _islemturu, value); }
        }


        string _tabloadi = "";
        [Persistent("tabloadi")]
        [Size(100)]
        public string tabloadi
        {
            get { return _tabloadi; }
            set { SetPropertyValue<string>("tabloadi", ref _tabloadi, value); }
        }

        string _satirid = "";
        [Persistent("satirid")]
        [Size(100)]
        public string satirid
        {
            get { return _satirid; }
            set { SetPropertyValue<string>("satirid", ref _satirid, value); }
        }

        string _epc = "";
        [Persistent("epc")]
        [Size(100)]
        public string epc
        {
            get { return _epc; }
            set { SetPropertyValue<string>("epc", ref _epc, value); }
        }

        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(100)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _sernr = "";
        [Persistent("sernr")]
        [Size(100)]
        public string sernr
        {
            get { return _sernr; }
            set { SetPropertyValue<string>("sernr", ref _sernr, value); }
        }

        string _matnr = "";
        [Persistent("matnr")]
        [Size(100)]
        public string matnr
        {
            get { return _matnr; }
            set { SetPropertyValue<string>("matnr", ref _matnr, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(100)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
        }
    }
}
