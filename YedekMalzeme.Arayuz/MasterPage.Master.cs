using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace YedekMalzeme.Arayuz
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _yetki = "";

            
            if (!Page.IsPostBack)
            {
                try
                {
                
                  
                    if (Session["isLogin"] == null || bool.Parse(Session["isLogin"].ToString()) == false)
                    {
                        Response.Redirect("login.aspx");

                        
                      
                    }

                    else
                    {
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            tblarayuzkullanici _Temp = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.kullaniciadi.Equals(HttpContext.Current.Session["KullaniciAdi"].ToString()));

                            if (_Temp != null)
                            {
                                kisiad.InnerText = _Temp.adi;
                                kisisoyad.InnerText = _Temp.soyadi;
                                _yetki = _Temp.yetki;
                            }
                            if (_yetki=="2")
                            {
                                
                                
                                Li6.Visible = false;
                                Li5.Visible = false;
                            }

                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
    }
}