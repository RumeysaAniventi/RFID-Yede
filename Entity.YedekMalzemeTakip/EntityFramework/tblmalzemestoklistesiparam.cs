using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblmalzemestoklistesiparam")]
    public class tblmalzemestoklistesiparam : TabloObject
    {
        public tblmalzemestoklistesiparam(Session session) : base(session) { }

        string _werks = "";
        [Persistent("werks")]
        [Size(250)]
        public string werks
        {
            get { return _werks; }
            set { SetPropertyValue<string>("werks", ref _werks, value); }
        }

        string _mtart = "";
        [Persistent("mtart")]
        [Size(250)]
        public string mtart
        {
            get { return _mtart; }
            set { SetPropertyValue<string>("mtart", ref _mtart, value); }
        }

        string _lgort = "";
        [Persistent("lgort")]
        [Size(250)]
        public string lgort
        {
            get { return _lgort; }
            set { SetPropertyValue<string>("lgort", ref _lgort, value); }
        }
    }
}
