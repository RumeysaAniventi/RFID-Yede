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
    public class KimliklendirmeRaporController : ApiController
    {


        [HttpPost]
        [Route("api/FiltrelenmisKimlikMalzemeListele")]
        [Authorize(Roles = "roleadmin")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public FiltrelenmisKimlikMalzemeListeleResponse FiltrelenmisKimlikMalzemeListele(FiltrelenmisKimlikMalzemeListeleRequest v_Gelen)
        {
            return new kimliklendirmeRaporManager().fn_FiltrelenmisKimlikMalzemeListele(v_Gelen);
        }

        [HttpPost]
        [Route("api/KimlikRaporFiltreleme")]
        [Authorize(Roles = "roleadmin")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public KimlikRaporFiltrelemeResponse KimlikRaporFiltreleme(KimlikRaporFiltrelemeRequest v_Gelen)
        {
            return new kimliklendirmeRaporManager().fn_KimlikRaporFiltreleme(v_Gelen);
        }


        [HttpPost]
        [Route("api/KimlikTarihListele")]

        [Authorize(Roles = "roleadmin")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public KimlikTarihListeleResponse KimlikTarihListele(KimlikTarihListeleRequest v_Gelen)
        {
            return new kimliklendirmeRaporManager().fn_KimlikTarihListele(v_Gelen);
        }



        [HttpPost]
        [Route("api/KimliklendirmeRaporListele")]
        [Authorize(Roles = "roleadmin")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        public KimliklendirmeRaporListeleResponse KimliklendirmeRaporListele(KimliklendirmeRaporListeleRequest v_Gelen)
        {
            return new kimliklendirmeRaporManager().fn_KimliklendirmeRaporListele(v_Gelen);
        }

        [HttpPost]
        [Route("api/KimliklendirilmisMalzemeListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KimliklendirilmisMalzemeListeleResponse KimliklendirilmisMalzemeListele(KimliklendirilmisMalzemeListeleRequest v_Gelen)
        {
            return new kimliklendirmeRaporManager().fn_KimliklendirilmisMalzemeListele(v_Gelen);
        }
    }
}
