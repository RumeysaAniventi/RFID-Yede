using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuketim.Manager;
using Tuketim.Request;
using Tuketim.Response;

namespace Tuketim.Controllers
{
    public class TuketimController : Controller
    {
        [HttpPost]
        [Route("api/Tuketim")]
        public ViewTuketimDurum TuketimDurumEkle(RequestTuketimDurum v_Gelen)
        {
            return new TuketimManager().fn_TuketimDurumEkle(v_Gelen);
        }
        
    }
}