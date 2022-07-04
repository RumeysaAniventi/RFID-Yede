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
    public class kapirapor2Controller : ApiController
    {
        
        [HttpPost]
        [Route("api/FiltreliKapiListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public FiltreliKapiListesiResponse SipariseBilesenEkle(FiltreliKapiListesiRequest v_Gelen)
        {
            return new kapirapor2Manager().fn_SipariseBilesenEkle(v_Gelen);
        }
    }
}
