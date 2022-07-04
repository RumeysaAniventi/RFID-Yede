using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblarsivmalzemeserialstoklistesi")]
    public class tblarsivmalzemeserialstoklistesi:TabloObject
    {
        public tblarsivmalzemeserialstoklistesi(Session session) : base(session) { }

        string _degisenalan = "";
        [Persistent("degisenalan")]
        [Size(250)]
        public string degisenalan
        {
            get { return _mantnr; }
            set { SetPropertyValue<string>("degisenalan", ref _degisenalan, value); }
        }

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

        string _sernr = "";
        [Persistent("sernr")]
        [Size(250)]
        public string sernr
        {
            get { return _sernr; }
            set { SetPropertyValue<string>("sernr", ref _sernr, value); }
        }

        string _bwerk = "";
        [Persistent("bwerk")]
        [Size(250)]
        public string bwerk
        {
            get { return _bwerk; }
            set { SetPropertyValue<string>("bwerk", ref _bwerk, value); }
        }

        string _blager = "";
        [Persistent("blager")]
        [Size(250)]
        public string blager
        {
            get { return _blager; }
            set { SetPropertyValue<string>("blager", ref _blager, value); }
        }

        string _aciklama = "";
        [Persistent("aciklama")]
        [Size(450)]
        public string aciklama
        {
            get { return _aciklama; }
            set { SetPropertyValue<string>("aciklama", ref _aciklama, value); }
        }
    }
}
