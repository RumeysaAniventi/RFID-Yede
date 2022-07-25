using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.manager
{
    public class pmsiparisparametreManager
    {

        internal PMParametreKayitResponse fn_PMParametreKayit(PMParametreKayitRequest v_Gelen)
        {
            #region Değişkenler
            PMParametreKayitResponse _Cevap = new PMParametreKayitResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblzrfidorderlistparam _Temp = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblzrfidorderlistparam(session)
                        {
                            aktif = 1,
                            aufnr = v_Gelen.zaufnr,
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            erdathigh = v_Gelen.zerdathigh,
                            erdatlow = v_Gelen.zerdatlow,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            iwerk = v_Gelen.ziwerk,
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),

                        }.Save();
                       
                    }
                    else
                    {
                        _Temp.aufnr = v_Gelen.zaufnr;
                        _Temp.erdathigh = v_Gelen.zerdathigh;
                        _Temp.erdatlow = v_Gelen.zerdatlow;
                        _Temp.guncellemezamani = DateTime.Now;
                        _Temp.iwerk = v_Gelen.ziwerk;
                        _Temp.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();


                        new tbl08log(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            aufnr ="",
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            epc = "",
                            islemturu = "Parametreler erdathigh: " +v_Gelen.zerdathigh+ " erdatlow:"+v_Gelen.zerdatlow + " iwerk :"+ v_Gelen.ziwerk+ " aufnr "+ v_Gelen.zaufnr+" olarak guncellendi",
                            islemyapan = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = "",
                            matnr = "",
                            satirid = _Temp.id,
                            sernr = "",
                            tabloadi = "tblzrfidorderlistparam"
                        }.Save();


                        _Temp.Save();
                    }

                    _Cevap = new PMParametreKayitResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "İşlem Başarılı";
                }
                    
            }
            catch (Exception ex)
            {

                _Cevap = new PMParametreKayitResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

            }
            return _Cevap;
        }

        internal PMParametreListesiResponse fn_PMParametreListesi(PMParametreListesiRequest v_Gelen)
        {
            #region
            string _TabloYazisi = "";

            PMParametreListesiResponse _Cevap;
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {

                    // tabloda hiç kayıt yoksa tüm değerleri boş bir kayıt ekliyorum
                    tblzrfidorderlistparam _Param = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w=> w.aktif==1);

                    if (_Param == null)
                    {
                        new tblzrfidorderlistparam(session)
                        {
                            createuser = "Admin",                            
                            lastupdateuser = "Admin",
                            aktif = 1,
                            aufnr = "",
                            databasekayitzamani = DateTime.Now,
                            erdathigh = "",
                            erdatlow = "",
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            iwerk = ""
                        }.Save();
                    }

                    tblzrfidorderlistparam _Kayit = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w => w.aktif == 1);



                    if (_Kayit != null)
                    {
                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td>" + _Kayit.erdatlow + " </td>";
                        _TabloYazisi += "<td>" + _Kayit.erdathigh + " </td>";
                        _TabloYazisi += "<td>" + _Kayit.iwerk + " </td>";
                        _TabloYazisi += "<td>" + _Kayit.aufnr + " </td>";
                        _TabloYazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick = jsListele();>Güncelle</a></td>";
                        _TabloYazisi += "</tr>";

                    }
                   
                    _Cevap = new PMParametreListesiResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zTabloYazisi = _TabloYazisi;
                    _Cevap.zAciklama = "";

                    
                }

            }
            catch (Exception ex)
            {
                _Cevap = new PMParametreListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";

                return _Cevap;
            }

         

            return _Cevap;
        }
        

        internal PMParametreDegerResponse fn_PMParametreDeger(PMParametreDegerRequest v_Gelen)
        {
            #region Değişkenler
            PMParametreDegerResponse _Cevap = new PMParametreDegerResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblzrfidorderlistparam _Param = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param != null)
                    {
                        _Cevap = new PMParametreDegerResponse();
                        _Cevap.zAciklama = "";
                        _Cevap.zaufnr = _Param.aufnr;
                        _Cevap.zerdathigh = _Param.erdathigh;
                        _Cevap.zerdatlow = _Param.erdatlow;
                        _Cevap.ziwerk = _Param.iwerk;
                        _Cevap.zSonuc = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                _Cevap = new PMParametreDegerResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "HTN 05:Sistemsel bir hata oluştu.Lütden daha sonra tekrar deneyiniz";
                _Cevap.zaufnr = "";
                _Cevap.zerdathigh = "";
                _Cevap.zerdatlow = "";
                _Cevap.ziwerk = "";
            }

            return _Cevap;
        }
    }
}