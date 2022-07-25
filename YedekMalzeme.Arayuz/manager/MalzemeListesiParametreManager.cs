using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.manager
{
    public class MalzemeListesiParametreManager
    {
        internal MalzemeListesiParametreKayitResponse fn_MalzemeListesiParametreKayit(MalzemeListesiParametreKayitRequest v_Gelen)
        {
            MalzemeListesiParametreKayitResponse _Cevap = new MalzemeListesiParametreKayitResponse();
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiparam _Param = session.Query<tblmalzemelistesiparam>().FirstOrDefault(w => w.aktif == 1);
                    if (_Param == null)
                    {
                        new tblmalzemelistesiparam(session)
                        {
                            aktif = 1,
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            mtart = v_Gelen.zMtart,
                            werks = v_Gelen.zIwerk

                        }.Save();
                    }
                    else
                    {
                        _Param.werks = v_Gelen.zIwerk;
                        _Param.mtart = v_Gelen.zMtart;
                        _Param.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _Param.guncellemezamani = DateTime.Now;

                        _Param.Save();

                        new tbl08log(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            aufnr = "",
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            epc = "",
                            islemturu = " Parametreler werks: " + v_Gelen.zIwerk + " mtart:" + v_Gelen.zMtart + " olarak guncellendi",
                            islemyapan = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = "",
                            matnr = "",
                            satirid = _Param.id,
                            sernr = "",
                            tabloadi = "tblmalzemelistesiparam"
                        }.Save();

                    }

                }
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;

            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }
            return _Cevap;
        }

        internal MalzemeListesiParametreDegerResponse fn_MalzemeListesiParametreDeger(MalzemeListesiParametreDegerRequest v_Gelen)
        {
            MalzemeListesiParametreDegerResponse _Cevap = new MalzemeListesiParametreDegerResponse();
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiparam _Param = session.Query<tblmalzemelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param != null)
                    {
                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;
                        _Cevap.zIwerk = _Param.werks;
                        _Cevap.zMtart = _Param.mtart;
                    }

                }

            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zIwerk = "";
                _Cevap.zMtart = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal MalzemeListesiParametreListesiResponse fn_MalzemeListesiParamListesi(MalzemeListesiParametreListesiRequest v_Gelen)
        {
            MalzemeListesiParametreListesiResponse _Cevap = new MalzemeListesiParametreListesiResponse();
            string TabloYazisi = "";
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiparam _Param = session.Query<tblmalzemelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param == null)
                    {
                        new tblmalzemelistesiparam(session)
                        {
                            aktif = 1,
                            createuser = "aniventi",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            werks = "",
                            mtart = "",
                            lastupdateuser = "aniventi"
                        }.Save();

                    }
                    tblmalzemelistesiparam _Param2 = session.Query<tblmalzemelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param2 != null)
                    {
                        TabloYazisi += "<tr>";
                        TabloYazisi += "<td>" + _Param2.mtart + "</td>";
                        TabloYazisi += "<td>" + _Param2.werks + "</td>";
                        TabloYazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick = jsMalzemeListele();>Güncelle</a></td>";
                        TabloYazisi += "</tr>";
                    }


                }


                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap.zTabloYazisi = TabloYazisi;
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
                _Cevap.zTabloYazisi = "";
            }
            return _Cevap;
        }
    }
}