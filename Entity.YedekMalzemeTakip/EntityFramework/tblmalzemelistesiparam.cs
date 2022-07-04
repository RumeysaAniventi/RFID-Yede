using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblmalzemelistesiparam")]
    public class tblmalzemelistesiparam : TabloObject
    {
        public tblmalzemelistesiparam(Session session) : base(session) { }

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
    }
}
