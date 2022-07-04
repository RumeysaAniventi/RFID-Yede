using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Entity.YedekMalzemeTakip.Md5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.manager
{
    public class kullaniciYetkilendirme1tManager
    {
        internal KisiKaydetResponse fn_KisiKaydet(KisiKaydetRequest v_Gelen)
        {
            KisiKaydetResponse _Cevap = new KisiKaydetResponse();
            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    if (v_Gelen.zadi=="" || v_Gelen.zsoyadi=="" || v_Gelen.zsifre=="" || v_Gelen.zkullaniciadi=="")
                    {
                        _Cevap.zAciklama = "Bilgiler Boş bırakılamaz";
                        _Cevap.zSonuc = -1;
                        return _Cevap;
                    }
                    new tblarayuzkullanici(session)
                    {
                        aktif = 1,
                        createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        databasekayitzamani = DateTime.Now,
                        lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        guncellemezamani = DateTime.Now,
                        id = Guid.NewGuid().ToString().ToUpper(),
                        kullaniciadi = v_Gelen.zkullaniciadi,
                        soyadi = v_Gelen.zsoyadi,
                        adi = v_Gelen.zadi,
                        sifre = EncryptionHelper.ToMD5(v_Gelen.zsifre).ToUpper(),
                        yetki = v_Gelen.zyetki

                    }.Save();

                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;



                }

            }
            catch (Exception ex )
            {
                _Cevap.zAciklama = ex.ToString();
                _Cevap.zSonuc =- 1;
            }

            return _Cevap;
        }

        internal SifreYenilemeResponse fn_SifreYenileme(SifreYenilemeRequest v_gelen)
        {
            SifreYenilemeResponse _Cevap = new SifreYenilemeResponse();
            string _YeniSifre = "";
            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.id.Equals(v_gelen.zid));

                    if (_Kullanici!=null)
                    {
                        _YeniSifre = Guid.NewGuid().ToString();
                        char[] _Dizi1 = _YeniSifre.ToCharArray(0,4);

                        string _Yenisifre1 = new string(_Dizi1);
                      

                        _Kullanici.sifre = EncryptionHelper.ToMD5(_Yenisifre1);
                        _Kullanici.lastupdateuser=HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _Kullanici.guncellemezamani = DateTime.Now;
                        _Kullanici.Save();

                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;
                        _Cevap.zYenisifre = _Yenisifre1;
                    }
                   
                }

            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
                _Cevap.zYenisifre = "";
            }

            return _Cevap;
        }

        internal KisiListele1Response fn_KisiListele(KisiListele1Request v_Gelen)
        {
            KisiListele1Response _Cevap = new KisiListele1Response();

            string _TabloYazisi = "";
            try
            {
                using (Session session =XpoManager.Instance.GetNewSession())
                {
                    List<tblarayuzkullanici> _KullaniciListesi = session.Query<tblarayuzkullanici>().Where(k => k.aktif == 1).ToList();

                    foreach (var Item in _KullaniciListesi)
                    {
                        if (_KullaniciListesi != null)
                        {
                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center; display:none;'>" + Item.id + "</td>";
                            _TabloYazisi += "<td style='text-align:center;' id='" + Item.id + "'>" + Item.adi + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + Item.soyadi + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + Item.kullaniciadi + "</td>";

                            if (Item.yetki == "1")
                            {
                                _TabloYazisi += "<td style='text-align:center;'>Admin</td>";
                            }
                            else
                            {
                                _TabloYazisi += "<td style='text-align:center;'>Kullanıcı</td>";
                            }

                            _TabloYazisi += "<td style='text-align:center;'>...</td>";


                        }
                    }


                }

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap.ztabloyazisi = _TabloYazisi;
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
                _Cevap.ztabloyazisi = "";
            }
            return _Cevap;
        }

        internal YetkiGuncelleModalAcResponse fn_YetkiGuncelleModalAc(YetkiGuncelleModalAcRequest v_Gelen)
        {
          YetkiGuncelleModalAcResponse _Cevap = new YetkiGuncelleModalAcResponse();
            try
            {

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1&&k.id.Equals(v_Gelen.zid));

                    if (_Kullanici!=null)
                    {
                        _Cevap.zAdi = _Kullanici.adi;
                        _Cevap.zSoyAdi = _Kullanici.soyadi;
                        _Cevap.zKullaniciAdi = _Kullanici.kullaniciadi;
                        _Cevap.zYetki = _Kullanici.yetki;
                        _Cevap.zid = v_Gelen.zid;
                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;

                    }
                }
              

;            }
            catch (Exception)
            {
                _Cevap.zid = "";
                _Cevap.zAdi = "";
                _Cevap.zKullaniciAdi = "";
                _Cevap.zSoyAdi = "";
                _Cevap.zYetki = "";
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }
           return _Cevap;
        }

        internal YetkiGuncelleResponse fn_YetkiGuncelle(YetkiGuncelleRequest v_Gelen)
        {
            YetkiGuncelleResponse _Cevap = new YetkiGuncelleResponse();
           
            try
            {
                
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.id.Equals(v_Gelen.zid));

                    if (_Kullanici != null)
                    {
                        
                        if (v_Gelen.zad != "")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.adi = v_Gelen.zad;
                            _Kullanici.Save();
                        }

                        if (v_Gelen.zsoyad != "")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.soyadi = v_Gelen.zsoyad;
                            _Kullanici.Save();
                        }
                        if (v_Gelen.zkullaniciadi != "")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.kullaniciadi = v_Gelen.zkullaniciadi;
                            _Kullanici.Save();
                        }
                        if (v_Gelen.zyetki != "")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.yetki = v_Gelen.zyetki;
                            _Kullanici.Save();

                        }
                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;
                        _Cevap.zid = v_Gelen.zid;

                    }
                }
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "Güncelleme işlemi başarısız";
                _Cevap.zSonuc = -1;
                _Cevap.zid = "";
            }


            return _Cevap;
        }

        internal YetkiSilResponse fn_YetkiSil(YetkiSilRequest v_Gelen)
        {
            YetkiSilResponse _Cevap = new YetkiSilResponse();

            try
            {
                using (Session session =XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.id == v_Gelen.zid);

                    if (_Kullanici !=null)
                    {
                        _Kullanici.aktif = 0;
                        _Kullanici.Save();

                    }

                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;

                }
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal KisiListeleResponse fn_KisiListele(KisiListeleRequest v_Gelen)
        {
            KisiListeleResponse _Cevap = new KisiListeleResponse();
            string _TabloYazisi = "";
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<tblarayuzkullanici> _Kullanici = session.Query<tblarayuzkullanici>().Where(k => k.aktif == 1).ToList();

                    foreach (var Item in _Kullanici)
                    {

                        if (_Kullanici != null)
                        {
                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center; display:none;'>" + Item.id + "</td>";
                            _TabloYazisi += "<td style='text-align:center;' id='"+Item.id+"'>" + Item.adi + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + Item.soyadi + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + Item.kullaniciadi + "</td>";
                           
                            if (Item.yetki=="1")
                            {
                                _TabloYazisi += "<td style='text-align:center;'>Admin</td>";
                            }
                            else
                            {
                                _TabloYazisi += "<td style='text-align:center;'>Kullanıcı</td>";
                            }
                           
                            _TabloYazisi += "<td style='text-align:center;'><i class='la la-cog'></i></i><a class='m-linkm-badge m-badgelinkm--icon m-link m-badge--icon-only  m-badge--danger m-badge--wide' href=# onclick  = JsGuncelleModalAc('" + Item.id + "');>GÜNCELLE</a></td>";
                            _TabloYazisi += "<td style='text-align:center;'><i class='la la-trash-o'></i></i><a class='m-linkm-badge m-badgelinkm--icon m-link m-badge--icon-only  m-badge--danger m-badge--wide' href=# onclick  = jsYetkiSil('" + Item.id + "');>SİL</a></td>";
                          


                            //   _TabloYazisi += "<td style='text-align:center;'><a class='m-link' href=# onclick='jsYetkiSil(" + Item.id + ");>SİL</a></td>";
                            _TabloYazisi += "</tr>";

                            //  _TabloYazisi += "<td style='text-align:center;' onclick='jsYetkiSil( " + Item.id + ")' >SİL</td>";
                            // _TabloYazisi += " <td <button type='button' class='btn btn-outline-warning m-btn m-btn--icon m-btn--icon-only m-btn--custom ' onclick='jsYetkiSil("+ Item.id +"); '><i class='la la-folder'> </i></button></td>";

                            //"<td style='text-align:center;'>< a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick='jsYetkiGuncelle(" + Item.id + ")'>GÜNCELLE</a></td>";


                            //"<td style='text-align:center;' onclick='jsYetkiGuncelle( " + Item.id + ")' >Güncelle</td>";
                            //< a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick='jsYetkiGuncelle( " + Item.id + ")';>Alarm Kapat</a>
                        }



                    }



                }

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap.ztabloyazisi = _TabloYazisi;
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
                _Cevap.ztabloyazisi = "";

            }

            return _Cevap;
        }
    }
}