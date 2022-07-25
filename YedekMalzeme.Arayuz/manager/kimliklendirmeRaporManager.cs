
using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using YedekMalzeme.Arayuz.View;


namespace YedekMalzeme.Arayuz.manager
{
    public class kimliklendirmeRaporManager
    {
        

        internal KimliklendirmeRaporListeleResponse fn_KimliklendirmeRaporListele(KimliklendirmeRaporListeleRequest v_Gelen)
        {
            #region Değişkenler

            string _TabloYazisi = "";

            string _LinkYazisi = "";

            KimliklendirmeRaporListeleResponse _Cevap = new KimliklendirmeRaporListeleResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<tblmalzemelistesiresponse> _malzemelistesi = session.Query<tblmalzemelistesiresponse>().Where(z => z.aktif == 1 && z.matnr!="").ToList();
                   
                    foreach (var item in _malzemelistesi)
                    {
                        
                        List<tblkimliklendirme> _kimliklendirmeTable = session.Query<tblkimliklendirme>().Where(y => y.aktif == 1 && y.matnr.Equals(item.matnr)).ToList();
                        
                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td style='text-align:center;'>" + item.matnr + "</td>";
                        _TabloYazisi += "<td style='text-align:center;'>" + item.maktx + "</td>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _kimliklendirmeTable.Count() + "</td>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _LinkYazisi + "</td>";
                        _TabloYazisi += "</tr>";
                    }

                    _Cevap.zListeYazisi = _TabloYazisi;
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                }



            }
            catch (Exception ex)
            {
                _Cevap = new KimliklendirmeRaporListeleResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "Sistemsel bir hata oluştu.";
                _Cevap.zListeYazisi = "";

                return _Cevap;
            }

            return _Cevap;
        }

        internal FiltrelenmisKimlikMalzemeListeleResponse fn_FiltrelenmisKimlikMalzemeListele(FiltrelenmisKimlikMalzemeListeleRequest v_Gelen)
        {
            #region  Değişkenler

            FiltrelenmisKimlikMalzemeListeleResponse _Cevap = new FiltrelenmisKimlikMalzemeListeleResponse();
            string _Sql = "";
            cVeriTabaniIslem _myIslem = new cVeriTabaniIslem();
            DataTable _dTable = new DataTable();

            #endregion

            try
            {

                _Sql = " select " +
                        " gelenepc, " +
                        " databasekayitzamani, " +
                        " createuser, " +
                        " matnr, " +
                        " maktx, " +
                        " sernr " +
                        " from tblkimliklendirme " +
                        " where aktif=1 and";


                if (v_Gelen.zkimliklendiren != "")
                {
                    _Sql += " tblkimliklendirme.createuser ilike '%"+ v_Gelen.zkimliklendiren+"%' and";
                }

                if (v_Gelen.zilktarih != "" && v_Gelen.zsontarih!="")
                {
                    _Sql += " tblkimliklendirme.databasekayitzamani between '" + v_Gelen.zilktarih + "' and '" + v_Gelen.zsontarih + "' and";
                }

                if (v_Gelen.zmatnr != "")
                {
                    _Sql += " tblkimliklendirme.matnr ilike '%"+v_Gelen.zmatnr+"%' and";
                }


                List<string> sqllist = new List<string>();
                sqllist = _Sql.Split(' ').ToList();
                sqllist.RemoveAt(sqllist.Count - 1);

                string _newsql = String.Join(" ", sqllist);
                _Sql = _newsql;


                

                NpgsqlCommand _Komut = new NpgsqlCommand(_Sql);

                _Komut.Parameters.Clear();
                if (v_Gelen.zmatnr != "")
                {
                    _Komut.Parameters.AddWithValue("@tblkimliklendirme.matnr", v_Gelen.zmatnr);
                }

                if (v_Gelen.zkimliklendiren != "")
                {
                    _Komut.Parameters.AddWithValue("@tblkimliklendirme.createuser", v_Gelen.zkimliklendiren);
                }

                if (v_Gelen.zilktarih != "" && v_Gelen.zsontarih != "")
                {
                    _Komut.Parameters.AddWithValue("@tblkimliklendirme.databasekayitzamani", Convert.ToDateTime(v_Gelen.zilktarih));
                }

                _dTable = _myIslem._fnDataTable(_Komut);

                _Cevap = new FiltrelenmisKimlikMalzemeListeleResponse();
                _Cevap.zSonuc = 1;
                _Cevap.zDizi = new List<FiltrelenmisKimliklendirmeView>();
                _Cevap.zAciklama = "";

                for (int intSayac = 0; intSayac < _dTable.Rows.Count; intSayac++)
                {
                    _Cevap.zDizi.Add(new FiltrelenmisKimliklendirmeView()
                    {
                        zepc= _dTable.Rows[intSayac]["gelenepc"].ToString(),
                        zkimliktarih= _dTable.Rows[intSayac]["databasekayitzamani"].ToString(),
                        zkimliklendiren= _dTable.Rows[intSayac]["createuser"].ToString(),
                        zmatnr = _dTable.Rows[intSayac]["matnr"].ToString(),
                        zmaktx= _dTable.Rows[intSayac]["maktx"].ToString(),
                        zsern= _dTable.Rows[intSayac]["sernr"].ToString()



                    });
                }

            }
            catch (Exception ex)
            {
                _Cevap = new FiltrelenmisKimlikMalzemeListeleResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();


            }

            return _Cevap;
        }

        internal KimlikRaporFiltrelemeResponse fn_KimlikRaporFiltreleme(KimlikRaporFiltrelemeRequest v_Gelen)
        {
            #region  Değişkenler

            KimlikRaporFiltrelemeResponse _Cevap = new KimlikRaporFiltrelemeResponse();
            string _Sql = "";
            

            cVeriTabaniIslem _myIslem = new cVeriTabaniIslem();

            DataTable _dTable = new DataTable();

            #endregion

            try
            {

                    _Sql = " select " +
                            " tblmalzemelistesiresponse.matnr, " +
                            " tblmalzemelistesiresponse.maktx, " +
                            " count(tblkimliklendirme.matnr) as kimliklimalzemesayisi " +
                            " from tblmalzemelistesiresponse " +
                            " inner join tblkimliklendirme  on tblmalzemelistesiresponse.matnr=tblkimliklendirme.matnr " +
                            " where tblkimliklendirme.aktif=1 and tblmalzemelistesiresponse.aktif=1 and";
                            

                    if (v_Gelen.zkimliklendiren!="")
                    {
                        _Sql += " tblkimliklendirme.createuser ilike '%"+ v_Gelen.zkimliklendiren + "%' and";
                    }

                   if(v_Gelen.zilktarih!="" && v_Gelen.zsontarih!="")
                   {

                    _Sql += " tblkimliklendirme.databasekayitzamani between '"+v_Gelen.zilktarih+"' and '"+v_Gelen.zsontarih+"' and";

                   }

                    if (v_Gelen.zmatnr!="")
                    {
                        _Sql += " tblmalzemelistesiresponse.matnr ilike '%"+v_Gelen.zmatnr+"%' and";
                    }

                    if (v_Gelen.zmaktx!="")
                    {
                        _Sql += " tblmalzemelistesiresponse.maktx  ilike '%"+v_Gelen.zmaktx +"%'  and";

                    }


                    
                    List<string> sqllist = new List<string>();
                    sqllist = _Sql.Split(' ').ToList();
                    sqllist.RemoveAt(sqllist.Count - 1);

                    string _newsql = String.Join(" ", sqllist);
                    _Sql = _newsql;





                    _Sql += " group by tblmalzemelistesiresponse.matnr,tblmalzemelistesiresponse.maktx";

                    NpgsqlCommand _Komut = new NpgsqlCommand(_Sql);

                    _Komut.Parameters.Clear();
                    if (v_Gelen.zmatnr != "")
                    {
                        _Komut.Parameters.AddWithValue("@tblmalzemelistesiresponse.matnr", v_Gelen.zmatnr);
                    }

                    if (v_Gelen.zmaktx != "")
                    {
                        _Komut.Parameters.AddWithValue("@tblmalzemelistesiresponse.maktx", v_Gelen.zmaktx);
                    }

                    if (v_Gelen.zkimliklendiren != "")
                    {
                        _Komut.Parameters.AddWithValue("@tblkimliklendirme.createuser", v_Gelen.zkimliklendiren);
                    }

               
                if (v_Gelen.zilktarih != "" && v_Gelen.zsontarih != "")
                {
                    _Komut.Parameters.AddWithValue("@tblkimliklendirme.databasekayitzamani", Convert.ToDateTime(v_Gelen.zilktarih));
                   // _Komut.Parameters.AddWithValue("@tblkimliklendirme.guncellemezamani", Convert.ToDateTime(v_Gelen.zsontarih));
                }

                _dTable = _myIslem._fnDataTable(_Komut);

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    
                    _Cevap = new KimlikRaporFiltrelemeResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zDizi = new List<KimliklendirmeFiltrelemeView>();
                    _Cevap.zAciklama = "";

                    for (int intSayac = 0; intSayac < _dTable.Rows.Count; intSayac++)
                    {
                        _Cevap.zDizi.Add(new KimliklendirmeFiltrelemeView()
                        {
                            zmatnr = _dTable.Rows[intSayac]["matnr"].ToString(),
                            zmaktx = _dTable.Rows[intSayac]["maktx"].ToString(),
                            zkimliklimalzemesayisi = _dTable.Rows[intSayac]["kimliklimalzemesayisi"].ToString()

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimlikRaporFiltrelemeResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                
            }
            return _Cevap;
        }

        internal KimlikTarihListeleResponse fn_KimlikTarihListele(KimlikTarihListeleRequest v_Gelen)
        {
                        
            #region  Değişkenler
            string _tabloyazisi = "";

            KimlikTarihListeleResponse _cevap = new KimlikTarihListeleResponse();

            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {

                    List<KimliklendirmeTarihView> _ItemDizi = session.Query<tblkimliklendirme>().Where(w => w.aktif == 1).Select(w => new KimliklendirmeTarihView()
                    {
                        zkimliktarih = w.databasekayitzamani.ToString()
                    }).Distinct().OrderByDescending(w => w.zkimliktarih).ToList();

                    _tabloyazisi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in _ItemDizi)
                    {
                        List<string> _temList = new List<string>();
                        _temList = item.zkimliktarih.Split('.').ToList();


                        _tabloyazisi += "<option value='" + item.zkimliktarih + "'>" + _temList[0] + "</option>";
                    }

                    _cevap.ztabloyazisi = _tabloyazisi;
                    _cevap.zSonuc = 1;
                    _cevap.zAciklama = "";



                }
            }
            catch (Exception)
            {

                _cevap.ztabloyazisi = "";
                _cevap.zSonuc = -1;
                _cevap.zAciklama = "";
            }

            return _cevap;
        }

        internal KimliklendirilmisMalzemeListeleResponse fn_KimliklendirilmisMalzemeListele(KimliklendirilmisMalzemeListeleRequest v_Gelen)
        {
            #region Değişkenler

            string _TabloYazisi = "";

            KimliklendirilmisMalzemeListeleResponse _Cevap = new KimliklendirilmisMalzemeListeleResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    _TabloYazisi = "";

                    List<tblkimliklendirme> _ItemDizi = session.Query<tblkimliklendirme>().Where(w => w.aktif == 1 && w.matnr.Equals(v_Gelen.zmatnr)).OrderBy(w => w.matnr).ToList();

                    foreach (var _Item in _ItemDizi)
                    {
                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _Item.gelenepc + "</td>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _Item.databasekayitzamani + "</td>";
                        _TabloYazisi += "<td>" + _Item.createuser + "</td>";
                        _TabloYazisi += "<td style='text-align:right;'>" + _Item.matnr + "</td>";
                        _TabloYazisi += "<td>" + _Item.maktx+ "</td>";
                        _TabloYazisi += "<td style='text-align:right;'>" + _Item.sernr + "</td>";
                        _TabloYazisi += "</tr>";
                    }

                    _Cevap = new KimliklendirilmisMalzemeListeleResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.ztabloyazisi = _TabloYazisi;

                    return _Cevap;
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimliklendirilmisMalzemeListeleResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }

            
        }
    }
}