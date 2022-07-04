using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class MalzemeListesiParametreDegerResponse:ResponseOrtak
    {
        public string zIwerk { get; set; }
        public string zMtart { get; set; }
    }
}