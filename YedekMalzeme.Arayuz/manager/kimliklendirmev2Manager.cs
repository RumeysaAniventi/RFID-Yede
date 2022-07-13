using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using YedekMalzeme.Arayuz.View;

namespace YedekMalzeme.Arayuz.manager
{
    public class kimliklendirmev2Manager
    {
        internal KimlikKimliklendirResponse fn_KimlikKimliklendir(KimlikKimliklendirRequest v_gelen)
        {
            #region Değişkenler
            KimlikKimliklendirResponse _Cevap = new KimlikKimliklendirResponse();
            #endregion

            #region Kontroller
            v_gelen.zepc = v_gelen.zepc.Trim();
            v_gelen.zmatnr = v_gelen.zmatnr.Trim();
            v_gelen.zmaktx = v_gelen.zmaktx.Trim();
            v_gelen.zsernr = v_gelen.zsernr.Trim();

            #endregion

            if (v_gelen.zepc.Length != 24)
            {
                _Cevap = new KimlikKimliklendirResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "EPC Değeri 24 hane olmalıdır";

                return _Cevap;
            }

            if (v_gelen.zmatnr.Length < 5)
            {
                _Cevap = new KimlikKimliklendirResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "Lütfen malzeme seçiniz";

                return _Cevap;
            }

            if (v_gelen.zsernr.Equals("-1"))
            {
                _Cevap = new KimlikKimliklendirResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "Lütfen serino seçiniz";

                return _Cevap;
            }

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiresponse _Temp = session.Query<tblmalzemelistesiresponse>().FirstOrDefault(w => w.aktif == 1 && w.matnr.Equals(v_gelen.zmatnr));
                    if (_Temp == null)
                    {
                        _Cevap = new KimlikKimliklendirResponse();
                        _Cevap.zSonuc = -1;
                        _Cevap.zAciklama = "Kodu "+ v_gelen .zmatnr+ " olan malzeme bulunamadı ";

                        return _Cevap;
                    }

                    tblmalzemeserialstoklistesi _Kontrol = session.Query<tblmalzemeserialstoklistesi>().FirstOrDefault(w => w.aktif == 1 && w.matnr.Equals(v_gelen.zmatnr) && w.sernr.Equals(v_gelen.zsernr));
                    if (_Kontrol == null)
                    {
                        _Cevap = new KimlikKimliklendirResponse();
                        _Cevap.zSonuc = -1;
                        _Cevap.zAciklama = "Kodu " + v_gelen.zmatnr + " ve seri numarası "+ v_gelen.zsernr + " olan malzeme bulunamadı ";

                        return _Cevap;
                    }

                    tbl06analiz _KimlikTemp = session.Query<tbl06analiz>().FirstOrDefault(k =>k.aktif==1 && k.epc.Equals(v_gelen.zepc));
                    if (_KimlikTemp != null)
                    {
                        if ( _KimlikTemp.tuketim == 0)
                        {
                            _Cevap = new KimlikKimliklendirResponse();
                            _Cevap.zSonuc = -1;
                            _Cevap.zAciklama = "Bu etiket zaten kimliklendirilmiş bir ürün";
                        }
                       
                    }


                    //tblkimliklendirme _KimlikTemp = session.Query<tblkimliklendirme>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(v_gelen.zepc));
                    //if (_KimlikTemp != null)
                    //{
                    //    _Cevap = new KimlikKimliklendirResponse();
                    //    _Cevap.zSonuc = -1;
                    //    _Cevap.zAciklama = "Bu etiket daha önceden kimliklendirilmiş";

                    //    return _Cevap;
                    //}

                    tblkimliklendirme _KimlikTemp2 = session.Query<tblkimliklendirme>().FirstOrDefault(w => w.aktif == 1 && w.maktx.Equals(v_gelen.zmaktx)&& w.matnr.Equals(v_gelen.zmatnr) && w.sernr.Equals(v_gelen.zsernr));
                    if (_KimlikTemp2 != null)
                    {
                        _Cevap = new KimlikKimliklendirResponse();
                        _Cevap.zSonuc = -1;
                        _Cevap.zAciklama = "Bu seri numarası daha önceden kimliklendirilmiş";

                        return _Cevap;
                    }

                    tbl06analiz _tuketilmismalzeme = session.Query<tbl06analiz>().FirstOrDefault(t => t.aktif == 1  && t.sernr.Equals(v_gelen.zsernr) && t.matnr.Equals(v_gelen.zmatnr) && t.maktx.Equals(v_gelen.zmaktx));

                    if (_tuketilmismalzeme !=null)
                    {
                        if (_tuketilmismalzeme.tuketim==1)
                        {
                            _Cevap = new KimlikKimliklendirResponse();
                            _Cevap.zSonuc = -1;
                            _Cevap.zAciklama = "Bu seri numaralı ürün tüketilmiştir. Yeniden kimliklendiremezsiniz";
                        }
                    }

                    new tblkimliklendirme(session)
                    {
                        aktif = 1,
                        sernr = v_gelen.zsernr,
                        gelenepc = v_gelen.zepc,
                        createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        databasekayitzamani = DateTime.Now,
                        guncellemezamani = DateTime.Now,
                        id = Guid.NewGuid().ToString().ToUpper(),
                        kullaniciadi = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        maktx = v_gelen.zmaktx,
                        matnr = v_gelen.zmatnr
                    }.Save();

                    new tbl06analiz(session)
                    {
                        id = Guid.NewGuid().ToString().ToUpper(),
                        createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        aktif = 1,
                        databasekayitzamani = DateTime.Now,
                        guncellemezamani = DateTime.Now,
                        epc = v_gelen.zepc,
                        sernr = v_gelen.zsernr,
                        aufnr = "ilişkisiz",
                        matnr = v_gelen.zmatnr,
                        maktx = v_gelen.zmaktx,
                        iliskilendiren = "yok",
                        iliskiiptaleden = "yok",
                        iliskiyeri = 0,
                        kimliklendiren = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        kimlikiptaleden = "yok",
                        tuketim = 0,
                        gecisizni = 0,
                        okumabaslangic = DateTime.MinValue,
                        okumabitis = DateTime.MaxValue,
                        alarmdurum = 0,
                        alarmkapatmatarih = DateTime.MinValue,
                        alarmkapatmaaciklama = "",
                        alarmkapatan = "yok",
                        kapireader=0,
                        

                    }.Save();



                   // new tbl04arsivkimliklendirmeiptal(session)().s

                    _Cevap = new KimlikKimliklendirResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimlikKimliklendirResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();
            }

            return _Cevap;
        }

        internal SecimiTamamlaResponse fn_SecimiTamamla(SecimiTamamlaRequest v_gelen)
        {
            SecimiTamamlaResponse _Cevap = new SecimiTamamlaResponse();

            try
            {
                _Cevap.zmatnr = v_gelen.zmatnr;
                _Cevap.zmaktx = v_gelen.zmaktx;
                _Cevap.zSonuc = 1;
                _Cevap.zAciklama = "";
            }
            catch (Exception)
            {

                _Cevap.zmatnr = "";
                _Cevap.zmaktx = "";
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";
            }
            return _Cevap;

        }

        internal MalzemeAdiBulResponse fn_MalzemeAdiBul(MalzemeAdiBulRequest v_gelen)
        {
            MalzemeAdiBulResponse _Cevap = new MalzemeAdiBulResponse();

            try
            {
                using (Session session =XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiresponse secilimalzeme = session.Query<tblmalzemelistesiresponse>().FirstOrDefault(s => s.aktif == 1 && s.matnr.Equals(v_gelen.zmatnr));

                    _Cevap.zmaktx= secilimalzeme.maktx;
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                }
            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zmaktx = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal MalzemeKoduBulResponse fn_MalzemeKoduBul(MalzemeKoduBulRequest v_gelen)
        {
            MalzemeKoduBulResponse _Cevap = new MalzemeKoduBulResponse();

            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiresponse secilimalzeme = session.Query<tblmalzemelistesiresponse>().FirstOrDefault(m => m.aktif == 1 && m.maktx.Equals(v_gelen.zmaktx)) ;

                    _Cevap.zmatnr = secilimalzeme.matnr;
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                }
            }
            catch (Exception)
            {
                _Cevap.zmatnr = "";
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";


            }
            return _Cevap;
        }

        internal MazlemeListeleResponse fn_MazlemeListele(MazlemeListeleRequest v_gelen)
        {
            MazlemeListeleResponse _Cevap = new MazlemeListeleResponse();
            string _MalzemeAdiListesi = "";
            string _MalzemeKoduListesi = "";

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<MalzemeKoduListeleView> MalzemeKoduListesi = session.Query<tblmalzemelistesiresponse>().Where(m => m.aktif == 1).Select(m => new MalzemeKoduListeleView()
                    {
                        zmatnr = m.matnr
                    }).Distinct().OrderBy(m=>m.zmatnr).ToList();


                    _MalzemeKoduListesi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in MalzemeKoduListesi)

                        _MalzemeKoduListesi += "<option value='" + item.zmatnr + "'>" + item.zmatnr + "</option>";

                    List<MalzemeAdiListeleView> MalzemeAdiListesi = session.Query<tblmalzemelistesiresponse>().Where(m => m.aktif == 1).Select(m => new MalzemeAdiListeleView()
                    {
                        zMalzemeAdi = m.maktx
                    }).Distinct().OrderBy(m => m.zMalzemeAdi).ToList();


                    _MalzemeAdiListesi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in MalzemeAdiListesi)

                        _MalzemeAdiListesi += "<option value='" + item.zMalzemeAdi + "'>" + item.zMalzemeAdi + "</option>";
                }


                _Cevap.zSonuc = 1;
                _Cevap.zAciklama = "";
                _Cevap.zmalzemkodulistesi = _MalzemeKoduListesi;
                _Cevap.zmalzemeListesi = _MalzemeAdiListesi;

            }
            catch (Exception)
            {

                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";
                _Cevap.zmalzemkodulistesi = "";
                _Cevap.zmalzemeListesi = "";
            }
            return _Cevap;
        }

     

        internal MalzemeAdiListeleResponse fn_MalzemeAdiListele(MalzemeAdiListeleRequest v_gelen)
        {
            MalzemeAdiListeleResponse _Cevap = new MalzemeAdiListeleResponse();

            string _MalzemeListesi = "";
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<MalzemeAdiListeleView> MalzemeListesi = session.Query<tblmalzemelistesiresponse>().Where(m => m.aktif == 1).Select(m => new MalzemeAdiListeleView()
                    {
                        zMalzemeAdi = m.maktx
                    }).Distinct().ToList();


                    _MalzemeListesi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in MalzemeListesi)

                        _MalzemeListesi += "<option value='" + item.zMalzemeAdi + "'>" + item.zMalzemeAdi + "</option>";
                }

                _Cevap.zAciklama = "";
                _Cevap.zmalzemkodulistesi = _MalzemeListesi;
                _Cevap.zSonuc = 1;

            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zmalzemkodulistesi = "";
                _Cevap.zSonuc = -1;

            }

            return _Cevap;

        }

        internal MalzemeKoduListeleResponse fn_MalzemeKoduListele(MalzemeKoduListeleRequest v_gelen)
        {
            MalzemeKoduListeleResponse _Cevap = new MalzemeKoduListeleResponse();
            string _MalzemeListesi = "";

            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    List<MalzemeKoduListeleView> MalzemeListesi = session.Query<tblmalzemelistesiresponse>().Where(m => m.aktif == 1).Select(m => new MalzemeKoduListeleView()
                    {
                        zmatnr = m.matnr
                    }).Distinct().ToList();


                    _MalzemeListesi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in MalzemeListesi)

                        _MalzemeListesi += "<option value='" + item.zmatnr + "'>" + item.zmatnr + "</option>";
                }

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap.zmalzemeListesi = _MalzemeListesi;


            }
            catch (Exception)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
                _Cevap.zmalzemeListesi = "";

            }

            return _Cevap;

        }

        internal KimlikSeriNoListeResponse fn_KimlikSeriNoListe(KimlikSeriNoListeRequest v_gelen)
        {
            #region Değişkenler
            KimlikSeriNoListeResponse _Cevap = new KimlikSeriNoListeResponse();

            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                   List<tblmalzemeserialstoklistesi> _Temp = session.Query<tblmalzemeserialstoklistesi>().Where(w => w.aktif == 1 && w.matnr.Equals(v_gelen.zmatnr)).ToList();

                    _Cevap = new KimlikSeriNoListeResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                    _Cevap.zDizi = new List<ViewSeriNo>();

                    foreach (var item in _Temp)
                    {

                        tblkimliklendirme _KmlTemp = session.Query<tblkimliklendirme>().FirstOrDefault(w => w.aktif == 1 && w.sernr.Equals(item.sernr));

                        if (_KmlTemp == null)
                        {
                            _Cevap.zDizi.Add(new ViewSeriNo()
                            {
                                zsernrad = item.sernr,
                                zsernrkod = item.sernr
                            });
                        }
                        else
                        {
                            _Cevap.zDizi.Add(new ViewSeriNo()
                            {
                                zsernrad = item.sernr +" (Kimliklendirilmiş)",
                                zsernrkod = item.sernr
                            });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimlikSeriNoListeResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();
                _Cevap.zDizi = new List<ViewSeriNo>();


              
            }


            return _Cevap;
        }

       

        internal KimliklMalzemeListesiResponse fn_KimliklMalzemeListesi(KimliklMalzemeListesiRequest v_gelen)
        {
            #region Değişkenler

            string _TabloYazisi = "";
            string _Sql = "";

            NpgsqlCommand _Komut = new NpgsqlCommand();

            DataTable _dTable = new DataTable();

            cVeriTabaniIslem _myIslen = new cVeriTabaniIslem();

            KimliklMalzemeListesiResponse _Cevap = new KimliklMalzemeListesiResponse();

            #endregion

            try
            {
                _Sql = "select matnr, maktx from tblmalzemelistesiresponse where aktif = 1 ";

                if (v_gelen.zmalzemeadi.Trim().Length > 1)
                {
                    _Sql += " AND upper(maktx) like '%" + v_gelen.zmalzemeadi.ToUpper() + "%'";
                }

                if (v_gelen.zmalzemekodu.Trim().Length > 1)
                {
                    _Sql += " AND matnr like '%" + v_gelen.zmalzemekodu + "%'";
                }

                _Komut = new NpgsqlCommand();
                _Komut.CommandText = _Sql;
                _Komut.Parameters.Clear();

                _dTable = _myIslen._fnDataTable(_Komut);

                _TabloYazisi = "";

                 _Cevap = new KimliklMalzemeListesiResponse();
                _Cevap.zSonuc = 1;
                _Cevap.zAciklama = "";
                _Cevap.zDizi = new List<ViewMalzeme>();

                for (int intSayac = 0; intSayac < _dTable.Rows.Count; intSayac++)
                {
                    _Cevap.zDizi.Add(new ViewMalzeme()
                    {
                        zmaktx = _dTable.Rows[intSayac]["maktx"].ToString().Trim(),
                        zmatnr = _dTable.Rows[intSayac]["matnr"].ToString().Trim(),
                    });
                }  
            }
            catch (Exception ex)
            {
                _Cevap = new KimliklMalzemeListesiResponse();
                _Cevap.zAciklama = ex.ToString();
                _Cevap.zSonuc = -1;
                _Cevap.zDizi = new List<ViewMalzeme>();               
            }


            return _Cevap;

        }

        internal KimlikEpcGetirResponse fn_KimlikEpcGetir(KimlikEpcGetirRequest v_gelen)
        {
            #region Değişkenler

            KimlikEpcGetirResponse _Cevap = new KimlikEpcGetirResponse();

            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirme _Temp = session.Query<tblreaderkimliklendirme>().Where(w => w.aktif == 1).OrderByDescending(w => w.sonokuma).FirstOrDefault();

                    if (_Temp != null)
                    {
                        _Cevap = new KimlikEpcGetirResponse();
                        _Cevap.zepc = _Temp.gelenepc;
                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "";
                    }

                    else
                    {
                        _Cevap = new KimlikEpcGetirResponse();
                        _Cevap.zepc ="";
                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "";
                    }
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimlikEpcGetirResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }


            return _Cevap;
        }
    }
}