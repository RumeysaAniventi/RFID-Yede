using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblmalzemebelgelistesiresponse")]
    public class tblmalzemebelgelistesiresponse : TabloObject
    {
        public tblmalzemebelgelistesiresponse(Session session) : base(session) { }


        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(250)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _mblnr = "";
        [Persistent("mblnr")]
        [Size(250)]
        public string mblnr
        {
            get { return _mblnr; }
            set { SetPropertyValue<string>("mblnr", ref _mblnr, value); }
        }

        string _mjahr = "";
        [Persistent("mjahr")]
        [Size(250)]
        public string mjahr
        {
            get { return _mjahr; }
            set { SetPropertyValue<string>("mjahr", ref _mjahr, value); }
        }

        string _zeile = "";
        [Persistent("zeile")]
        [Size(250)]
        public string zeile
        {
            get { return _zeile; }
            set { SetPropertyValue<string>("zeile", ref _zeile, value); }
        }

        string _mantnr = "";
        [Persistent("matnr")]
        [Size(250)]
        public string matnr
        {
            get { return _mantnr; }
            set { SetPropertyValue<string>("matnr", ref _mantnr, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(250)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
        }

        string _sernr = "";
        [Persistent("sernr")]
        [Size(250)]
        public string sernr
        {
            get { return _sernr; }
            set { SetPropertyValue<string>("sernr", ref _sernr, value); }
        }

        string _lgort = "";
        [Persistent("lgort")]
        [Size(250)]
        public string lgort
        {
            get { return _lgort; }
            set { SetPropertyValue<string>("lgort", ref _lgort, value); }
        }

        string _menge = "";
        [Persistent("menge")]
        [Size(250)]
        public string menge
        {
            get { return _menge; }
            set { SetPropertyValue<string>("menge", ref _menge, value); }
        }

        string _meins = "";
        [Persistent("meins")]
        [Size(250)]
        public string meins
        {
            get { return _meins; }
            set { SetPropertyValue<string>("meins", ref _meins, value); }
        }

        string _bwart = "";
        [Persistent("bwart")]
        [Size(250)]
        public string bwart
        {
            get { return _bwart; }
            set { SetPropertyValue<string>("bwart", ref _bwart, value); }
        }

        string _budat = "";
        [Persistent("budat")]
        [Size(250)]
        public string budat
        {
            get { return _budat; }
            set { SetPropertyValue<string>("budat", ref _budat, value); }
        }


        string _vornr = "";
        [Persistent("vornr")]
        [Size(250)]
        public string vornr
        {
            get { return _vornr; }
            set { SetPropertyValue<string>("vornr", ref _vornr, value); }
        }


        string _mblnr2 = "";
        [Persistent("mblnr2")]
        [Size(250)]
        public string mblnr2
        {
            get { return _mblnr2; }
            set { SetPropertyValue<string>("mblnr2", ref _mblnr2, value); }
        }

        string _zeile2 = "";
        [Persistent("zeile2")]
        [Size(250)]
        public string zeile2
        {
            get { return _zeile2; }
            set { SetPropertyValue<string>("zeile2", ref _zeile2, value); }
        }



        string _mjahr2 = "";
        [Persistent("mjahr2")]
        [Size(250)]
        public string mjahr2
        {
            get { return _mjahr2; }
            set { SetPropertyValue<string>("mjahr2", ref _mjahr2, value); }
        }


    }
}
