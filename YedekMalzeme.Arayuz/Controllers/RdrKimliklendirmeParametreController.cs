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
    public class RdrKimliklendirmeParametreController : ApiController
    {
        [HttpPost]
        [Route("api/RdrKimliklendirmeParametreKayit")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RdrKimliklendirmeParametreKayitResponse RdrKimliklendirmeParametreKayit(RdrKimliklendirmeParametreKayitRequest v_Gelen)
        {
            return new RdrKimliklendirmeParametreManager().fn_RdrKimliklendirmeParametreKayit(v_Gelen);
        }

        [HttpPost]
        [Route("api/RdrKimliklendirmeParametreDeger")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RdrKimliklendirmeParametreDegerResponse RdrKimliklendirmeParametreDeger(RdrKimliklendirmeParametreDegerRequest v_Gelen)
        {
            return new RdrKimliklendirmeParametreManager().fn_RdrKimliklendirmeParametreDeger(v_Gelen);
        }

        [HttpPost]
        [Route("api/RdrKimliklendirmeParametreListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RdrKimliklendirmeParametreListesiResponse RdrKimliklendirmeParametreListesi(RdrKimliklendirmeParametreListesiRequest v_Gelen)
        {
            return new RdrKimliklendirmeParametreManager().fn_RdrKimliklendirmeParametreListesi(v_Gelen);
        }
    }
}
