using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YedekMalzeme.Arayuz.response
{
    public class MalzemeBelgeListesiParamDegerResponse : ResponseOrtak
    {
        public string z_BudatLow { get; set; }
        public string z_BudatHigh { get; set; }
        public string z_Iwerk { get; set; }
        public string z_Aufnr { get; set; }
    }
}