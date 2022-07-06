﻿using System;
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
        public FiltreliKapiListesiResponse fn_FiltreliKapiListesi(FiltreliKapiListesiRequest v_Gelen)
        {
            return new kapirapor2Manager().fn_FiltreliKapiListesi(v_Gelen);
        }

        [HttpPost]
        [Route("api/AlarmDurumGoruntule")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public AlarmDurumGoruntuleResponse fn_AlarmDurumGoruntule(AlarmDurumGoruntuleRequest v_Gelen)
        {
            return new kapirapor2Manager().fn_AlarmDurumGoruntule(v_Gelen);
        }
    }
}
