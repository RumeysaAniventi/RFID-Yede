using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YedekMalzeme.Arayuz
{
    public partial class StokRapor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Session session = XpoManager.Instance.GetNewSession())
            {
                string _yetki = HttpContext.Current.Session["Yetki"].ToString();
                if (_yetki == "Kullanici")
                {
                    Response.Redirect("login.aspx");
                }


            }
        }
    }
}