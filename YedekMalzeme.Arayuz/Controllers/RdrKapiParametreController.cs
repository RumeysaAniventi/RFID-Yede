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
    public class RdrKapiParametreController : ApiController
    {
        [HttpPost]
        [Route("api/RdrKapiParametreKayit")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RdrKapiParametreKayitResponse RdrKapiParametreKayit(RdrKapiParametreKayitRequest v_Gelen)
        {
            return new RdrKapiParametreManager().fn_RdrKapiParametreDeger(v_Gelen);
        }

        [HttpPost]
        [Route("api/RdrKapiParametreDeger")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RdrKapiParametreDegerResponse RdrKapiParametreDeger(RdrKapiParametreDegerRequest v_Gelen)
        {
            return new RdrKapiParametreManager().fn_RdrKapiParametreDeger(v_Gelen);
        }

        [HttpPost]
        [Route("api/RdrKapiParametreListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RdrKapiParametreListesiResponse RdrKapiParametreListesi(RdrKapiParametreListesiiRequest v_Gelen)
        {
            return new RdrKapiParametreManager().fn_RdrKapiParametreListesi(v_Gelen);
        }
    }
}
