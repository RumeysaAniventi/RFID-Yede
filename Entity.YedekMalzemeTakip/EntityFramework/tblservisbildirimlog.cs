using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblservisbildirimlog")]
    public class tblservisbildirimlog : TabloObject
    {
        public tblservisbildirimlog(Session session) : base(session) { }

        int _mailatildi = 0;
        [Persistent("mailatildi")]
        public int mailatildi
        {
            get { return _mailatildi; }
            set { SetPropertyValue<int>("mailatildi", ref _mailatildi, value); }
        }

        string _servisadi = "";
        [Persistent("servisadi")]
        [Size(450)]
        public string servisadi
        {
            get { return _servisadi; }
            set { SetPropertyValue<string>("servisadi", ref _servisadi, value); }
        }

        string _aciklama01 = "";
        [Persistent("aciklama01")]
        [Size(450)]
        public string aciklama01
        {
            get { return _aciklama01; }
            set { SetPropertyValue<string>("aciklama01", ref _aciklama01, value); }
        }

        string _aciklama02 = "";
        [Persistent("aciklama02")]
        [Size(450)]
        public string aciklama02
        {
            get { return _aciklama02; }
            set { SetPropertyValue<string>("aciklama02", ref _aciklama02, value); }
        }

        string _aciklama03 = "";
        [Persistent("aciklama03")]
        [Size(450)]
        public string aciklama03
        {
            get { return _aciklama03; }
            set { SetPropertyValue<string>("aciklama03", ref _aciklama03, value); }
        }

    }
}
