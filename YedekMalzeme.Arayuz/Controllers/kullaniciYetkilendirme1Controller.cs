using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class kullaniciYetkilendirme1Controller : ApiController
    {

        [HttpPost]
        [Route("api/SifreYenileme")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public SifreYenilemeResponse SifreYenileme(SifreYenilemeRequest v_gelen)
        {
            return new kullaniciYetkilendirme1tManager().fn_SifreYenileme(v_gelen);
        }

        [HttpPost]
        [Route("api/KisiKaydet")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KisiKaydetResponse KisiKaydet(KisiKaydetRequest v_Gelen)
        {
            return new kullaniciYetkilendirme1tManager().fn_KisiKaydet(v_Gelen);
        }

       

        
        [HttpPost]
        [Route("api/YetkiSil")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public YetkiSilResponse YetkiSil(YetkiSilRequest v_Gelen)
        {
            return new kullaniciYetkilendirme1tManager().fn_YetkiSil(v_Gelen);
        }

        
        [HttpPost]
        [Route("api/YetkiGuncelle")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public YetkiGuncelleResponse YetkiGuncelle(YetkiGuncelleRequest v_Gelen)
        {
            return new kullaniciYetkilendirme1tManager().fn_YetkiGuncelle(v_Gelen);
        }

        
        [HttpPost]
        [Route("api/YetkiGuncelleModalAc")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public YetkiGuncelleModalAcResponse YetkiGuncelleModalAc(YetkiGuncelleModalAcRequest v_Gelen)
        {
            return new kullaniciYetkilendirme1tManager().fn_YetkiGuncelleModalAc(v_Gelen);
        }


        [HttpPost]
        [Route("api/KisiListele1")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KisiListele1Response KisiListele1(KisiListele1Request v_Gelen)
        {
            return new kullaniciYetkilendirme1tManager().fn_KisiListele(v_Gelen);
        }
    }
}
