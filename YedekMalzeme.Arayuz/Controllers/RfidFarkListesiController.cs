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
    public class RfidFarkListesiController : ApiController
    {

        [HttpPost]
        [Route("api/RfidFarklistesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public RfidFarkListesiResponse fn_RfidFarkListesi(RfidFarkListesiRequest v_Gelen)
        {
            return new rfidFarkManager().fn_RfidFarkListesi(v_Gelen);
        }

    }

   
}
