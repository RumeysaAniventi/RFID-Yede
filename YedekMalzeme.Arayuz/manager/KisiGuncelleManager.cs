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
    public class KisiGuncelleManager
    {
        internal KisiGetirResponse fn_KisiGetir(KisiGetirRequest v_gelen)
        {
            KisiGetirResponse _Cevap = new KisiGetirResponse();
            String kisi= HttpContext.Current.Session["KullaniciAdi"].ToString();

            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.kullaniciadi.Equals(kisi));

                    _Cevap.zadi = _Kullanici.adi;
                    _Cevap.zsoyadi = _Kullanici.soyadi;
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                }
            }
            catch (Exception)
            {

                _Cevap.zadi ="";
                _Cevap.zsoyadi = "";
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }
            return _Cevap;
        }

        internal KullaniciGuncelleResponse fn_KullaniciGuncelle(KullaniciGuncelleEtRequest v_gelen)
        {
            KullaniciGuncelleResponse _Cevap = new KullaniciGuncelleResponse();
            try
            {
                String kisi = HttpContext.Current.Session["tblkullaniciid"].ToString();

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 && k.id.Equals(kisi));

                    if (_Kullanici!=null)
                    {
                        if (v_gelen.zad!="" )
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.adi = v_gelen.zad;
                            _Kullanici.Save();
                        }

                        if (v_gelen.zsoyad!="")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.soyadi = v_gelen.zsoyad;
                            _Kullanici.Save();
                        }
                        if (v_gelen.zkullanici !="")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.kullaniciadi = v_gelen.zkullanici;
                            _Kullanici.Save();
                        }
                        if (v_gelen.zsifre !="")
                        {
                            _Kullanici.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                            _Kullanici.guncellemezamani = DateTime.Now;
                            _Kullanici.sifre = EncryptionHelper.ToMD5( v_gelen.zsifre);
                            _Kullanici.Save();

                        }
                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;

                    }
                }
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "Güncelleme işlemi başarısız";
                _Cevap.zSonuc = -1;
            }


            return _Cevap;
        }

        internal KisiKontrolEtResponse fn_KisiKontrolEt(KisiKontrolEtRequest v_gelen)
        {
            KisiKontrolEtResponse _Cevap = new KisiKontrolEtResponse();
            string _Sifre = "";
            

            try
            {
                _Sifre = EncryptionHelper.ToMD5( v_gelen.zsifre);
                using (Session session=XpoManager.Instance.GetNewSession())
                {

                    tblarayuzkullanici _Kullanici = session.Query<tblarayuzkullanici>().FirstOrDefault(k => k.aktif == 1 &&k.kullaniciadi.Equals(v_gelen.zkullanici) && k.sifre.Equals(_Sifre));

                    if (_Kullanici!=null)
                    {
                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;
                    }
                    else
                    {
                        _Cevap.zAciklama = "Lütfen Kullanıcı Adı ve Şifrenizi doğru girdiğinizden emin olun.";
                        _Cevap.zSonuc = -1;
                    }
                }

            }
            catch (Exception)
            {
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;


            }  
            return _Cevap;
        }
    }
}