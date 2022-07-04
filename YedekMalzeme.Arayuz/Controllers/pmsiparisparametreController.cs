using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class pmsiparisparametreController : ApiController
    {
        [HttpPost]
        [Route("api/PMParametreKayit")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public PMParametreKayitResponse PMParametreKayit(PMParametreKayitRequest v_Gelen)
        {
            return new pmsiparisparametreManager().fn_PMParametreKayit(v_Gelen);
        }

        [HttpPost]
        [Route("api/PMParametreDeger")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public PMParametreDegerResponse PMParametreDeger(PMParametreDegerRequest v_Gelen)
        {
            return new pmsiparisparametreManager().fn_PMParametreDeger(v_Gelen);
        }

        [HttpPost]
        [Route("api/PMParametreListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public PMParametreListesiResponse PMParametreListesi(PMParametreListesiRequest v_Gelen)
        {
            return new pmsiparisparametreManager().fn_PMParametreListesi(v_Gelen);
        }
    }
}
