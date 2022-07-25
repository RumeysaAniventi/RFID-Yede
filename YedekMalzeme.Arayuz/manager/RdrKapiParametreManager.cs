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
    public class RdrKapiParametreManager
    {
        internal RdrKapiParametreKayitResponse fn_RdrKapiParametreDeger(RdrKapiParametreKayitRequest v_Gelen)
        {
            #region Değişkenler
            RdrKapiParametreKayitResponse _Cevap = new RdrKapiParametreKayitResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkapiparam _Temp = session.Query<tblreaderkapiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblreaderkapiparam(session)
                        {
                            aktif = 1,

                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            kapiepc = v_Gelen.zRfidId,
                            kapiip = v_Gelen.zReaderIp,
                            kapiokumagucu = v_Gelen.zReaderPower

                        }.Save();

                    }
                    else
                    {
                        _Temp.kapiepc = v_Gelen.zRfidId;
                        _Temp.kapiip = v_Gelen.zReaderIp;
                        _Temp.kapiokumagucu = v_Gelen.zReaderPower;
                        _Temp.guncellemezamani = DateTime.Now;
                        _Temp.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _Temp.Save();

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
                            islemturu = " Parametreler kapiepc: " + v_Gelen.zRfidId + " kapiip:" + v_Gelen.zReaderIp + " kapiokumagucu :" + v_Gelen.zReaderPower+ " olarak guncellendi",
                            islemyapan = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = "",
                            matnr = "",
                            satirid = _Temp.id,
                            sernr = "",
                            tabloadi = "tblreaderkapiparam"
                        }.Save();

                    }


                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "İşlem Başarılı";
                }

            }
            catch (Exception ex)
            {
                
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

            }
            return _Cevap;
        }

        internal RdrKapiParametreDegerResponse fn_RdrKapiParametreDeger(RdrKapiParametreDegerRequest v_Gelen)
        {
            #region Değişkenler
            RdrKapiParametreDegerResponse _Cevap = new RdrKapiParametreDegerResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkapiparam _Param = session.Query<tblreaderkapiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param != null)
                    {
                        _Cevap = new RdrKapiParametreDegerResponse();
                        _Cevap.zAciklama = "";
                        _Cevap.zReaderIp = _Param.kapiip;
                        _Cevap.zReaderPower = _Param.kapiokumagucu;
                        _Cevap.zRfidId = _Param.kapiepc;
                        _Cevap.zSonuc = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                _Cevap = new RdrKapiParametreDegerResponse();
                _Cevap.zAciklama = "";
                _Cevap.zReaderIp = "";
                _Cevap.zReaderPower = "";
                _Cevap.zRfidId = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal RdrKapiParametreListesiResponse fn_RdrKapiParametreListesi(RdrKapiParametreListesiiRequest v_Gelen)
        {
            #region
            string _TabloYazisi = "";

            RdrKapiParametreListesiResponse _Cevap;
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {

                    // tabloda hiç kayıt yoksa tüm değerleri boş bir kayıt ekliyorum
                    tblreaderkapiparam _Param = session.Query<tblreaderkapiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param == null)
                    {
                        new tblreaderkapiparam(session)
                        {
                            createuser = "Admin",
                            lastupdateuser = "Admin",
                            aktif = 1,
                            kapiepc = "",
                            databasekayitzamani = DateTime.Now,
                            kapiip = "",
                            kapiokumagucu = "",
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),

                        }.Save();
                    }

                    tblreaderkapiparam _Kayit = session.Query<tblreaderkapiparam>().FirstOrDefault(w => w.aktif == 1);



                    if (_Kayit != null)
                    {
                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td>" + _Kayit.kapiip + " </td>";
                        _TabloYazisi += "<td>" + _Kayit.kapiokumagucu + " </td>";
                        _TabloYazisi += "<td>" + _Kayit.kapiepc + " </td>";
                        _TabloYazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick = jsListele();>Güncelle</a></td>";
                        _TabloYazisi += "</tr>";

                    }

                    _Cevap = new RdrKapiParametreListesiResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zTabloYazisi = _TabloYazisi;
                    _Cevap.zAciklama = "";


                }

            }
            catch (Exception ex)
            {
                _Cevap = new RdrKapiParametreListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";

                return _Cevap;
            }



            return _Cevap;
        }
    }
}