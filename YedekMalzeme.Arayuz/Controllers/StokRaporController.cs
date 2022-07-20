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
    public class StokRaporController : ApiController
    {
        [HttpPost]
        [Route("api/DepoListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public DepoListeleResponse fn_DepoListele(DepoListeleRequest v_Gelen)
        {
            return new StokRaporManager().fn_DepoListele(v_Gelen);
        }

        
        [HttpPost]
        [Route("api/StokSay")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public StokSayResponse fn_StokSay(StokSayRequest v_Gelen)
        {
            return new StokRaporManager().fn_StokSay(v_Gelen);
        }

        
        [HttpPost]
        [Route("api/KoltukDepoListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public KoltukDepoListeleResponse fn_KoltukDepoListele(KoltukDepoListeleRequest v_Gelen)
        {
            return new StokRaporManager().fn_KoltukDepoListele(v_Gelen);
        }

    }
}
