using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace YedekMalzeme.Arayuz
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error(Object o, EventArgs e)
        {
            System.Exception hata = Server.GetLastError();

            hatayiGunlugeYaz(hata);

            Server.ClearError();

            // Response.End();

            Response.Redirect("Anasayfa.aspx");
        }

        void hatayiGunlugeYaz(Exception e)
        {

            string _Tarih = DateTime.Now.ToString("dd.MM.yyyy");

            _Tarih = _Tarih.Replace(" ", "_");
            _Tarih = _Tarih.Replace(".", "_");
            _Tarih = _Tarih.Replace(":", "_");

            StreamWriter sw = new StreamWriter(Server.MapPath("~/gunlukler/hatagunlugu_"+_Tarih+".log"), true, Encoding.UTF8);

            sw.WriteLine("--- HATA ---");

            sw.Write("Zaman          :");
            sw.WriteLine(DateTime.UtcNow.ToString());

            sw.Write("IP             :");
            sw.WriteLine(Request.ServerVariables["REMOTE_ADDR"]);

            sw.Write("Hata Metni     : ");
            sw.WriteLine(e.ToString());

            sw.Write("Yığın izlemesi : ");
            sw.WriteLine(e.StackTrace);

            sw.Write("Dil            : ");
            sw.WriteLine(Request.ServerVariables["HTTP_ACCEPT_LANGUAGE"].ToString());

            sw.Write("Tarayıcı       : ");
            sw.WriteLine(Request.Browser.Type);

            sw.Write("Sürüm          : ");
            sw.WriteLine(Request.Browser.Version);

            sw.Write("İşletim sistemi: ");
            sw.WriteLine(Request.Browser.Platform);

            sw.Write("Yol            : ");
            sw.WriteLine(Request.Path.ToString());

            sw.Write("Sorgu Dizesi   : ");
            sw.WriteLine(Request.QueryString.ToString());

            sw.WriteLine("--- HATA SONU ---");

            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        protected void Application_PostAuthorizeRequest()
        {
            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            }
        }

        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {

            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket ticket = id.Ticket; string userData = ticket.UserData;
                        string[] roles = userData.Split(new char[] { ',' });
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }
                }
            }
        }

    }
}
