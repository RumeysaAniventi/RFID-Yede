using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tbldepo")]
    public class tbldepo : TabloObject
    {
        public tbldepo(Session session) : base(session) { }

        string _ad = "";
        [Persistent("ad")]
        [Size(150)]
        public string ad
        {
            get { return _ad; }
            set { SetPropertyValue<string>("ad", ref _ad, value); }
        }

    }
}
