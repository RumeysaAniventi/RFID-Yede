using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{

    [Persistent("tblreaderokuma")]
    public class tblreaderokuma : TabloObject
    {
        public tblreaderokuma(Session session) : base(session) { }

        string _epc = "";
        [Persistent("epc")]
        [Size(150)]
        public string demirbasid
        {
            get { return _epc; }
            set { SetPropertyValue<string>("epc", ref _epc, value); }
        }

 
        private DateTime _okumazamani;
        [Persistent("okumazamani")]
        public DateTime okumazamani
        {
            get { return _okumazamani; }
            set { SetPropertyValue<DateTime>("okumazamani", ref _okumazamani, value); }
        }

    }
}
