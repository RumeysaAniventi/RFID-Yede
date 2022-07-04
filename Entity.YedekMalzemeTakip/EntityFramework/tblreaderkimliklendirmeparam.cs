using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblreaderkimliklendirmeparam")]
    public class tblreaderkimliklendirmeparam:TabloObject
    {
        public tblreaderkimliklendirmeparam(Session session) : base(session) { }

        string _readerepc = "";
        [Persistent("readerepc")]
        [Size(150)]
        public string readerepc
        {
            get { return _readerepc; }
            set { SetPropertyValue<string>("readerepc ", ref _readerepc, value); }
        }

        string _readerokumagucu = "";
        [Persistent("readerokumagucu")]
        [Size(150)]
        public string readerokumagucu
        {
            get { return _readerokumagucu; }
            set { SetPropertyValue<string>("readerokumagucu", ref _readerokumagucu, value); }
        }

        string _readerip = "";
        [Persistent("readerip")]
        [Size(150)]
        public string readerip
        {
            get { return _readerip; }
            set { SetPropertyValue<string>("readerip", ref _readerip, value); }
        }





    }
}
