using System.ServiceProcess;
using System.Threading;

namespace AbdiIbrahim.YedekMalzeme.Reader.Kapi
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ThreadPool.QueueUserWorkItem(new cOkumaImpinj().fn_OkumaBasla);

            ThreadPool.QueueUserWorkItem(new cOkumaImpinj().fn_Sirala);
        }

        protected override void OnStop()
        {
        }
    }
}
