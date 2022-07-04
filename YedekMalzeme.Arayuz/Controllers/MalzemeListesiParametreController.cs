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
    public class MalzemeListesiParametreController : ApiController
    {
        [HttpPost]
        [Route("api/MalzemeListesiParamKayit")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public MalzemeListesiParametreKayitResponse MalzemeListesiParamKayit(MalzemeListesiParametreKayitRequest v_Gelen)
        {
            return new MalzemeListesiParametreManager().fn_MalzemeListesiParametreKayit(v_Gelen);
        }

        [HttpPost]
        [Route("api/MalzemeListesiParamDeger")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public MalzemeListesiParametreDegerResponse MalzemeListesiParamDeger(MalzemeListesiParametreDegerRequest v_Gelen)
        {
            return new MalzemeListesiParametreManager().fn_MalzemeListesiParametreDeger(v_Gelen);
        }

        [HttpPost]
        [Route("api/MalzemeListesiParamListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public MalzemeListesiParametreListesiResponse MalzemeListesiParamListesi(MalzemeListesiParametreListesiRequest v_Gelen)
        {
            return new MalzemeListesiParametreManager().fn_MalzemeListesiParamListesi(v_Gelen);
        }

    }
}
