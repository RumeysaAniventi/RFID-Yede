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
    public class MalzemeBelgeListesiParametreController : ApiController
    {
        [HttpPost]
        [Route("api/MalzemeBelgeListesiParametreDegeri")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public MalzemeBelgeListesiParamDegerResponse MalzemeBelgeListesiParamDeger(MalzemeBelgeListesiParamDegerRequest v_gelen)
        {
            return new MalzemeBelgeListesiParamManager().fn_BelgeDegerleriListeDeger(v_gelen);
        }

        [HttpPost]
        [Route("api/MalzemeBelgeListesiParametreListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public MalzemeBelgeListesiParametreListesiResponse MalzemeBelgeListesiParametreListesi(MalzemeBelgeListesiParametreRequest v_gelen)
        {
            int i = 0;
            return new MalzemeBelgeListesiParamManager().fn_BelgeDegerleriListele(v_gelen);
        }

        [HttpPost]
        [Route("api/MalzemeBelgeListesiParametreKayit")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public MalzemeBelgeListesiParametreKayitResponse MalzemeBelgeListesiParametreKayit(MalzemeBelgeListesiParametreKayitRequest v_gelen)
        {
            return new MalzemeBelgeListesiParamManager().fn_BelgeDegerleriKaydet(v_gelen);
        }
    }
}
