using System;
using System.Web.UI;

namespace YedekMalzeme.Arayuz
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Response.Redirect("Anasayfa.aspx");
            }
        }
    }
}