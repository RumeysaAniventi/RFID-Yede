using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
   
    [Persistent("tbldepodemirbas")]
    public class tbldepodemirbas : TabloObject
    {
        public tbldepodemirbas(Session session) : base(session) { }

        string _depoid = "";
        [Persistent("depoid")]
        [Size(150)]
        public string depoid
        {
            get { return _depoid; }
            set { SetPropertyValue<string>("depoid", ref _depoid, value); }
        }

        string _demirbasid = "";
        [Persistent("demirbasid")]
        [Size(150)]
        public string demirbasid
        {
            get { return _depoid; }
            set { SetPropertyValue<string>("demirbasid", ref _demirbasid, value); }
        }

    }
}
