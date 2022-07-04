using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblgelenyedekmalzeme")]
    public class tblgelenyedekmalzeme : TabloObject
    {
        public tblgelenyedekmalzeme(Session session) : base(session) { }

        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(350)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _mblnr = "";
        [Persistent("mblnr")]
        [Size(350)]
        public string mblnr
        {
            get { return _mblnr; }
            set { SetPropertyValue<string>("mblnr", ref _mblnr, value); }
        }

        string _mjahr = "";
        [Persistent("mjahr")]
        [Size(350)]
        public string mjahr
        {
            get { return _mjahr; }
            set { SetPropertyValue<string>("mjahr", ref _mjahr, value); }
        }

        string _zeile = "";
        [Persistent("zeile")]
        [Size(350)]
        public string zeile
        {
            get { return _zeile; }
            set { SetPropertyValue<string>("mjahr", ref _zeile, value); }
        }

        string _matnr = "";
        [Persistent("matnr")]
        [Size(350)]
        public string matnr
        {
            get { return _matnr; }
            set { SetPropertyValue<string>("matnr", ref _matnr, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(350)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
        }

        string _sernr = "";
        [Persistent("sernr")]
        [Size(350)]
        public string sernr
        {
            get { return _sernr; }
            set { SetPropertyValue<string>("sernr", ref _sernr, value); }
        }

        string _lgort = "";
        [Persistent("lgort")]
        [Size(350)]
        public string lgort
        {
            get { return _lgort; }
            set { SetPropertyValue<string>("lgort", ref _lgort, value); }
        }

        string _menge = "";
        [Persistent("menge")]
        [Size(350)]
        public string menge
        {
            get { return _menge; }
            set { SetPropertyValue<string>("menge", ref _menge, value); }
        }

        string _meins = "";
        [Persistent("meins")]
        [Size(350)]
        public string meins
        {
            get { return _meins; }
            set { SetPropertyValue<string>("meins", ref _meins, value); }
        }

        string _bwart = "";
        [Persistent("bwart")]
        [Size(350)]
        public string bwart
        {
            get { return _bwart; }
            set { SetPropertyValue<string>("bwart", ref _bwart, value); }
        }

        string _budat = "";
        [Persistent("budat")]
        [Size(350)]
        public string budat
        {
            get { return _budat; }
            set { SetPropertyValue<string>("budat", ref _budat, value); }
        }

        string _vornr = "";
        [Persistent("vornr")]
        [Size(350)]
        public string vornr
        {
            get { return _vornr; }
            set { SetPropertyValue<string>("vornr", ref _vornr, value); }
        }

        string _mblnr2 = "";
        [Persistent("mblnr2")]
        [Size(350)]
        public string mblnr2
        {
            get { return _mblnr2; }
            set { SetPropertyValue<string>("mblnr2", ref _mblnr2, value); }
        }
    }
}
