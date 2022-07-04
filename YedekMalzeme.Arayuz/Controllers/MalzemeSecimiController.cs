using System.Web.Http;
using YedekMalzeme.Arayuz.manager;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.Controllers
{
    public class MalzemeSecimiController : ApiController
    {
        [HttpPost]
        [Route("api/MalzemeAraListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MalzemeAraListeleResponse MalzemeAraListele(MalzemeAraListeleRequest v_Gelen)
        {
            return new MalzemeSecimiManager().fn_MalzemeAraListele(v_Gelen);
        }

        [HttpPost]
        [Route("api/MalzemeUrunListele")]
        [System.Web.Mvc.ValidateAntiForgeryToken]
        [Authorize(Roles = "roleadmin,Kullanici")]
        public MalzemeUrunListeleResponse MalzemeUrunListele(MalzemeUrunListeleRequest v_Gelen)
        {
            return new MalzemeSecimiManager().fn_MalzemeUrunListele(v_Gelen);
        }
    }
}
