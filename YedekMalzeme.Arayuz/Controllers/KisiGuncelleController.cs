
using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class KisiGuncelleController : ApiController
    {
        [HttpPost]
        [Route("api/KisiGetir")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KisiGetirResponse KisiGetir(KisiGetirRequest v_gelen)
        {
            return new KisiGuncelleManager().fn_KisiGetir(v_gelen);
        }

        [HttpPost]
        [Route("api/KisiKontrolEt")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KisiKontrolEtResponse KisiKontrolEt(KisiKontrolEtRequest v_gelen)
        {
            return new KisiGuncelleManager().fn_KisiKontrolEt(v_gelen);
        }

        [HttpPost]
        [Route("api/KullaniciGuncelle")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KullaniciGuncelleResponse KullaniciGuncelle(KullaniciGuncelleEtRequest v_gelen)
        {
            return new KisiGuncelleManager().fn_KullaniciGuncelle(v_gelen);
        }

        
    }
}
