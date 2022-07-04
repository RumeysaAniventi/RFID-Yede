using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.View;

namespace YedekMalzeme.Arayuz.response
{
    public class FiltrelenmisKimlikMalzemeListeleResponse:ResponseOrtak
    {

        public List<FiltrelenmisKimliklendirmeView> zDizi { get; set; }
    }
}