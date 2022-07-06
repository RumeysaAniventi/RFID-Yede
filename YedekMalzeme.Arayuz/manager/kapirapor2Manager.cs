using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
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
                        zid= _dTable.Rows[i]["id"].ToString().Trim(),


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

        internal AlarmKapatResponse fn_AlarmKapat(AlarmKapatRequest v_Gelen)
        {
            AlarmKapatResponse _Cevap = new AlarmKapatResponse();
            try
            {
                if (v_Gelen.zaciklama == "")
                {
                    _Cevap.zAciklama = "Açıklama Boş olamaz";
                    _Cevap.zSonuc = -1;
                }
                else
                {
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {

                        tbl06analiz _guncelleanliz = session.Query<tbl06analiz>().FirstOrDefault(a => a.id.Equals(v_Gelen.zid));
                        _guncelleanliz.gecisizni = 0;
                        _guncelleanliz.alarmdurum = 2;
                        _guncelleanliz.alarmkapatmaaciklama = v_Gelen.zaciklama;
                        _guncelleanliz.alarmkapatmatarih = DateTime.Now;
                        _guncelleanliz.alarmkapatan = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _guncelleanliz.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _guncelleanliz.guncellemezamani = DateTime.Now;
                        _guncelleanliz.Save();


                        _Cevap.zAciklama = "";
                        _Cevap.zSonuc = 1;

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

        internal AlarmDurumGoruntuleResponse fn_AlarmDurumGoruntule(AlarmDurumGoruntuleRequest v_Gelen)
        {
            AlarmDurumGoruntuleResponse _Cevap = new AlarmDurumGoruntuleResponse();

            string _TabloBasligi = "";
            string _TabloYazisi = "";
            try
            {


                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tbl06analiz _guncelanaliz = session.Query<tbl06analiz>().FirstOrDefault(a =>a.aktif==1 &&  a.id.Equals(v_Gelen.zid));

                    _TabloYazisi += "<tr>";
                    _TabloBasligi += "<tr>";

                

                    _TabloBasligi += "<th style ='text-align: center'> Etiket Değeri </ th >";
                    _TabloYazisi += "<td id='epc' name='epc' style='text-align:center'  >"+ _guncelanaliz.epc + "</td>";


                    _TabloBasligi += "<th style ='text-align: center'> Malzeme Adı </ th >";
                    _TabloBasligi += "<th style ='text-align: center'> Malzeme Kodu </ th >";
                    _TabloBasligi += "<th style ='text-align: center'> Seri No </ th >";


                    _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.matnr + "</td>";
                    _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.maktx + "</td>";
                    _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.sernr + "</td>";


                    if (_guncelanaliz.alarmdurum==1)
                    {

                        _TabloBasligi += "<th style ='text-align: center'> AÇIKLAMA</ th >";
                        _TabloYazisi += " <td><input type = 'text' id = 'aciklama' autocomplete='off' name = 'aciklama' style='width:100% ;border:3px' > </td>";

                        _TabloBasligi += "<th style ='text-align: center'> Alarm Kapat </ th >";
                        _TabloYazisi += "<td style='text-align:center;'><a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick  = fn_AlarmKapat('" + v_Gelen.zid + "');>Alarm Kapat</a></td>";


                    }


                    if (_guncelanaliz.alarmdurum == 2)
                    {
                        _TabloBasligi += "<th style ='text-align: center'> Alarmı Kapatan </ th >";
                        _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.alarmkapatan + "</td>";
                        _TabloBasligi += "<th style ='text-align: center'> Alarmı Kapatma Tarihi </ th >";
                        _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.alarmkapatmatarih + "</td>";

                    }

                    if (_guncelanaliz.aufnr == "ilişkisiz")
                    {
                        
                        _TabloBasligi += "<th style ='text-align: center'> Siparişe Ekle </ th >";
                        _TabloYazisi += "<td style='text-align:center;'><a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick  = fn_AlarmKapat('" + _guncelanaliz.id + "');>Siparişe Ekle</a></td>";

                    }


                    _TabloYazisi += "</tr>";

                }
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap._TabloYazisi = _TabloYazisi;
                _Cevap._TabloBasligi = _TabloBasligi;

            }
            catch (Exception ex)
            {


                _Cevap.zAciklama = ex.ToString();
                _Cevap.zSonuc = -1;
                _Cevap._TabloYazisi = "";
                _Cevap._TabloBasligi = "";
            }

            return _Cevap;
        }
    }
}