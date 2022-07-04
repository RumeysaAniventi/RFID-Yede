using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblmalzemestoklistesiresponse")]
    public class tblmalzemestoklistesiresponse : TabloObject
    {

        public tblmalzemestoklistesiresponse(Session session) : base(session) { }

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


        //tblmalzemelistesi _maktx;
        //[Persistent("maktx")]
        //[Size(250)]
        //public tblmalzemelistesi maktx
        //{
        //    get { return _maktx; }
        //    set { SetPropertyValue<tblmalzemelistesi>("maktx", ref _maktx, value); }
        //}


        string _meins = "";
        [Persistent("meins")]
        [Size(250)]
        public string meins
        {
            get { return _meins; }
            set { SetPropertyValue<string>("meins", ref _meins, value); }
        }

        string _mtart = "";
        [Persistent("mtart")]
        [Size(250)]
        public string mtart
        {
            get { return _mtart; }
            set { SetPropertyValue<string>("mtart", ref _mtart, value); }
        }

        string _mtbez = "";
        [Persistent("mtbez")]
        [Size(250)]
        public string mtbez
        {
            get { return _mtbez; }
            set { SetPropertyValue<string>("mtbez", ref _mtbez, value); }
        }

        string _werks = "";
        [Persistent("werks")]
        [Size(250)]
        public string werks
        {
            get { return _werks; }
            set { SetPropertyValue<string>("werks", ref _werks, value); }
        }

        string _lgort = "";
        [Persistent("lgort")]
        [Size(250)]
        public string lgort
        {
            get { return _lgort; }
            set { SetPropertyValue<string>("lgort", ref _lgort, value); }
        }

        string _labst = "";
        [Persistent("labst")]
        [Size(250)]
        public string labst
        {
            get { return _labst; }
            set { SetPropertyValue<string>("labst", ref _labst, value); }
        }
        string _speme = "";
        [Persistent("speme")]
        [Size(250)]
        public string speme
        {
            get { return _speme; }
            set { SetPropertyValue<string>("speme", ref _speme, value); }
        }
        string _lgpbe = "";
        [Persistent("lgpbe")]
        [Size(250)]
        public string lgpbe
        {
            get { return _lgpbe; }
            set { SetPropertyValue<string>("lgpbe", ref _lgpbe, value); }
        }

    }
}
