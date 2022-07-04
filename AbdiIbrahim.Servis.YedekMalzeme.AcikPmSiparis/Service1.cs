using log4net;
using System.ServiceProcess;
using System.Threading;

namespace AbdiIbrahim.Servis.YedekMalzeme.AcikPmSiparis
{
    public partial class Service1 : ServiceBase
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(Service1));

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _LogDosyasi.Info("Servis başladı");

            ThreadPool.QueueUserWorkItem(new cAcikPmSiparisiIslem().fn_OrderList);
           
        }

        protected override void OnStop()
        {
            _LogDosyasi.Info("Servis durdu");
        }
    }
}
