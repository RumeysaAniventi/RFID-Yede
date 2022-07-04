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
    public class RdrKimliklendirmeParametreManager
    {
        internal RdrKimliklendirmeParametreKayitResponse fn_RdrKimliklendirmeParametreKayit(RdrKimliklendirmeParametreKayitRequest v_Gelen)
        {
            #region Değişkenler
            RdrKimliklendirmeParametreKayitResponse _Cevap = new RdrKimliklendirmeParametreKayitResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirmeparam _Temp = session.Query<tblreaderkimliklendirmeparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblreaderkimliklendirmeparam(session)
                        {
                            aktif = 1,
                            
                            createuser = "Admin",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            lastupdateuser = "Admin",
                            readerepc=v_Gelen.zRfidId,
                            readerip=v_Gelen.zReaderIp,
                            readerokumagucu=v_Gelen.zReaderPower

                        }.Save();

                    }
                    else
                    {
                        _Temp.readerepc = v_Gelen.zRfidId;
                        _Temp.readerip = v_Gelen.zReaderIp;
                        _Temp.readerokumagucu = v_Gelen.zReaderPower;
                        _Temp.guncellemezamani = DateTime.Now;
                        _Temp.lastupdateuser = "Admin";
                        _Temp.Save();
                    }

                    _Cevap = new RdrKimliklendirmeParametreKayitResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "İşlem Başarılı";
                }

            }
            catch (Exception ex)
            {

                _Cevap = new RdrKimliklendirmeParametreKayitResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

            }
            return _Cevap;
        }

        internal RdrKimliklendirmeParametreDegerResponse fn_RdrKimliklendirmeParametreDeger(RdrKimliklendirmeParametreDegerRequest v_Gelen)
        {
            #region Değişkenler
            RdrKimliklendirmeParametreDegerResponse _Cevap = new RdrKimliklendirmeParametreDegerResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirmeparam _Param = session.Query<tblreaderkimliklendirmeparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param != null)
                    {
                        _Cevap = new RdrKimliklendirmeParametreDegerResponse();
                        _Cevap.zAciklama = "";
                        _Cevap.zReaderIp = _Param.readerip;
                        _Cevap.zReaderPower = _Param.readerokumagucu;
                        _Cevap.zRfidId = _Param.readerepc;
                        _Cevap.zSonuc = 1;
                    }
                }

            }
            catch (Exception ex)
            {
                _Cevap = new RdrKimliklendirmeParametreDegerResponse();
                _Cevap.zAciklama = "";
                _Cevap.zReaderIp = "";
                _Cevap.zReaderPower = "";
                _Cevap.zRfidId = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal RdrKimliklendirmeParametreListesiResponse fn_RdrKimliklendirmeParametreListesi(RdrKimliklendirmeParametreListesiRequest v_Gelen)
        {
            #region
            string _TabloYazisi = "";

            RdrKimliklendirmeParametreListesiResponse _Cevap;
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {

                    // tabloda hiç kayıt yoksa tüm değerleri boş bir kayıt ekliyorum
                    tblreaderkimliklendirmeparam _Param = session.Query<tblreaderkimliklendirmeparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Param == null)
                    {
                        new tblreaderkimliklendirmeparam(session)
                        {
                            createuser = "Admin",
                            lastupdateuser = "Admin",
                            aktif = 1,
                            readerepc = "",
                            databasekayitzamani = DateTime.Now,
                            readerip = "",
                            readerokumagucu = "",
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            
                        }.Save();
                    }

                    tblreaderkimliklendirmeparam _Kayit = session.Query<tblreaderkimliklendirmeparam>().FirstOrDefault(w => w.aktif == 1);



                    if (_Kayit != null)
                    {
                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td>" + _Kayit.readerip + " </td>";
                        _TabloYazisi += "<td>" + _Kayit.readerokumagucu+ " </td>";
                        _TabloYazisi += "<td>" + _Kayit.readerepc + " </td>";
                        _TabloYazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick = jsListele();>Güncelle</a></td>";
                        _TabloYazisi += "</tr>";

                    }

                    _Cevap = new RdrKimliklendirmeParametreListesiResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zTabloYazisi = _TabloYazisi;
                    _Cevap.zAciklama = "";


                }

            }
            catch (Exception ex)
            {
                _Cevap = new RdrKimliklendirmeParametreListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";

                return _Cevap;
            }



            return _Cevap;
        }
    }
}