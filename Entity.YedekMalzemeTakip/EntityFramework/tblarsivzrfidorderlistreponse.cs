using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblarsivzrfidorderlistreponse")]
    public class tblarsivzrfidorderlistreponse : TabloObject
    {
        public tblarsivzrfidorderlistreponse(Session session) : base(session) { }

        string _degisenalan = "";
        [Persistent("degisenalan")]
        [Size(500)]
        public string degisenalan
        {
            get { return _degisenalan; }
            set { SetPropertyValue<string>("degisenalan", ref _degisenalan, value); }
        }

        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(500)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _posnr = "";
        [Persistent("posnr")]
        [Size(500)]
        public string posnr
        {
            get { return _posnr; }
            set { SetPropertyValue<string>("posnr", ref _posnr, value); }
        }

        string _matnr = "";
        [Persistent("matnr")]
        [Size(500)]
        public string matnr
        {
            get { return _matnr; }
            set { SetPropertyValue<string>("matnr", ref _matnr, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(500)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
        }

        string _bdmng = "";
        [Persistent("bdmng")]
        [Size(500)]
        public string bdmng
        {
            get { return _bdmng; }
            set { SetPropertyValue<string>("bdmng", ref _bdmng, value); }
        }

        string _enmng = "";
        [Persistent("enmng")]
        [Size(500)]
        public string enmng
        {
            get { return _enmng; }
            set { SetPropertyValue<string>("enmng", ref _enmng, value); }
        }

        string _kzear = "";
        [Persistent("kzear")]
        [Size(500)]
        public string kzear
        {
            get { return _kzear; }
            set { SetPropertyValue<string>("kzear", ref _kzear, value); }
        }

        string _meins = "";
        [Persistent("meins")]
        [Size(500)]
        public string meins
        {
            get { return _meins; }
            set { SetPropertyValue<string>("meins", ref _meins, value); }
        }

        string _werks = "";
        [Persistent("werks")]
        [Size(500)]
        public string werks
        {
            get { return _werks; }
            set { SetPropertyValue<string>("werks", ref _werks, value); }
        }

        string _lgort = "";
        [Persistent("lgort")]
        [Size(500)]
        public string lgort
        {
            get { return _lgort; }
            set { SetPropertyValue<string>("lgort", ref _lgort, value); }
        }

        string _vornr = "";
        [Persistent("vornr")]
        [Size(500)]
        public string vornr
        {
            get { return _vornr; }
            set { SetPropertyValue<string>("vornr", ref _vornr, value); }
        }

        string _erdat = "";
        [Persistent("erdat")]
        [Size(500)]
        public string erdat
        {
            get { return _erdat; }
            set { SetPropertyValue<string>("erdat", ref _erdat, value); }
        }

        string _rsnum = "";
        [Persistent("rsnum")]
        [Size(500)]
        public string rsnum
        {
            get { return _rsnum; }
            set { SetPropertyValue<string>("rsnum", ref _rsnum, value); }
        }

        string _rspos = "";
        [Persistent("rspos")]
        [Size(500)]
        public string rspos
        {
            get { return _rspos; }
            set { SetPropertyValue<string>("rspos", ref _rspos, value); }
        }
    }
}
