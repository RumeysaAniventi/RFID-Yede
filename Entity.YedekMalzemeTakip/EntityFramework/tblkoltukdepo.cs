using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{


    [Persistent("tblkoltukdepo")]
    public class tblkoltukdepo : TabloObject
    {
        public tblkoltukdepo(Session session) : base(session) { }


        string _bakimsiparisid = "";
        [Persistent("bakimsiparisid")]
        [Size(150)]
        public string bakimsiparisid
        {
            get { return _bakimsiparisid; }
            set { SetPropertyValue<string>("bakimsiparisid", ref _bakimsiparisid, value); }
        }

        string _demirbasid = "";
        [Persistent("demirbasid")]
        [Size(150)]
        public string demirbasid
        {
            get { return _demirbasid; }
            set { SetPropertyValue<string>("demirbasid", ref _demirbasid, value); }
        }



    }


}
