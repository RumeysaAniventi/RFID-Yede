using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tbl07sapfark")]
    public class tbl07sapfark: TabloObject
    {
        public tbl07sapfark(Session session) : base(session) { }


        string _aufnr = "";
        [Persistent("aufnr")]
        [Size(100)]
        public string aufnr
        {
            get { return _aufnr; }
            set { SetPropertyValue<string>("aufnr", ref _aufnr, value); }
        }

       
        int _iliskiliSayisi = 0;
        [Persistent("iliskiliSayisi")]
        [Size(100)]
        public int iliskiliSayisi
        {
            get { return _iliskiliSayisi; }
            set { SetPropertyValue<int>("iliskiliSayisi", ref _iliskiliSayisi, value); }
        }

        int _toplamSayi = 0;
        [Persistent("toplamSayi")]
        [Size(100)]
        public int toplamSayi
        {
            get { return _toplamSayi; }
            set { SetPropertyValue<int>("toplamSayi", ref _toplamSayi, value); }
        }

        int _emailgonderimi = 0;
        [Persistent("emailgonderimi")]
        [Size(100)]
        public int emailgonderimi
        {
            get { return _emailgonderimi; }
            set { SetPropertyValue<int>("emailgonderimi", ref _emailgonderimi, value); }
        }
    }
}
