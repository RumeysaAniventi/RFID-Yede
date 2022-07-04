using DevExpress.Xpo;
using System;

namespace Entity.YedekMalzemeTakip.Important
{
    [NonPersistent]
    public class TabloObject : XPBaseObject
    {
        public TabloObject(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            _aktif = 1;
            //databaseKayitZamani = DateTime.Now;
            // id = Guid.NewGuid().ToString().ToUpper();
        }

        private string _id;
        [Persistent("id")]
        [Key(false)]
        [Size(250)]
        public string id
        {
            get { return _id; }
            set { SetPropertyValue<string>("id", ref _id, value); }
        }

        private string _createuser;
        [Persistent("createuser")]
        [Size(250)]
        public string createuser
        {
            get { return _createuser; }
            set { SetPropertyValue<string>("createuser", ref _createuser, value); }
        }


        private string _lastupdateuser;
        [Persistent("lastupdateuser")]
        [Size(250)]
        public string lastupdateuser
        {
            get { return _lastupdateuser; }
            set { SetPropertyValue<string>("lastupdateuser", ref _lastupdateuser, value); }
        }

        int _aktif = 1;
        [Persistent("aktif")]
        [Indexed]
        public int aktif
        {
            get { return _aktif; }
            set { SetPropertyValue<int>("aktif", ref _aktif, value); }
        }

        private DateTime _databasekayitzamani;
        [Persistent("databasekayitzamani")]
        public DateTime databasekayitzamani
        {
            get { return _databasekayitzamani; }
            set { SetPropertyValue<DateTime>("databasekayitzamani", ref _databasekayitzamani, value); }
        }


        private DateTime _guncellemezamani;
        [Persistent("guncellemezamani")]
        public DateTime guncellemezamani
        {
            get { return _guncellemezamani; }
            set { SetPropertyValue<DateTime>("guncellemezamani", ref _guncellemezamani, value); }
        }

        //long _kayitsirano;
        //[Persistent("kayitsirano")]
        //[Key(true)]
        //[Indexed]
        //public long kayitsirano
        //{
        //    get { return _kayitsirano; }
        //    set { SetPropertyValue<long>("kayitsirano", ref _kayitsirano, value); }
        //}
    }
}
