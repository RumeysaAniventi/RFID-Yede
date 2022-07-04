using Arayuz.Manager;
using Arayuz.Request;
using Arayuz.Response;
using System.Web.Http;

namespace Arayuz.Controllers
{
    public class BildirimController : ApiController
    {
        [HttpPost]
        [Route("api/BildirimEkle")]
        public BildirimSonucView BildirimEkle(RequestBildirimTuru v_Gelen)
        {
            return new BildirimEkraniManager().fn_BildirimEkle(v_Gelen);
        }

        [HttpPost]
        [Route("api/YedekMalzemeBildirim")]
        public YedekMalzemeBildirimResponse YedekMalzemeBildirim(YedekMalzemeBildirimRequest v_Gelen)
        {
            return new BildirimEkraniManager().fn_YedekMalzemeBildirim(v_Gelen);
        }

    }
}
