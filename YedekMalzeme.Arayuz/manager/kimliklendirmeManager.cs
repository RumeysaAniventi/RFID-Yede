using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using System.Linq;
using Npgsql;
using System.Data;

namespace YedekMalzeme.Arayuz.manager
{
    public class kimliklendirmeManager
    {
        internal KimliklendirmeMalzemeListesiResponse fn_KimliklendirmeMalzemeListesi(KimliklendirmeMalzemeListesiRequest v_gelen)
        {
            #region Değişkenler
            string _TabloYazisi = "";
            string _LinkYazisi = "";

            KimliklendirmeMalzemeListesiResponse _Cevap = new KimliklendirmeMalzemeListesiResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    _TabloYazisi = "";

                    List<tblmalzemelistesiresponse> _ItemDizi = session.Query<tblmalzemelistesiresponse>().Where(w => w.aktif == 1).OrderBy(w => w.matnr).ToList();

                    foreach (var _Item in _ItemDizi)
                    {
                        _LinkYazisi = "<a class='m-link' href=# onclick =jsIncele('" + _Item.matnr + "') >İncele</a>";

                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _Item.matnr + "</td>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _Item.maktx + "</td>";
                        _TabloYazisi += "<td style='text-align:center;'>" + _LinkYazisi + "</td>";
                        _TabloYazisi += "</tr>";
                    }

                    _Cevap = new KimliklendirmeMalzemeListesiResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.ztabloyazisi = _TabloYazisi;

                    return _Cevap;
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimliklendirmeMalzemeListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }
            return _Cevap;


        }

        internal KimliklendirmeMalzemeListesiResponse fn_KimliklendirmeMalzemeListesi_v2(KimliklendirmeMalzemeListesiRequest v_gelen)
        {
            #region Değişkenler
            string _TabloYazisi = "";
            string _LinkYazisi = "";

            string _Sql = "";

            

            KimliklendirmeMalzemeListesiResponse _Cevap = new KimliklendirmeMalzemeListesiResponse();
            #endregion

            try
            {

                cVeriTabaniIslem _myIslem = new cVeriTabaniIslem();

                using (NpgsqlConnection con = new NpgsqlConnection(_myIslem._BaglantiString()))
                {
                    _Sql = "select matnr,maktx from tblmalzemelistesiresponse";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(_Sql, con))
                    {
                        cmd.CommandType = CommandType.Text;
                        con.Open();
                        NpgsqlDataReader _Reader = cmd.ExecuteReader();

                        _TabloYazisi = "";

                        while (_Reader.Read())
                        {
                            _LinkYazisi = "<a class='m-link' href=# onclick =jsIncele('" + _Reader["matnr"]+ "') >İncele</a>";

                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Reader["matnr"].ToString().Trim() + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Reader["maktx"].ToString().Trim() + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _LinkYazisi + "</td>";
                            _TabloYazisi += "</tr>";
                        }

                        _Cevap = new KimliklendirmeMalzemeListesiResponse();
                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "";
                        _Cevap.ztabloyazisi = _TabloYazisi;                     
                    }
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimliklendirmeMalzemeListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }
            return _Cevap;


        }
    }
}