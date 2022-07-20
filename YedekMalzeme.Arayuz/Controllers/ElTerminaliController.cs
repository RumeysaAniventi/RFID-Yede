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
        [Authorize(Roles = "roleadmin,Kullanici")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
       
        public ElTerminaliResponse fn_FiltreliKapiListesi(List<ElTerminaliRequest> v_Gelen)
        {
            return new elTerminaliManager().fn_ElTerminali(v_Gelen);
        }
        [HttpPost]
        [Route("api/loginAdmin")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
       
        public void loginAdmin(Login.loginUser user)
        {
            Login login = new Login();
            login.fn_LoginOl(user);           
        }
    }
}
