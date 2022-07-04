using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblstokeparametreparam")]
    public class tblstokeparametreparam : TabloObject
    {
        public tblstokeparametreparam(Session session) : base(session) { }

        string _mtart = "";
        [Persistent("mtart")]
        [Size(250)]
        public string mtart
        {
            get { return _mtart; }
            set { SetPropertyValue<string>("mtart", ref _mtart, value); }
        }
        string _iwerk = "";
        [Persistent("iwerk")]
        [Size(250)]
        public string iwerk
        {
            get { return _iwerk; }
            set { SetPropertyValue<string>("iwerk", ref _iwerk, value); }
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
