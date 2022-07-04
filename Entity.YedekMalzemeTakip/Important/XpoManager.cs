using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Web.Configuration;


namespace Entity.YedekMalzemeTakip.Important
{
    public class XpoManager : Singleton<XpoManager>
    {
        private string _connectionString;

        public string ConnectionString
        {
            get { return _connectionString; }
        }

        //public void InitServiceXpo(String connectionString)
        //{
        //    _connectionString = connectionString;
        //    UpdateDatabase();
        //}


        public void InitXpo()
        {
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;


            string Server = ConfigurationManager.AppSettings["BaglantiSunucuIp"].ToString().Trim();
            string UserID = ConfigurationManager.AppSettings["BaglantiKullaniciAdi"].ToString().Trim();            
            string Database = ConfigurationManager.AppSettings["BaglantiDatabase"].ToString().Trim();
            var password = "";

            var section = WebConfigurationManager.GetSection("secureAppSettings") as NameValueCollection;

            if (section != null && section["BaglantiSifre"] != null)
            {
                password = section["BaglantiSifre"];
            }

            _connectionString = PostgreSqlConnectionProvider.GetConnectionString(Server, UserID, password, Database);

            UpdateDatabase();
        }

        private XpoManager() { }

        public Session GetNewSession()
        {
            string Server = ConfigurationManager.AppSettings["BaglantiSunucuIp"].ToString().Trim();
            string UserID = ConfigurationManager.AppSettings["BaglantiKullaniciAdi"].ToString().Trim();
            //string password = ConfigurationManager.AppSettings["BaglantiSifre"].ToString().Trim();
            string Database = ConfigurationManager.AppSettings["BaglantiDatabase"].ToString().Trim();

            var password = "";

            var section = WebConfigurationManager.GetSection("secureAppSettings") as NameValueCollection;

            if (section != null && section["BaglantiSifre"] != null)
            {
                password = section["BaglantiSifre"];
            }

            _connectionString = PostgreSqlConnectionProvider.GetConnectionString(Server, UserID, password, Database);

            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;

            using (IDataLayer dal = XpoDefault.GetDataLayer(_connectionString, AutoCreateOption.DatabaseAndSchema))
            {
                return new Session(DataLayer);
            }
        }



        public UnitOfWork GetNewUnitOfWork()
        {
            return new UnitOfWork(DataLayer);
        }

        private readonly object _lockObject = new object();

        volatile IDataLayer _fDataLayer;

        IDataLayer DataLayer
        {
            get
            {
                if (_fDataLayer == null)
                {
                    lock (_lockObject)
                    {
                        if (_fDataLayer == null)
                        {
                            _fDataLayer = GetDataLayer();
                        }
                    }
                }
                return _fDataLayer;
            }
        }

        private IDataLayer GetDataLayer()
        {

            XpoDefault.Session = null;
            XPDictionary dict = new ReflectionDictionary();
            dict.GetDataStoreSchema(Assembly.GetExecutingAssembly());

            return XpoDefault.GetDataLayer(_connectionString, AutoCreateOption.DatabaseAndSchema);

        }





        private void UpdateDatabase()
        {
            SimpleDataLayer.SuppressReentrancyAndThreadSafetyCheck = true;

            string Server = ConfigurationManager.AppSettings["BaglantiSunucuIp"].ToString().Trim();
            string UserID = ConfigurationManager.AppSettings["BaglantiKullaniciAdi"].ToString().Trim();
            //string password = ConfigurationManager.AppSettings["BaglantiSifre"].ToString().Trim();
            string Database = ConfigurationManager.AppSettings["BaglantiDatabase"].ToString().Trim();
            var password = "";
            var section = WebConfigurationManager.GetSection("secureAppSettings") as NameValueCollection;

            if (section != null && section["BaglantiSifre"] != null)
            {
                password = section["BaglantiSifre"];
            }

            _connectionString = PostgreSqlConnectionProvider.GetConnectionString(Server, UserID, password, Database);

            using (IDataLayer dal = XpoDefault.GetDataLayer(_connectionString, AutoCreateOption.DatabaseAndSchema))
            {
                using (Session session = new Session(dal))
                {
                    Assembly asm = Assembly.GetExecutingAssembly();
                    session.UpdateSchema(asm);
                    session.CreateObjectTypeRecords(asm);
                }
            }
        }
    }
}
