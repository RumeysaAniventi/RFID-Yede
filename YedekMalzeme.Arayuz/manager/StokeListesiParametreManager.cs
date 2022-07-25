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
    public class StokeListesiParametreManager
    {
        internal StokeListesiParametreKayitResponse StokParametreKayit(StokeListesiParametreKayitRequest v_Gelen)
        {
            StokeListesiParametreKayitResponse _Cevap = new StokeListesiParametreKayitResponse();
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemestoklistesiparam _Temp = session.Query<tblmalzemestoklistesiparam>().FirstOrDefault(w => w.aktif == 1);
                    if (_Temp == null)
                    {
                        new tblmalzemestoklistesiparam(session)
                        {
                            aktif = 1,
                            createuser = "Admin",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            werks = v_Gelen.ziwerk,
                            lastupdateuser = "Admin",
                            lgort = v_Gelen.zlgort,
                            mtart = v_Gelen.zmtart


                        }.Save();
                    }
                    else

                    {
                        _Temp.lgort = v_Gelen.zlgort;
                        _Temp.mtart = v_Gelen.zmtart;
                        _Temp.werks = v_Gelen.ziwerk;
                        _Temp.guncellemezamani = DateTime.Now;
                        _Temp.lastupdateuser = "Admin";

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
                            islemturu = " Parametreler lgort: " + v_Gelen.zlgort + " mtart:" + v_Gelen.zmtart + " werks :" + v_Gelen.ziwerk + " olarak guncellendi",
                            islemyapan = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = "",
                            matnr = "",
                            satirid = _Temp.id,
                            sernr = "",
                            tabloadi = "tblmalzemestoklistesiparam"
                        }.Save();

                        _Temp.Save();

                    }
                    _Cevap = new StokeListesiParametreKayitResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "İşlem Başarılı";
                }
            }
            catch (Exception)
            {
                _Cevap = new StokeListesiParametreKayitResponse();
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;


            }
            return _Cevap;
        }

        internal StokeListesiParametreListesiResponse StokeListesiGetir(StokeListesiParametreListesiRequest v_Gelen)
        {
            #region Değişkenler

            StokeListesiParametreListesiResponse _Cevap = new StokeListesiParametreListesiResponse();

            string _Tabloyazisi = "";
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    //Hiç kayıt yoksa 
                    tblmalzemestoklistesiparam _Temp = session.Query<tblmalzemestoklistesiparam>().FirstOrDefault(w => w.aktif == 1);
                    if (_Temp == null)
                    {
                        new tblmalzemestoklistesiparam(session)
                        {
                            aktif = 1,
                            createuser = "Admin",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            werks = "",
                            lastupdateuser = "Admin",
                            lgort = "",
                            mtart = "",


                        }.Save();
                    }

                    //Kayit varsa Listeliyorum tek tek tabloya yazıyorum
                    tblmalzemestoklistesiparam _Kayit = session.Query<tblmalzemestoklistesiparam>().FirstOrDefault(w => w.aktif == 1);
                    if (_Kayit != null)

                    {
                        _Tabloyazisi += "<tr>";
                        _Tabloyazisi += "<td>" + _Kayit.mtart + " </td>";
                        _Tabloyazisi += "<td>" + _Kayit.werks + " </td>";
                        _Tabloyazisi += "<td>" + _Kayit.lgort + " </td>";
                        _Tabloyazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick = jsStokeListele();>Güncelle</a></td>";
                        _Tabloyazisi += "</tr>";
                    }

                    _Cevap = new StokeListesiParametreListesiResponse();
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                    _Cevap.ztabloyazisi = _Tabloyazisi;

                    return _Cevap;



                }

            }
            catch (Exception)
            {

                _Cevap = new StokeListesiParametreListesiResponse();
                _Cevap.zAciklama = " Stoklar Listelenmedi ";
                _Cevap.zSonuc = -1;
                _Cevap.ztabloyazisi = "";

                return _Cevap;
            }


        }



        internal StokeListesiParametreDegerResponse StokDegerOlustur(StokeListesiParametreDegerRequest v_Gelen)
        {

            StokeListesiParametreDegerResponse _Cevap = new StokeListesiParametreDegerResponse();

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemestoklistesiparam _Param = session.Query<tblmalzemestoklistesiparam>().FirstOrDefault(w => w.aktif == 1);
                    {
                        if (_Param != null)
                        {
                            _Cevap = new StokeListesiParametreDegerResponse();
                            _Cevap.zSonuc = 1;
                            _Cevap.zAciklama = "";
                            _Cevap.ziwerk = _Param.werks;
                            _Cevap.zlgort = _Param.lgort;
                            _Cevap.zmtart = _Param.mtart;


                        }

                    }

                }
                return _Cevap;
            }
            catch (Exception)
            {

                _Cevap = new StokeListesiParametreDegerResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";
                _Cevap.ziwerk = "";
                _Cevap.zlgort = "";
                _Cevap.zmtart = "";
                return _Cevap;
            }
        }


    }
}