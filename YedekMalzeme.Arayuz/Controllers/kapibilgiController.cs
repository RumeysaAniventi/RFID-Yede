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
    public class kapibilgiController : ApiController
    {

        

        //[HttpPost]
        //[Route("api/SipariseBilesenEkle")]
        //[System.Web.Mvc.ValidateAntiForgeryToken]
        //[Authorize(Roles = "roleadmin")]
        //public SipariseBilesenEkleResponse SipariseBilesenEkle(SipariseBilesenEkleRequest v_Gelen)
        //{
        //    return new kapibilgiManager().fn_SipariseBilesenEkle(v_Gelen);
        //}
        //[HttpPost]
        //[Route("api/SipariseEkle")]
        //[System.Web.Mvc.ValidateAntiForgeryToken]
        //[Authorize(Roles = "roleadmin")]
        //public SipariseEkleResponse SipariseEkle(SipariseEkleRequest v_Gelen)
        //{
        //    return new kapibilgiManager().fn_SipariseEkle(v_Gelen);
        //}
        //[HttpPost]
        //[Route("api/AlarmKapat")]
        //[System.Web.Mvc.ValidateAntiForgeryToken]
        //[Authorize(Roles = "roleadmin")]
        //public AlarmKapatResponse AlarmKapat(AlarmKapatRequest v_Gelen)
        //{
        //    return new kapibilgiManager().fn_AlarmKapat(v_Gelen);
        //}



        [HttpPost]
        [Route("api/AlarmGoruntule")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin")]
        public AlarmGoruntuleResponse AlarmGoruntule(AlarmGoruntuleRequest v_Gelen)
        {
            return new kapibilgiManager().fn_AlarmGoruntule(v_Gelen);
        }

        [HttpPost]
        [Route("api/KapiReaderListesi")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public KapiReaderListesiResponse KapiReaderListesi(KapiReaderListesiRequest v_Gelen)
        {
            return new kapibilgiManager().fn_KapiReaderListesi(v_Gelen);
        }

    }
}
