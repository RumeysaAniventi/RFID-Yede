using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.View;

namespace YedekMalzeme.Arayuz.response
{
    public class KoltukDepoListeleResponse:ResponseOrtak
    {
        public List<KoltukDepoListeleView> zdizi { get; set; }
     
    }
}