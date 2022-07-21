using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Entity.YedekMalzemeTakip.Md5;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Security;

namespace YedekMalzeme.Arayuz
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            fn_LoginOl(new loginUser());
            //fn_LoginOl();
        }

        public string GetUserIP()
        {
            string strIP = String.Empty;
            HttpRequest httpReq = HttpContext.Current.Request;

            if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }

            else if
            (
                (httpReq.UserHostAddress.Length != 0)
                &&

                ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
            )
            {
                strIP = httpReq.UserHostAddress;
            }

            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strIP = sr.ReadToEnd();
                }

                int i1 = strIP.IndexOf("Address: ") + 9;
                int i2 = strIP.LastIndexOf("</body>");
                strIP = strIP.Substring(i1, i2 - i1);
            }
            return strIP;
        }
        

        public class loginUser
        {
            public string _KullaniciAdi { get; set; }
            public string _Sifre { get; set; }
        }
        public void fn_LoginOl(loginUser loginUser)
        {

            #region Değişkenler
            string _KullaniciAdi = "";
            string _Sifre = "";
            string _Yetki = "";

            int _Sonuc = 0;
            #endregion

            //lblSonuc.Text = "  ";
            _KullaniciAdi = loginUser._KullaniciAdi == null ? txtUserName.Text.Trim() : loginUser._KullaniciAdi ;
            _Sifre = loginUser._Sifre == null ? txtPassword.Text.Trim() : loginUser._Sifre;

            if (string.IsNullOrEmpty(_KullaniciAdi) || string.IsNullOrWhiteSpace(_KullaniciAdi) || _KullaniciAdi == "")
            {
               // lblSonuc.Text = "Lütfen kullanıcı adı ve/veya şifre alanını doldurunuz";
                return;
            }

            if (string.IsNullOrEmpty(_Sifre) || string.IsNullOrWhiteSpace(_Sifre) || _Sifre == "")
            {
               // lblSonuc.Text = "Lütfen kullanıcı adı ve/veya şifre alanını doldurunuz";
                return;
            }

            _Sifre = EncryptionHelper.ToMD5(_Sifre);

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Temp = session.Query<tblarayuzkullanici>().FirstOrDefault(w => w.kullaniciadi.Equals(_KullaniciAdi) && w.aktif == 1 && w.sifre.Equals(_Sifre));
                 
                    if (_Temp == null)
                    {
                        new tblloginlog(session)
                        {
                            aktif = 1,
                            createuser = "aniventi",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            lastupdateuser = "aniventi",
                            zipadresi = GetUserIP(),
                            zkullaniciadi = _KullaniciAdi,
                            zsonuc = 0
                        }.Save();

                       // lblSonuc.Text = "Hatalı kullanıcı adı veya şifre";
                        return;

                    }
                    else
                    {
                        new tblloginlog(session)
                        {
                            aktif = 1,
                            createuser = "aniventi",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            lastupdateuser = "aniventi",
                            zipadresi = GetUserIP(),
                            zkullaniciadi = _KullaniciAdi,
                            zsonuc = 1
                        }.Save();

                        Session["tblkullaniciid"] = _Temp.id.ToString();
                        Session["KullaniciAdi"] = _KullaniciAdi;
                        Session["isLogin"] = true;




                        tblarayuzkullanici _Temp1 = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.kullaniciadi == _KullaniciAdi);
                        if (_Temp1.yetki == "1")
                        {
                            _Yetki = "roleadmin";
                        }
                        else
                        {
                            _Yetki = "Kullanici";
                        }




                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, _KullaniciAdi, DateTime.Now, DateTime.Now.AddMinutes(75), false, _Yetki, FormsAuthentication.FormsCookiePath);

                        string encTicket = FormsAuthentication.Encrypt(ticket);
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                        if (ticket.IsPersistent)
                        {
                            cookie.Expires = ticket.Expiration;
                        }
                        Response.Cookies.Add(cookie);

                        Response.Redirect("Anasayfa.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                //lblSonuc.Text = "Sistemsel br hata oluştu. Lütfen daha sonra tekrar deneyiniz";
            }
        }
    }
}