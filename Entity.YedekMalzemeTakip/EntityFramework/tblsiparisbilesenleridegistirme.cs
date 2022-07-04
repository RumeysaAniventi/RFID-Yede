using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblsiparisbilesenleridegistirme")]
    public class tblsiparisbilesenleridegistirme : TabloObject
    {
        public tblsiparisbilesenleridegistirme(Session session) : base(session) { }


      

        string _rsnum = "";
        [Persistent("rsnum")]
        [Size(255)]
        public string rsnum
        {
            get { return _rsnum; }
            set { SetPropertyValue<string>("rsnum", ref _rsnum, value); }
        }


        string _rspos = "";
        [Persistent("rspos")]
        [Size(255)]
        public string rspos
        {
            get { return _rspos; }
            set { SetPropertyValue<string>("rspos", ref _rspos, value); }
        }


        string _posnr = "";
        [Persistent("posnr")]
        [Size(255)]
        public string posnr
        {
            get { return _posnr; }
            set { SetPropertyValue<string>("posnr", ref _posnr, value); }
        }

        string _vornr = "";
        [Persistent("vornr")]
        [Size(255)]
        public string vornr
        {
            get { return _vornr; }
            set { SetPropertyValue<string>("vornr", ref _vornr, value); }
        }

        string _matnr = "";
        [Persistent("matnr")]
        [Size(255)]
        public string matnr
        {
            get { return _matnr; }
            set { SetPropertyValue<string>("matnr", ref _matnr, value); }
        }

       
        string _werks = "";
        [Persistent("werks")]
        [Size(255)]
        public string werks
        {
            get { return _werks; }
            set { SetPropertyValue<string>("werks", ref _werks, value); }
        }

        string _lgort = "";
        [Persistent("lgort")]
        [Size(255)]
        public string lgort
        {
            get { return _lgort; }
            set { SetPropertyValue<string>("lgort", ref _lgort, value); }
        }

        string _postp = "";
        [Persistent("postp")]
        [Size(255)]
        public string postp
        {
            get { return _postp; }
            set { SetPropertyValue<string>("postp", ref _postp, value); }
        }

        string _bdmng = "";
        [Persistent("bdmng")]
        [Size(255)]
        public string bdmng
        {
            get { return _bdmng; }
            set { SetPropertyValue<string>("bdmng", ref _bdmng, value); }
        }

        string _meins = "";
        [Persistent("meins")]
        [Size(255)]
        public string meins
        {
            get { return _meins; }
            set { SetPropertyValue<string>("meins", ref _meins, value); }
        }

        string _wempf = "";
        [Persistent("wempf")]
        [Size(255)]
        public string wempf
        {
            get { return _wempf; }
            set { SetPropertyValue<string>("wempf", ref _wempf, value); }
        }

        string _ablad = "";
        [Persistent("ablad")]
        [Size(255)]
        public string ablad
        {
            get { return _ablad; }
            set { SetPropertyValue<string>("ablad", ref _ablad, value); }
        }

        string _rgekz = "";
        [Persistent("rgekz")]
        [Size(255)]
        public string rgekz
        {
            get { return _rgekz; }
            set { SetPropertyValue<string>("rgekz", ref _rgekz, value); }
        }

        string _indc = "";
        [Persistent("indc")]
        [Size(255)]
        public string indc
        {
            get { return _indc; }
            set { SetPropertyValue<string>("indc", ref _indc, value); }
        }
    }
}
