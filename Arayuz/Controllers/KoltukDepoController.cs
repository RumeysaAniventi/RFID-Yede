
using Arayuz.Manager;
using Arayuz.Request;
using Arayuz.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Arayuz.Controllers
{
    public class KoltukDepoController : ApiController
    {
        [HttpPost]
        [Route("api/KoltukDepoKaydet")]
        public KoltukDepoKaydetView KoltukDepoKaydet(RequesKoltukDepoKaydet v_Gelen)
        {
            return new KoltukDepoManager().fn_KoltukDepoKaydet(v_Gelen);
        }
    }
}
