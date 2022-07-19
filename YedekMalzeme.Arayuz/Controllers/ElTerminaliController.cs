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
    public class ElTerminaliController : ApiController
    {

        [HttpPost]
        [Route("api/ElTerminali")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public ElTerminaliResponse fn_FiltreliKapiListesi(List<ElTerminaliRequest> v_Gelen)
        {
            return new elTerminaliManager().fn_ElTerminali(v_Gelen);
        }
    }
}
