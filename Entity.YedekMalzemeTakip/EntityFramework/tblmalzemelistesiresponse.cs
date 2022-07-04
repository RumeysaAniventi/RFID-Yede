using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;


namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblmalzemelistesiresponse")]
    public class tblmalzemelistesiresponse : TabloObject
    {

        public tblmalzemelistesiresponse(Session session) : base(session) { }


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

    }
}
