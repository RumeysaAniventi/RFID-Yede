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
    public class MalzemeBelgeListesiParamManager
    {
        public MalzemeBelgeListesiParametreListesiResponse fn_BelgeDegerleriListele(MalzemeBelgeListesiParametreRequest v_gelen)
        {
            #region Değişkenler
            string _Tabloyazisi = "";
            MalzemeBelgeListesiParametreListesiResponse _Cevap = new MalzemeBelgeListesiParametreListesiResponse();

            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemebelgelistesiparam _Param = session.Query<tblmalzemebelgelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param == null)
                    {
                        new tblmalzemebelgelistesiparam(session)
                        {
                            aktif = 1,
                            aufnr = "",
                            createuser = "Admin",
                            databasekayitzamani = DateTime.Now,
                            budathigh = "",
                            budatlow = "",
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            iwerk = "",
                            lastupdateuser = "Admin"
                        }.Save();

                    }
                    tblmalzemebelgelistesiparam _Param2 = session.Query<tblmalzemebelgelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param2 != null)
                    {
                        _Tabloyazisi += "<tr>";
                        _Tabloyazisi += "<td>" + _Param2.budatlow + "</td>";
                        _Tabloyazisi += "<td>" + _Param2.budathigh + "</td>";
                        _Tabloyazisi += "<td>" + _Param2.iwerk + "</td>";
                        _Tabloyazisi += "<td>" + _Param2.aufnr + "</td>";
                        _Tabloyazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick = jsBelgeListele();>Güncelle</a></td>";
                        _Tabloyazisi += "</tr>";
                    }


                }

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap.zTabloYazisi = _Tabloyazisi;

            }
            catch (Exception ex)
            {

                _Cevap.zAciklama = ex.ToString();
                _Cevap.zSonuc = -1;
                _Cevap.zTabloYazisi = "";
            }

            return _Cevap;
        }

        internal MalzemeBelgeListesiParametreKayitResponse fn_BelgeDegerleriKaydet(MalzemeBelgeListesiParametreKayitRequest v_gelen)
        {
            MalzemeBelgeListesiParametreKayitResponse _Cevap = new MalzemeBelgeListesiParametreKayitResponse();
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemebelgelistesiparam _Param = session.Query<tblmalzemebelgelistesiparam>().FirstOrDefault(w => w.aktif == 1);
                    if (_Param == null)
                    {
                        new tblmalzemebelgelistesiparam(session)
                        {
                            aktif = 1,
                            aufnr = v_gelen.z_Aufnr,
                            budathigh = v_gelen.z_BudatHigh,
                            budatlow = v_gelen.z_BodatLow,
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            iwerk = v_gelen.z_Iwerk,
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),

                        }.Save();
                    }
                    else
                    {
                        _Param.aufnr = v_gelen.z_Aufnr;
                        _Param.budathigh = v_gelen.z_BudatHigh;
                        _Param.budatlow = v_gelen.z_BodatLow;
                        _Param.iwerk = v_gelen.z_Iwerk;
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
                            islemturu = " Parametreler werks: " + v_gelen.z_Iwerk + " aufnr:" + v_gelen.z_Aufnr + " budathigh :" + v_gelen.z_BudatHigh + "budatlow: " + v_gelen.z_BodatLow +  "olarak guncellendi",
                            islemyapan = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = "",
                            matnr = "",
                            satirid = _Param.id,
                            sernr = "",
                            tabloadi = "tblmalzemebelgelistesiparam"
                        }.Save();


                    }
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;

                }

            }
            catch (Exception)
            {
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";
            }
            return _Cevap;
        }

        internal MalzemeBelgeListesiParamDegerResponse fn_BelgeDegerleriListeDeger(MalzemeBelgeListesiParamDegerRequest v_gelen)
        {
            #region Değişkenler
            MalzemeBelgeListesiParamDegerResponse _Cevap = new MalzemeBelgeListesiParamDegerResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemebelgelistesiparam _Param = session.Query<tblmalzemebelgelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param != null)
                    {
                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;
                        _Cevap.z_Aufnr = _Param.aufnr;
                        _Cevap.z_BudatHigh = _Param.budathigh;
                        _Cevap.z_BudatLow = _Param.budatlow;
                        _Cevap.z_Iwerk = _Param.iwerk;
                    }

                }

            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
                _Cevap.z_Aufnr = "";
                _Cevap.z_BudatHigh = "";
                _Cevap.z_BudatLow = "";
                _Cevap.z_Iwerk = "";

            }
            return _Cevap;
        }
    }
}