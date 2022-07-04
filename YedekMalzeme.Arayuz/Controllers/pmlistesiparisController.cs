using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class pmlistesiparisController : ApiController
    {

        [HttpPost]
        [Route("api/BilesenMalzemeListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public BilesenMalzemeListesiResponse BilesenMalzemeListesi(BilesenMalzemeListesiRequest v_Gelen)
        {
            return new koltukdepoManager().fn_BilesenMalzemeListesi(v_Gelen);
        }

        [HttpPost]
        [Route("api/SiparisBilesenDegistir")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public SiparisBilesenDegistirResponse SiparisBilesenDegistir(SiparisBilesenDegistirRequest v_Gelen)
        {
            return new pmlistesiparisManager().fn_SiparisBilesenDegistir(v_Gelen);
        }

        [HttpPost]
        [Route("api/EpcKimlikBilgileri")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public EpcKimlikBilgileriResponse EpcKimlikBilgileri(EpcKimlikBilgileriRequest v_Gelen)
        {
            return new pmlistesiparisManager().fn_EpcKimlikBilgileri(v_Gelen);
        }

        [HttpPost]
        [Route("api/SiparisMalzemeListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public SiparisMalzemeListesiResponse SiparisMalzemeListesi(SiparisMalzemeListesiRequest v_Gelen)
        {
            return new pmlistesiparisManager().fn_SiparisMalzemeListesi(v_Gelen);
        }

        [HttpPost]
        [Route("api/AcikPmListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public AcikPmListeleResponse AcikPmListele(AcikPmListeleRequest v_Gelen)
        {
            return new pmlistesiparisManager().fn_AcikPmListele(v_Gelen);
        }
    }
}
