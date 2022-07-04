using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Tuketim.Request;
using Tuketim.Response;

namespace Tuketim.Manager
{
    public class TuketimManager
    {
        public ViewTuketimDurum fn_TuketimDurumEkle(RequestTuketimDurum v_Gelen)
        {
            #region Değişkenler
            ViewTuketimDurum _Cevap = new ViewTuketimDurum();
            cVeriTabani _veriTabani = new cVeriTabani();
            string _Sql = "";
            DataTable _dTable = new DataTable();
            NpgsqlCommand _Komut = new NpgsqlCommand();
            DataTable _dTable1 = new DataTable();
            NpgsqlCommand _Komut1 = new NpgsqlCommand();
            string exMessage = "";
            #endregion

            //koltuk depoda gelen sipariş no ile malzeme var mı yok mu kontrol edilir . gelen yedekparcano tuketildi ise aktifliği kapatılır ve silinir.Yoksa eklenir.


            return _Cevap;
        }
    }
}