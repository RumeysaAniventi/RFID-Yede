using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class koltukdepoController : ApiController
    {
        [HttpPost]
        [Route("api/BilesenMalzemeListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public BilesenMalzemeListesiResponse BilesenMalzemeListesi(BilesenMalzemeListesiRequest v_Gelen)
        {
            return new koltukdepoManager().fn_BilesenMalzemeListesi(v_Gelen);
        }

    }
}
