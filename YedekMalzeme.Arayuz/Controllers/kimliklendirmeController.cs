using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class kimliklendirmeController : ApiController
    {
        [HttpPost]
        [Route("api/KimliklendirmeMalzemeListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KimliklendirmeMalzemeListesiResponse KimliklendirmeMalzemeListesi(KimliklendirmeMalzemeListesiRequest v_gelen)
        {
            return new kimliklendirmeManager().fn_KimliklendirmeMalzemeListesi_v2(v_gelen);
        }


    }
}
