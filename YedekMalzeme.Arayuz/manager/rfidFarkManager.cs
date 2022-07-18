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
    internal class rfidFarkManager
    {
        internal RfidFarkListesiResponse fn_RfidFarkListesi(RfidFarkListesiRequest v_Gelen)
        {
            RfidFarkListesiResponse _Cevap = new RfidFarkListesiResponse();

            string _Sql = "";
            //SqlCommand _Komut = new SqlCommand();

            DataTable _dTable = new DataTable();
            cVeriTabaniIslem _myIslem = new cVeriTabaniIslem();

            try
            {

                _Sql += "select * from tbl07sapfark";

                //if (!String.IsNullOrEmpty(v_Gelen.zilktarih) && !String.IsNullOrEmpty(v_Gelen.zsontarih))
                //{

                //    _Sql += "and okumabitis between '" + v_Gelen.zilktarih + "' and '" + v_Gelen.zsontarih + "' ";

                //}

                //if (!String.IsNullOrEmpty(v_Gelen.zaufnr))
                //{
                //    _Sql += "and aufnr ilike '%" + v_Gelen.zaufnr + "%' ";
                //}



                //if (!String.IsNullOrEmpty(v_Gelen.zmatnr))
                //{
                //    _Sql += "and matnr ilike '%" + v_Gelen.zmatnr + "%' ";
                //}

                //if (!String.IsNullOrEmpty(v_Gelen.zmaktx))
                //{
                //    _Sql += "and maktx  ilike '%" + v_Gelen.zmaktx + "%' ";

                //}

                //if (!String.IsNullOrEmpty(v_Gelen.zgecisizni))
                //{
                //    if (v_Gelen.zgecisizni != "2")
                //    {
                //        _Sql += "and gecisizni=" + v_Gelen.zgecisizni + " ";

                //    }

                //}

                //if (!String.IsNullOrEmpty(v_Gelen.zalarm))
                //{
                //    if (v_Gelen.zalarm != "3")
                //    {
                //        _Sql += "and alarmdurum=" + v_Gelen.zalarm + " ";
                //    }


                //}




                _dTable = _myIslem._fnDataTable(_Sql);
                _Cevap.zdizi = new List<RfidFarkListeleView>();

                for (int i = 0; i < _dTable.Rows.Count; i++)
                {
                    _Cevap.zdizi.Add(new RfidFarkListeleView()
                    {
                        zid = _dTable.Rows[i]["id"].ToString().Trim(),
                        zaufnr = _dTable.Rows[i]["aufnr"].ToString().Trim(),
                        zrfidcount = _dTable.Rows[i]["iliskiliSayisi"].ToString().Trim(),
                        zsapcount = _dTable.Rows[i]["toplamSayi"].ToString().Trim(),
                        zemail = _dTable.Rows[i]["emailgonderimi"].ToString().Trim()

                    });
                }
                _Cevap.zSonuc = 1;
                _Cevap.zAciklama = "Başarılı";
            }
            catch (Exception ex)
            {
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();
                _Cevap.zdizi = new List<RfidFarkListeleView>();
            }

            //cmd.Parameters.AddWithValue("@ish_id", hareketitem.ish_id);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //SqlDataReader dr = cmd.ExecuteReader();

            //if (dr.Read())
            //{
            //    int recno = Convert.ToInt32(dr["ish_recno"].ToString());
            //}


            return _Cevap;
        }
    }
}