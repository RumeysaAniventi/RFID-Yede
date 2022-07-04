using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class kimliklendirmev2Controller : ApiController
    {
        

        [HttpPost]
        [Route("api/SecimiTamamla")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public SecimiTamamlaResponse SecimiTamamla(SecimiTamamlaRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_SecimiTamamla(v_gelen);
        }


        [HttpPost]
        [Route("api/MalzemeAdiBul")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MalzemeAdiBulResponse MalzemeAdiBul(MalzemeAdiBulRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_MalzemeAdiBul(v_gelen);
        }

        [HttpPost]
        [Route("api/MalzemeKoduBul")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MalzemeKoduBulResponse MalzemeKoduBul(MalzemeKoduBulRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_MalzemeKoduBul(v_gelen);
        }

        [HttpPost]
        [Route("api/MazlemeListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MazlemeListeleResponse MazlemeListele(MazlemeListeleRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_MazlemeListele(v_gelen);
        }

        [HttpPost]
        [Route("api/MalzemeAdiListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MalzemeAdiListeleResponse MalzemeAdiListele(MalzemeAdiListeleRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_MalzemeAdiListele(v_gelen);
        }

        [HttpPost]
        [Route("api/MalzemeKoduListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MalzemeKoduListeleResponse MalzemeKoduListele(MalzemeKoduListeleRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_MalzemeKoduListele(v_gelen);
        }

        [HttpPost]
        [Route("api/KimlikKimliklendir")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KimlikKimliklendirResponse KimlikKimliklendir(KimlikKimliklendirRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_KimlikKimliklendir(v_gelen);
        }

        [HttpPost]
        [Route("api/KimlikSeriNoListe")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KimlikSeriNoListeResponse SeriNoListe(KimlikSeriNoListeRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_KimlikSeriNoListe(v_gelen);
        }

        [HttpPost]
        [Route("api/KimlikEpcGetir")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KimlikEpcGetirResponse KimlikEpcGetir(KimlikEpcGetirRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_KimlikEpcGetir(v_gelen);
        }

        [HttpPost]
        [Route("api/KimliklMalzemeListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KimliklMalzemeListesiResponse KimliklMalzemeListesi(KimliklMalzemeListesiRequest v_gelen)
        {
            return new kimliklendirmev2Manager().fn_KimliklMalzemeListesi(v_gelen);
        }
    }
}
