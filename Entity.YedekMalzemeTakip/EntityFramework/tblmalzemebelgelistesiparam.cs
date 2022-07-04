using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblmalzemebelgelistesiparam")]
    public class tblmalzemebelgelistesiparam : TabloObject
    {
        public tblmalzemebelgelistesiparam(Session session) : base(session) { }

        string _budatlow = "";
        [Persistent("budatlow")]
        [Size(150)]
        public string budatlow
        {
            get { return _budatlow; }
            set { SetPropertyValue<string>("budatlow", ref _budatlow, value); }
        }

        string _budathigh = "";
        [Persistent("budathigh")]
        [Size(150)]
        public string budathigh
        {
            get { return _budathigh; }
            set { SetPropertyValue<string>("budathigh", ref _budathigh, value); }
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
    }
}
