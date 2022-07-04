using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblzrfidorderlistparam")]
    public class tblzrfidorderlistparam : TabloObject
    {
        public tblzrfidorderlistparam(Session session) : base(session) { }

        string _erdatlow = "";
        [Persistent("erdatlow")]
        [Size(150)]
        public string erdatlow
        {
            get { return _erdatlow; }
            set { SetPropertyValue<string>("erdatlow", ref _erdatlow, value); }
        }

        string _erdathigh = "";
        [Persistent("erdathigh")]
        [Size(150)]
        public string erdathigh
        {
            get { return _erdathigh; }
            set { SetPropertyValue<string>("erdathigh", ref _erdathigh, value); }
        }

        string _iwerk = "";
        [Persistent("iwerk")]
        [Size(150)]
        public string iwerk
        {
            get { return _iwerk; }
            set { SetPropertyValue<string>("iwerk", ref _iwerk, value); }
        }

        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(150)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

        string _beklemesuresi = "";
        [Persistent("beklemesuresi")]
        [Size(150)]
        public string beklemesuresi
        {
            get { return _beklemesuresi; }
            set { SetPropertyValue<string>("beklemesuresi", ref _beklemesuresi, value); }
        }
    }
}
