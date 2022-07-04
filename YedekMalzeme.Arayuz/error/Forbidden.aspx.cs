using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;

namespace YedekMalzeme.Arayuz.error
{
    public partial class Forbidden : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            using (Session session = XpoManager.Instance.GetNewSession())
            {

                new tblhata(session)
                {
                    aktif = 1,
                    createuser = "",
                    databasekayitzamani = DateTime.Now,
                    guncellemezamani = DateTime.Now,
                    hatakodu = "403-Forbidden",
                    id = Guid.NewGuid().ToString().ToUpper(),
                    ipadresi = GetIPAddress(),
                    lastupdateuser = ""
                }.Save();
                
                Response.Redirect("~/Login.aspx");
            }
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}