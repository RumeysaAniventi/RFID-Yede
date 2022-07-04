using log4net;
using System.ServiceProcess;
using System.Threading;
using YedekMalzeme.SAPServis.MalzemeListesi;
using YedekMalzeme.SAPServis.MalzemeStokListesi;

namespace YedekMalzeme.SAPServis
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

            ThreadPool.QueueUserWorkItem(new cIslem().fn_MalzemeListesiCek);

            ThreadPool.QueueUserWorkItem(new cMalzemeStokListesiIslem().fn_MalzemeStokListesiCek);
        }

        protected override void OnStop()
        {
            _LogDosyasi.Info("Servis durdu");
        }
    }
}
