using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class StokeListesiParametreController : ApiController
    {
        [HttpPost]
        [Route("api/StokParametreKayit")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public StokeListesiParametreKayitResponse StokParametreKayit(StokeListesiParametreKayitRequest v_Gelen)
        {
            return new StokeListesiParametreManager().StokParametreKayit(v_Gelen);
        }

        [HttpPost]
        [Route("api/StokeListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public StokeListesiParametreListesiResponse StokeListesiGetir(StokeListesiParametreListesiRequest v_Gelen)
        {
            return new StokeListesiParametreManager().StokeListesiGetir(v_Gelen);
        }
        [HttpPost]
        [Route("api/StokDeger")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public StokeListesiParametreDegerResponse StokDegerOlustur(StokeListesiParametreDegerRequest v_Gelen)
        {
            return new StokeListesiParametreManager().StokDegerOlustur(v_Gelen);
        }


    }
}
