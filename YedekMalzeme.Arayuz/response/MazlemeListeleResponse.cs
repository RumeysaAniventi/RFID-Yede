using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class MazlemeListeleResponse:ResponseOrtak
    {
        public string zmalzemkodulistesi { get; set; }
        public string zmalzemeListesi { get; set; }
    }
}