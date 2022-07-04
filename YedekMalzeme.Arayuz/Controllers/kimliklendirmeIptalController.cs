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
    public class kimliklendirmeIptalController : ApiController
    {



        [HttpPost]
        [Route("api/KimlikIptalMalzemeGetir")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KimlikIptalMalzemeGetirResponse KimlikIptalMalzemeGetir(KimlikIptalMalzemeGetirRequest v_gelen)
        {
            return new KimliklendirmeIptalManager().fn_KimlikIptalMalzemeGetir(v_gelen);
        }

        [HttpPost]
        [Route("api/KimlikIptalEpcGetir")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KimlikIptalEpcGetirResponse KimlikIptalEpcGetir(KimlikIptalEpcGetirRequest v_gelen)
        {
            return new KimliklendirmeIptalManager().fn_KimlikIptalEpcGetir(v_gelen);
        }


        [HttpPost]
        [Route("api/KimlikMalzemeIptal")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KimlikMalzemeIptalResponse KimlikMalzemeIptal(KimlikMalzemeIptalRequest v_gelen)
        {
            return new KimliklendirmeIptalManager().fn_KimlikMalzemeIptal(v_gelen);
        }

    }
}
