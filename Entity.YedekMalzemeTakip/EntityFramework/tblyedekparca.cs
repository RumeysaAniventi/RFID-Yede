using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.YedekMalzemeTakip.EntityFramework
{
    [Persistent("tblyedekparca")]
    public class tblyedekparca : TabloObject
    {
        public tblyedekparca(Session session) : base(session) { }


    }
}
