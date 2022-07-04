using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
 
    [Persistent("tblrfiddemirbas")]
    public class tblrfiddemirbas : TabloObject
    {
        public tblrfiddemirbas(Session session) : base(session) { }

        string _urunid = "";
        [Persistent("urunid")]
        [Size(150)]
        public string urunid
        {
            get { return _urunid; }
            set { SetPropertyValue<string>("urunid", ref _urunid, value); }
        }



        string _rfid = "";
        [Persistent("rfid")]
        [Size(150)]
        public string rfid
        {
            get { return _rfid; }
            set { SetPropertyValue<string>("rfid", ref _rfid, value); }
        }

    }
}
