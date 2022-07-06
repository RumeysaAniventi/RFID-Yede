using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tbl06analiz")]
    public class tbl06analiz: TabloObject
    {
        public tbl06analiz(Session session) : base(session) { }


        string _epc = "";
        [Persistent("epc")]
        [Size(100)]
        public string epc
        {
            get { return _epc; }
            set { SetPropertyValue<string>("aufnr", ref _epc, value); }
        }


        string _sernr = "";
        [Persistent("sernr")]
        [Size(100)]
        public string sernr
        {
            get { return _sernr; }
            set { SetPropertyValue<string>("sernr", ref _sernr, value); }
        }


        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(100)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _matnr = "";
        [Persistent("matnr")]
        [Size(100)]
        public string matnr
        {
            get { return _matnr; }
            set { SetPropertyValue<string>("matnr", ref _matnr, value); }
        }

        string _maktx = "";
        [Persistent("maktx")]
        [Size(100)]
        public string maktx
        {
            get { return _maktx; }
            set { SetPropertyValue<string>("maktx", ref _maktx, value); }
        }

        string _iliskilendiren = "";
        [Persistent("iliskilendiren")]
        [Size(100)]
        public string iliskilendiren
        {
            get { return _iliskilendiren; }
            set { SetPropertyValue<string>("iliskilendiren", ref _iliskilendiren, value); }
        }


        string _iliskiiptaleden = "";
        [Persistent("iliskiiptaleden")]
        [Size(100)]
        public string iliskiiptaleden
        {
            get { return _iliskiiptaleden; }
            set { SetPropertyValue<string>("iliskiiptaleden", ref _iliskiiptaleden, value); }
        }


        int _iliskiyeri =0;
        [Persistent("iliskiyeri")]
        [Size(100)]
        public int iliskiyeri
        {
            get { return _iliskiyeri; }
            set { SetPropertyValue<int>("iliskiyeri", ref _iliskiyeri, value); }
        }

        string _kimliklendiren = "";
        [Persistent("kimliklendiren")]
        [Size(100)]
        public string kimliklendiren
        {
            get { return _kimliklendiren; }
            set { SetPropertyValue<string>("kimliklendiren", ref _kimliklendiren, value); }
        }

        string _kimlikiptaleden = "";
        [Persistent("kimlikiptaleden")]
        [Size(100)]
        public string kimlikiptaleden
        {
            get { return _kimlikiptaleden; }
            set { SetPropertyValue<string>("kimlikiptaleden", ref _kimlikiptaleden, value); }
        }

        int _tuketim = 0;
        [Persistent("tuketim")]
        [Size(100)]
        public int tuketim
        {
            get { return _tuketim; }
            set { SetPropertyValue<int>("tuketim", ref _tuketim, value); }
        }

        int _gecisizni = 0;
        [Persistent("gecisizni")]
        [Size(100)]
        public int gecisizni
        {
            get { return _gecisizni; }
            set { SetPropertyValue<int>("gecisizni", ref _gecisizni, value); }
        }

        DateTime _okumabaslangic ;
        [Persistent("okumabaslangic")]
        [Size(100)]
        public DateTime okumabaslangic
        {
            get { return _okumabaslangic; }
            set { SetPropertyValue<DateTime>("okumabaslangic", ref _okumabaslangic, value); }
        }



        DateTime _okumabitis ;
        [Persistent("okumabitis")]
        [Size(100)]
        public DateTime okumabitis
        {
            get { return _okumabitis; }
            set { SetPropertyValue<DateTime>("okumabitis", ref _okumabitis, value); }
        }


        int _alarmdurum = 0;
        [Persistent("alarmdurum")]
        [Size(100)]
        public int alarmdurum
        {
            get { return _alarmdurum; }
            set { SetPropertyValue<int>("alarmdurum", ref _alarmdurum, value); }
        }

        DateTime _alarmkapatmatarih ;
        [Persistent("alarmkapatmatarih")]
        [Size(100)]
        public DateTime alarmkapatmatarih
        {
            get { return _alarmkapatmatarih; }
            set { SetPropertyValue<DateTime>("alarmkapatmatarih", ref _alarmkapatmatarih, value); }
        }

        string _alarmkapatmaaciklama = "";
        [Persistent("alarmkapatmaaciklama")]
        [Size(500)]
        public string alarmkapatmaaciklama
        {
            get { return _alarmkapatmaaciklama; }
            set { SetPropertyValue<string>("alarmkapatmaaciklama", ref _alarmkapatmaaciklama, value); }
        }

        int _kapireader = 0;
        [Persistent("kapireader")]
        [Size(100)]
        public int kapireader
        {
            get { return _kapireader; }
            set { SetPropertyValue<int>("kapireader", ref _kapireader, value); }
        }


    }
}
