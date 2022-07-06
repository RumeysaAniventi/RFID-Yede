using System;
using System.Collections.Generic;
using System.Data;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using YedekMalzeme.Arayuz.View;

namespace YedekMalzeme.Arayuz.manager
{
    public class kapirapor2Manager
    {
        internal FiltreliKapiListesiResponse fn_FiltreliKapiListesi(FiltreliKapiListesiRequest v_Gelen)
        {
            FiltreliKapiListesiResponse _Cevap = new FiltreliKapiListesiResponse();
          
            string _Sql = "";
            //SqlCommand _Komut = new SqlCommand();

            DataTable _dTable = new DataTable();
            cVeriTabaniIslem _myIslem = new cVeriTabaniIslem();

            try
            {

                _Sql += "select * from tbl06analiz  where aktif =1 and kapireader=1";

                _dTable = _myIslem._fnDataTable(_Sql);
                _Cevap.zdizi = new List<KapiFiltreliListeleView>();

                for (int i = 0; i < _dTable.Rows.Count; i++)
                {
                    _Cevap.zdizi.Add(new KapiFiltreliListeleView()
                    {
                        zepc= _dTable.Rows[i]["epc"].ToString().Trim(),
                        zokumabaslangic = String.Format("{0:dd.MM.yyyy HH:mm:ss}", _dTable.Rows[i]["okumabaslangic"].ToString().Trim()),
                        zokumabitis = String.Format("{0:dd.MM.yyyy HH:mm:ss}", _dTable.Rows[i]["okumabitis"].ToString().Trim()),
                        zalarm= _dTable.Rows[i]["alarmdurum"].ToString().Trim(),
                        zgecisizni= _dTable.Rows[i]["gecisizni"].ToString().Trim(),
                        zmaktx= _dTable.Rows[i]["maktx"].ToString().Trim(),
                        zmatnr=_dTable.Rows[i]["matnr"].ToString().Trim(),
                        zsernr=_dTable.Rows[i]["sernr"].ToString().Trim(),
                        zaufnr= _dTable.Rows[i]["aufnr"].ToString().Trim(),


                    });
                }
                _Cevap.zSonuc = 1;
                _Cevap.zAciklama = "Başarılı";
            }
            catch (Exception ex)
            {
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();
                _Cevap.zdizi = new List<KapiFiltreliListeleView>();
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