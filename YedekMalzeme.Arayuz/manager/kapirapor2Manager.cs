using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using YedekMalzeme.Arayuz.ServiceReference1;
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

                _Sql += "select * from tbl06analiz  where aktif =1 and kapireader=1 ";

                if (!String.IsNullOrEmpty(v_Gelen.zilktarih) && !String.IsNullOrEmpty(v_Gelen.zsontarih))
                {

                    _Sql += "and okumabitis between '" + v_Gelen.zilktarih + "' and '" + v_Gelen.zsontarih + "' ";

                }

                if (!String.IsNullOrEmpty(v_Gelen.zaufnr))
                {
                    _Sql += "and aufnr ilike '%" + v_Gelen.zaufnr + "%' ";
                }



                if (!String.IsNullOrEmpty(v_Gelen.zmatnr))
                {
                    _Sql += "and matnr ilike '%" + v_Gelen.zmatnr + "%' ";
                }

                if (!String.IsNullOrEmpty(v_Gelen.zmaktx))
                {
                    _Sql += "and maktx  ilike '%" + v_Gelen.zmaktx + "%' ";

                }

                if (!String.IsNullOrEmpty(v_Gelen.zgecisizni))
                {
                    if (v_Gelen.zgecisizni != "2")
                    {
                        _Sql += "and gecisizni=" + v_Gelen.zgecisizni + " ";

                    }

                }

                if (!String.IsNullOrEmpty(v_Gelen.zalarm))
                {
                    if (v_Gelen.zalarm != "3")
                    {
                        _Sql += "and alarmdurum=" + v_Gelen.zalarm + " ";
                    }


                }

                _dTable = _myIslem._fnDataTable(_Sql);
                _Cevap.zdizi = new List<KapiFiltreliListeleView>();

                for (int i = 0; i < _dTable.Rows.Count; i++)
                {
                    _Cevap.zdizi.Add(new KapiFiltreliListeleView()
                    {
                        zepc = _dTable.Rows[i]["epc"].ToString().Trim(),
                        zokumabaslangic = String.Format("{0:dd.MM.yyyy HH:mm:ss}", _dTable.Rows[i]["okumabaslangic"].ToString().Trim()),
                        zokumabitis = String.Format("{0:dd.MM.yyyy HH:mm:ss}", _dTable.Rows[i]["okumabitis"].ToString().Trim()),
                        zalarm = _dTable.Rows[i]["alarmdurum"].ToString().Trim(),
                        zgecisizni = _dTable.Rows[i]["gecisizni"].ToString().Trim(),
                        zmaktx = _dTable.Rows[i]["maktx"].ToString().Trim(),
                        zmatnr = _dTable.Rows[i]["matnr"].ToString().Trim(),
                        zsernr = _dTable.Rows[i]["sernr"].ToString().Trim(),
                        zaufnr = _dTable.Rows[i]["aufnr"].ToString().Trim(),
                        zid = _dTable.Rows[i]["id"].ToString().Trim(),


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
                    tbl06analiz _guncelanaliz = session.Query<tbl06analiz>().FirstOrDefault(a => a.aktif == 1 && a.id.Equals(v_Gelen.zid));

                    _TabloYazisi += "<tr>";
                    _TabloBasligi += "<tr>";



                    _TabloBasligi += "<th style ='text-align: center'> Etiket Değeri </ th >";
                    _TabloYazisi += "<td id='epc' name='epc' style='text-align:center'  >" + _guncelanaliz.epc + "</td>";


                    _TabloBasligi += "<th style ='text-align: center'> Malzeme Adı </ th >";
                    _TabloBasligi += "<th style ='text-align: center'> Malzeme Kodu </ th >";
                    _TabloBasligi += "<th style ='text-align: center'> Seri No </ th >";


                    _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.matnr + "</td>";
                    _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.maktx + "</td>";
                    _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.sernr + "</td>";


                    if (_guncelanaliz.alarmdurum == 1)
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
                        _TabloBasligi += "<th style ='text-align: center'> Kapatma Açıklaması </ th >";
                        _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.alarmkapatmaaciklama + "</td>";
                        _TabloBasligi += "<th style ='text-align: center'> Alarmı Kapatma Tarihi </ th >";
                        _TabloYazisi += "<td style='text-align:center;'>" + _guncelanaliz.alarmkapatmatarih + "</td>";

                    }

                    if (_guncelanaliz.aufnr == "ilişkisiz" && _guncelanaliz.sernr!= "yok")
                    {

                        _TabloBasligi += "<th style ='text-align: center'> Siparişe Ekle </ th >";
                        _TabloYazisi += "<td style='text-align:center;'><a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick  = fn_SipariseEkle('" + _guncelanaliz.id + "');>Siparişe Ekle</a></td>";

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

        //update işlemi
        internal void fn_BilesenGuncelle(BilesenGuncelleRequest v_Gelen)
        {
            #region Değişkenler

            string UserName_d = "";
            string Password_d = "";

            string _ParamErdatLow = "";
            string _ParamErdatHigh = "";
            string _ParamIwerk = "";
            string _ParamAufnr = "";

            bool _LogAlindimi = false;

            #endregion

            try
            {

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    XpoManager.Instance.InitXpo();
                }


                _LogAlindimi = false;




                #region Parametre Çek
                try
                {
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        tblzrfidorderlistparam _Temp = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w => w.aktif == 1);

                        if (_Temp == null)
                        {



                        }
                        else
                        {


                            if (_Temp.aufnr != null)
                            {
                                _ParamAufnr = _Temp.aufnr;
                            }
                            else
                            {
                                _ParamAufnr = "";
                            }

                            if (_Temp.erdathigh != null)
                            {
                                _ParamErdatHigh = _Temp.erdathigh;
                            }
                            else
                            {
                                _ParamErdatHigh = "";
                            }

                            if (_Temp.erdatlow != null)
                            {
                                _ParamErdatLow = _Temp.erdatlow;
                            }
                            else
                            {
                                _ParamErdatLow = "";
                            }

                            if (_Temp.iwerk != null)
                            {
                                _ParamIwerk = _Temp.iwerk;
                            }
                            else
                            {
                                _ParamIwerk = "";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {


                }


                #endregion


                try
                {
                    _ParamAufnr = v_Gelen.zaufnr;

                    #region Bağlantı
                    ConfigurationManager.RefreshSection("appSettings");


                    UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                    Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

                    ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                    _Client.ClientCredentials.UserName.UserName = UserName_d;
                    _Client.ClientCredentials.UserName.Password = Password_d;
                    _Client.Open();

                    ZrfidStrOrderList _orderList = new ZrfidStrOrderList();

                    #region _orderList Deger
                    _orderList.Aufnr = "";
                    _orderList.Posnr = "";
                    _orderList.Matnr = "";
                    _orderList.Maktx = "";
                    _orderList.Bdmng = 0;
                    _orderList.Enmng = 0;
                    _orderList.Kzear = "";
                    _orderList.Meins = "";
                    _orderList.Werks = "";
                    _orderList.Lgort = "";
                    _orderList.Vornr = "";
                    _orderList.Erdat = "";
                    _orderList.Rsnum = "";
                    _orderList.Rspos = "";

                    #endregion


                    ZrfidStrOrderList[] _DiziOrderList = new ZrfidStrOrderList[1];
                    _DiziOrderList[0] = _orderList;


                    Bapiret2 _tmp2 = new Bapiret2();

                    #region Bapiret2 Deger
                    _tmp2.Field = "";
                    _tmp2.Id = "";
                    _tmp2.LogMsgNo = "";
                    _tmp2.Message = "";
                    _tmp2.MessageV1 = "";
                    _tmp2.MessageV3 = "";
                    _tmp2.MessageV3 = "";
                    _tmp2.MessageV4 = "";
                    _tmp2.Number = "";
                    _tmp2.Parameter = "";
                    _tmp2.Row = 0;
                    _tmp2.System = "";
                    _tmp2.Type = "";

                    Bapiret2[] _tmp2Dizi = new Bapiret2[1];
                    _tmp2Dizi[0] = _tmp2;

                    #endregion

                    #region Erdat

                    ZrfidStrErdat _erdat = new ZrfidStrErdat();
                    _erdat.ErdatHigh = _ParamErdatHigh;
                    _erdat.ErdatLow = _ParamErdatLow;

                    #endregion

                    #region IWERK

                    ZrfidStrIwerk _iwerk = new ZrfidStrIwerk();
                    _iwerk.Iwerk = _ParamIwerk;

                    ZrfidStrIwerk[] _IwerkDizi = new ZrfidStrIwerk[1];
                    _IwerkDizi[0] = _iwerk;


                    #endregion

                    #region Aufnr

                    ZrfidStrAufnr _aufnr = new ZrfidStrAufnr();
                    _aufnr.Aufnr = _ParamAufnr;

                    ZrfidStrAufnr[] _aufnrDizi = new ZrfidStrAufnr[1];
                    _aufnrDizi[0] = _aufnr;


                    #endregion



                    ZrfidOrderList _Request = new ZrfidOrderList();
                    _Request.EtOrderList = _DiziOrderList;
                    _Request.EtReturn = _tmp2Dizi;

                    if (_erdat.ErdatHigh.Trim().Length != 0 || _erdat.ErdatLow.Trim().Length != 0)
                    {
                        _Request.IsErdat = _erdat;
                    }

                    if (_ParamAufnr.Trim().Length != 0)
                    {
                        _Request.ItAufnr = _aufnrDizi;
                    }

                    if (_ParamIwerk.Trim().Length != 0)
                    {
                        _Request.ItIwerk = _IwerkDizi;
                    }
                    #endregion



                    ZrfidOrderListResponse _Cevap = _Client.ZrfidOrderList(_Request);

                    #region Önce Tüm Sipariş Listesinin aktifini 0 yap

                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        List<tblzrfidorderlistreponse> _Temp = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr)).ToList();

                        foreach (var item in _Temp)
                        {
                            item.aktif = 0;
                            item.Save();
                        }

                    }

                    #endregion

                    #region PM Sipariş Listesini Çek

                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        for (int _veriSayac = 0; _veriSayac < _Cevap.EtOrderList.Length; _veriSayac++)
                        {
                            #region Gelen Degerler
                            string _gelenAufnr = _Cevap.EtOrderList[_veriSayac].Aufnr;
                            string _gelenPosnr = _Cevap.EtOrderList[_veriSayac].Posnr;
                            string _gelenMatnr = _Cevap.EtOrderList[_veriSayac].Matnr;
                            string _gelenMaktx = _Cevap.EtOrderList[_veriSayac].Maktx;
                            decimal _gelenBdmng = _Cevap.EtOrderList[_veriSayac].Bdmng;
                            decimal _gelenEnmng = _Cevap.EtOrderList[_veriSayac].Enmng;
                            string _gelenKzear = _Cevap.EtOrderList[_veriSayac].Kzear;
                            string _gelenMeins = _Cevap.EtOrderList[_veriSayac].Meins;
                            string _gelenWerks = _Cevap.EtOrderList[_veriSayac].Werks;
                            string _gelenLgort = _Cevap.EtOrderList[_veriSayac].Lgort;
                            string _gelenVornr = _Cevap.EtOrderList[_veriSayac].Vornr;
                            string _gelenErdat = _Cevap.EtOrderList[_veriSayac].Erdat;
                            string _gelenRsnum = _Cevap.EtOrderList[_veriSayac].Rsnum;
                            string _gelenRspos = _Cevap.EtOrderList[_veriSayac].Rspos;



                            #endregion


                            tblzrfidorderlistreponse _Temp = session.Query<tblzrfidorderlistreponse>().FirstOrDefault(w => w.aufnr.Equals(_Cevap.EtOrderList[_veriSayac].Aufnr) && (w.aktif == 1) && (w.matnr.Equals(_Cevap.EtOrderList[_veriSayac].Matnr)));


                            if (_Temp == null)
                            {

                                new tblzrfidorderlistreponse(session)
                                {


                                    aktif = 1,
                                    databasekayitzamani = DateTime.Now,
                                    guncellemezamani = DateTime.Now,
                                    id = Guid.NewGuid().ToString().ToUpper(),
                                    createuser = "aniventi",
                                    lastupdateuser = "aniventi",
                                    aufnr = _gelenAufnr,
                                    posnr = _gelenPosnr,
                                    matnr = _gelenMatnr,
                                    maktx = _gelenMaktx,
                                    bdmng = _gelenBdmng.ToString(),
                                    enmng = _gelenEnmng.ToString(),
                                    kzear = _gelenKzear,
                                    meins = _gelenMeins,
                                    werks = _gelenWerks,
                                    lgort = _gelenLgort,
                                    vornr = _gelenVornr,
                                    erdat = _gelenErdat,
                                    rsnum = _gelenRsnum,
                                    rspos = _gelenRspos,


                                }.Save();


                            }
                            else
                            {
                                if (_Temp.aufnr.Equals(_gelenAufnr) == false)
                                {
                                    if (_LogAlindimi == false)
                                    {
                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = _Temp.databasekayitzamani,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "aufnr",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();


                                        _LogAlindimi = true;
                                    }


                                    _Temp.aufnr = _gelenAufnr;
                                    _Temp.lastupdateuser = "aniventi";
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();
                                }

                                if (_Temp.posnr.Equals(_gelenPosnr) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {

                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "posnr",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();

                                        _LogAlindimi = true;
                                    }



                                    _Temp.posnr = _gelenPosnr;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();

                                }

                                if (_Temp.matnr.Equals(_gelenMatnr) == false)
                                {
                                    if (_LogAlindimi == false)
                                    {

                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "matnr",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();

                                        _LogAlindimi = true;
                                    }



                                    _Temp.matnr = _gelenMatnr;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();
                                }

                                if (_Temp.bdmng.Equals(_gelenBdmng) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {

                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "bdmng",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();

                                        _LogAlindimi = true;
                                    }



                                    _Temp.bdmng = _gelenBdmng.ToString();
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();
                                }

                                if (_Temp.meins.Equals(_gelenMeins) == false)
                                {
                                    if (_LogAlindimi == false)
                                    {

                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "meins",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();

                                        _LogAlindimi = true;
                                    }



                                    _Temp.meins = _gelenMeins;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();
                                }

                                if (_Temp.werks.Equals(_gelenWerks) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {
                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "werks",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();


                                        _LogAlindimi = true;
                                    }



                                    _Temp.werks = _gelenWerks;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();

                                }

                                if (_Temp.lgort.Equals(_gelenLgort) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {

                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "lgort",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();

                                        _LogAlindimi = true;
                                    }


                                    _Temp.lgort = _gelenLgort;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();

                                }

                                if (_Temp.vornr.Equals(_gelenVornr) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {

                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "vornr",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();

                                        _LogAlindimi = true;
                                    }



                                    _Temp.vornr = _gelenVornr.ToString();
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();
                                }

                                if (_Temp.rspos.Equals(_gelenRspos) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {
                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "rspos",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();


                                        _LogAlindimi = true;
                                    }



                                    _Temp.rspos = _gelenRspos;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();

                                }

                                if (_Temp.rsnum.Equals(_gelenRsnum) == false)
                                {

                                    if (_LogAlindimi == false)
                                    {
                                        new tblarsivzrfidorderlistreponse(session)
                                        {
                                            aktif = _Temp.aktif,
                                            databasekayitzamani = DateTime.Now,
                                            createuser = "aniventi",
                                            guncellemezamani = DateTime.Now,
                                            degisenalan = "rsnum",
                                            aufnr = _Temp.aufnr,
                                            bdmng = _Temp.bdmng,
                                            enmng = _Temp.enmng,
                                            erdat = _Temp.erdat,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            kzear = _Temp.kzear,
                                            lastupdateuser = "aniventi",
                                            lgort = _Temp.lgort,
                                            maktx = _Temp.maktx,
                                            matnr = _Temp.matnr,
                                            meins = _Temp.meins,
                                            posnr = _Temp.posnr,
                                            rsnum = _Temp.rsnum,
                                            rspos = _Temp.rspos,
                                            vornr = _Temp.vornr,
                                            werks = _Temp.werks

                                        }.Save();


                                        _LogAlindimi = true;
                                    }



                                    _Temp.rsnum = _gelenRsnum;
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.Save();

                                }




                            }


                        }


                        for (int verisayac1 = 0; verisayac1 < _Cevap.EtReturn.Length; verisayac1++)
                        {
                            #region Gelen Değerler

                            string _gelenMessage = _Cevap.EtReturn[verisayac1].Message;
                            string _gelenfield = _Cevap.EtReturn[verisayac1].Field;
                            string _gelenlogmsgno = _Cevap.EtReturn[verisayac1].LogMsgNo;
                            string _gelenlogno = _Cevap.EtReturn[verisayac1].LogNo;
                            string _gelenmessage = _Cevap.EtReturn[verisayac1].Message;
                            string _gelenmessagev1 = _Cevap.EtReturn[verisayac1].MessageV1;
                            string _gelenmessagev2 = _Cevap.EtReturn[verisayac1].MessageV2;
                            string _gelenmessagev3 = _Cevap.EtReturn[verisayac1].MessageV3;
                            string _gelenmessagev4 = _Cevap.EtReturn[verisayac1].MessageV4;
                            string _gelennumber = _Cevap.EtReturn[verisayac1].Number;
                            string _gelenparameter = _Cevap.EtReturn[verisayac1].Parameter;
                            string _gelenrow = _Cevap.EtReturn[verisayac1].Row.ToString();
                            string _gelensystem = _Cevap.EtReturn[verisayac1].System;
                            string _gelentype = _Cevap.EtReturn[verisayac1].Type;

                            #endregion



                            if (_gelenMessage != null && _gelenMessage != " ")
                            {
                                new tblzrfidorderlistreturn(session)
                                {
                                    aktif = 1,
                                    createuser = "aniventi,",
                                    databasekayitzamani = DateTime.Now,
                                    guncellemezamani = DateTime.Now,
                                    id = Guid.NewGuid().ToString().ToUpper(),
                                    lastupdateuser = "aniventi,",
                                    field = _gelenfield,
                                    logmsgno = _gelenlogmsgno,
                                    logno = _gelenlogno,
                                    message = _gelenMessage,
                                    messagev1 = _gelenmessagev1,
                                    messagev2 = _gelenmessagev2,
                                    messagev3 = _gelenmessagev3,
                                    messagev4 = _gelenmessagev4,
                                    number = _gelennumber,
                                    parameter = _gelenparameter,
                                    row = _gelenrow,
                                    system = _gelenrow,
                                    type = _gelentype,

                                }.Save();
                            }



                        }


                    }


                    #endregion

                    #region Pasif kayıtların son güncellenme zamanını tut
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        List<tblzrfidorderlistreponse> _Liste = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif == 0).ToList();

                        foreach (var item in _Liste)
                        {
                            item.lastupdateuser = "aniventi";
                            item.guncellemezamani = DateTime.Now;
                            item.Save();
                        }
                    }
                    #endregion

                    _Client.Close();


                }
                catch (Exception ex)
                {


                }

            }
            catch (Exception ex)
            {


            }
        }
        internal SipariseBilesenEkleResponse fn_SipariseBilesenEkle(SipariseBilesenEkleRequest v_Gelen)
        {
            SipariseBilesenEkleResponse _Cevap = new SipariseBilesenEkleResponse();
            try
            {
                if (v_Gelen.zaufnr != "-1")
                {
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {

                        #region Değişkenler
                        string _IslemKodu = "0";
                        string _Posnr = "0";
                        string _Rsnum = "";
                        string _RsPos = "";
                        int _EkliMalzemeSayisi = 0;
                        string _IstekNo = "";

                        #endregion

                        #region Gıncel analiz

                        tbl06analiz _guncelleanliz = session.Query<tbl06analiz>().FirstOrDefault(a => a.id.Equals(v_Gelen.zid));


                        _guncelleanliz.aufnr = v_Gelen.zaufnr;
                        _guncelleanliz.iliskilendiren = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _guncelleanliz.gecisizni = 1;
                        _guncelleanliz.alarmdurum = 2;
                        _guncelleanliz.alarmkapatan = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _guncelleanliz.alarmkapatmatarih = DateTime.Now;
                        _guncelleanliz.alarmkapatmaaciklama = "Geçmişe dönük siparişle ilişkilendirildi.";
                        _guncelleanliz.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _guncelleanliz.guncellemezamani = DateTime.Now;
                        #endregion


                        if (v_Gelen.zaufnr.ToString().StartsWith("00") == false)
                        {
                            v_Gelen.zaufnr = "00" + v_Gelen.zaufnr;
                        }

                        #region Sipariş Detayı
                        tblzrfidorderlistreponse _OrderList = session.Query<tblzrfidorderlistreponse>().FirstOrDefault(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr));
                        if (_OrderList!=null)
                        {
                            _Posnr = _OrderList.posnr;
                            _Rsnum = _OrderList.rsnum;
                            _RsPos = _OrderList.rspos;
                        }
                      
                        #endregion

                        #region Ekli Malzeme Sayısı
                        List<tbl01eklecikararsiv> _EpcList = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr) && w.matnr.Equals(_guncelleanliz.matnr)).ToList();
                        _EkliMalzemeSayisi = _EpcList.Count;
                        #endregion

                        #region Önce EPC ekli mi kontrol

                        tbl01eklecikararsiv _TempEpc = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(_guncelleanliz.epc) && w.aufnr.Equals(v_Gelen.zaufnr));

                        if (_TempEpc != null)
                        {
                            _IslemKodu = "100"; //epc ekli ise siparişten çıkar
                        }
                        else
                        {
                            _TempEpc = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr) && w.matnr.Equals(_guncelleanliz.matnr));

                            if (_TempEpc != null)
                            {
                                _IslemKodu = "101";// aynı malzeme kodundan yine ekleyiorum

                            }
                            else
                            {
                                _IslemKodu = "102"; //daha önce eklenmiş malzeme kodunu ekliyorum

                             }
                        }
                        #endregion

                        #region İstek Arşiv

                        tbl02istektakip _Takip = new tbl02istektakip(session)
                        {
                            aktif = 1,
                            aufnr = v_Gelen.zaufnr,
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            gelenepc = _guncelleanliz.epc,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            islemkodu = _IslemKodu,
                            islemturu = _IslemKodu,
                            kullanici = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = _guncelleanliz.maktx,
                            matnr = _guncelleanliz.matnr,
                            sernr = _guncelleanliz.maktx
                        };

                        if (_IslemKodu.Equals("100"))
                        {
                            _Takip.aktif = 0;
                            _Takip.Save();


                            List<tbl02istektakip> _Eski = session.Query<tbl02istektakip>().Where(w => w.aufnr.Equals(_Takip.aufnr) && w.gelenepc.Equals(_Takip.gelenepc) && w.sernr.Equals(_Takip.sernr)).ToList();

                            foreach (var item in _Eski)
                            {
                                item.aktif = 0;
                                item.Save();
                            }
                        }


                        _IstekNo = _Takip.id;
                        #endregion

                        #region SAP Connect


                        string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                        string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

                        ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                        _Client.ClientCredentials.UserName.UserName = UserName_d;
                        _Client.ClientCredentials.UserName.Password = Password_d;
                        _Client.Open();

                        ZrfidStrComponentUpdate _Param = new ZrfidStrComponentUpdate();

                        _Param.Ablad = "";

                        if (_IslemKodu == "100")
                        {
                            _Param.Bdmng = _EkliMalzemeSayisi - 1;
                            _Param.Posnr = _Posnr;
                            _Param.Rsnum = _Rsnum;
                            _Param.Rspos = _RsPos;


                            if (_Param.Bdmng <= 0)
                            {
                                _Param.Indc = "D";
                            }
                            else
                            {
                                if (_Param.Bdmng > 0)
                                {
                                    _Param.Indc = "C";
                                }
                            }

                        }
                        else
                        {
                            if (_IslemKodu == "101")
                            {
                                _Param.Posnr = _Posnr;
                                _Param.Bdmng = _EkliMalzemeSayisi + 1;
                                _Param.Indc = "C";
                                _Param.Rsnum = _Rsnum;
                                _Param.Rspos = _RsPos;
                            }
                            else
                            {
                                if (_IslemKodu == "102")
                                {
                                    List<tbl01eklecikararsiv> _arsivtemp = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 && w.sernr.Equals(_guncelleanliz.sernr)).ToList();
                                    if (_arsivtemp != null)
                                    {
                                        _Param.Posnr = "";
                                        _Param.Rsnum = "";
                                        _Param.Rspos = "";
                                        _Param.Bdmng = _EkliMalzemeSayisi + 1;
                                        _Param.Indc = "I";
                                    }


                                }
                            }
                        }
                        _Param.Lgort = "1910";
                        _Param.Matnr = _guncelleanliz.matnr;
                        _Param.Meins = "ADT";
                        
                        _Param.Postp = "";
                        _Param.Rgekz = "";

                        _Param.Vornr = "0010";
                        _Param.Wempf = "rfid_" + HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _Param.Werks = "1210";

                        ZrfidStrComponentUpdate[] _DiziParam = new ZrfidStrComponentUpdate[1];
                        _DiziParam[0] = _Param;


                        #region Bapiret2 Deger

                        Bapiret2 _babiretParam = new Bapiret2();

                        _babiretParam.Field = "";
                        _babiretParam.Id = "";
                        _babiretParam.LogMsgNo = "";
                        _babiretParam.Message = "";
                        _babiretParam.MessageV1 = "";
                        _babiretParam.MessageV3 = "";
                        _babiretParam.MessageV3 = "";
                        _babiretParam.MessageV4 = "";
                        _babiretParam.Number = "";
                        _babiretParam.Parameter = "";
                        _babiretParam.Row = 0;
                        _babiretParam.System = "";
                        _babiretParam.Type = "";

                        Bapiret2[] _babiretParamDizi = new Bapiret2[1];
                        _babiretParamDizi[0] = _babiretParam;

                        #endregion

                        #endregion

                        #region Aufnr               

                        ZrfidComponentUpdate _Request = new ZrfidComponentUpdate();

                        _Request.ItComponent = _DiziParam;
                        _Request.EtReturn = _babiretParamDizi;
                        _Request.IpAufnr = v_Gelen.zaufnr;

                        if (_Request.IpAufnr.StartsWith("00") == false)
                        {
                            _Request.IpAufnr = "00" + _Request.IpAufnr;
                        }

                        ZrfidComponentUpdateResponse _ServisYanit = _Client.ZrfidComponentUpdate(_Request);

                        for (int verisayac1 = 0; verisayac1 < _ServisYanit.EtReturn.Length; verisayac1++)
                        {
                            #region Gelen Değerler

                            string _gelenMessage = _ServisYanit.EtReturn[verisayac1].Message;
                            string _gelenfield = _ServisYanit.EtReturn[verisayac1].Field;
                            string _gelenlogmsgno = _ServisYanit.EtReturn[verisayac1].LogMsgNo;
                            string _gelenlogno = _ServisYanit.EtReturn[verisayac1].LogNo;
                            string _gelenmessage = _ServisYanit.EtReturn[verisayac1].Message;
                            string _gelenmessagev1 = _ServisYanit.EtReturn[verisayac1].MessageV1;
                            string _gelenmessagev2 = _ServisYanit.EtReturn[verisayac1].MessageV2;
                            string _gelenmessagev3 = _ServisYanit.EtReturn[verisayac1].MessageV3;
                            string _gelenmessagev4 = _ServisYanit.EtReturn[verisayac1].MessageV4;
                            string _gelennumber = _ServisYanit.EtReturn[verisayac1].Number;
                            string _gelenparameter = _ServisYanit.EtReturn[verisayac1].Parameter;
                            string _gelenrow = _ServisYanit.EtReturn[verisayac1].Row.ToString();
                            string _gelensystem = _ServisYanit.EtReturn[verisayac1].System;
                            string _gelentype = _ServisYanit.EtReturn[verisayac1].Type;

                            #endregion


                            tblzrfidorderlistreturn _Return = session.Query<tblzrfidorderlistreturn>().FirstOrDefault(w => w.message.Equals(_ServisYanit.EtReturn[verisayac1].Message));

                            if (_gelenMessage != null && _gelenMessage != "")
                            {
                                new tblzrfidorderlistreturn(session)
                                {
                                    istekno = _IstekNo,
                                    aktif = 1,
                                    createuser = "aniventi,",
                                    databasekayitzamani = DateTime.Now,
                                    guncellemezamani = DateTime.Now,
                                    id = Guid.NewGuid().ToString().ToUpper(),
                                    lastupdateuser = "aniventi,",
                                    field = _gelenfield,
                                    logmsgno = _gelenlogmsgno,
                                    logno = _gelenlogno,
                                    message = _gelenMessage,
                                    messagev1 = _gelenmessagev1,
                                    messagev2 = _gelenmessagev2,
                                    messagev3 = _gelenmessagev3,
                                    messagev4 = _gelenmessagev4,
                                    number = _gelennumber,
                                    parameter = _gelenparameter,
                                    row = _gelenrow,
                                    system = _gelenrow,
                                    type = _gelentype,

                                }.Save();
                            }

                        }

                        #region İşlem Sonucunu Kontrol Et

                        List<tblzrfidorderlistreturn> _YanitDizi = session.Query<tblzrfidorderlistreturn>().Where(w => w.istekno.Equals(_IstekNo)).ToList();

                        bool _IslemBasarili = true;
                        string _HataMesajlari = "";

                        int _iSayac = 0;

                        foreach (var _Yanit in _YanitDizi)
                        {
                            if (_Yanit.type.Trim().Equals("E"))
                            {
                                _iSayac += 1;

                                _IslemBasarili = false;
                                _HataMesajlari += _iSayac + " -" + _Yanit.message + "\n";
                            }
                        }

                        if (_IslemBasarili == true)
                        {


                            _Cevap.zSonuc = 1;
                            _Cevap.zAciklama = "";

                            tbl01eklecikararsiv _ArsivTemp = new tbl01eklecikararsiv(session)
                            {
                                aktif = 1,
                                aufnr = v_Gelen.zaufnr,
                                createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                                databasekayitzamani = DateTime.Now,
                                gelenepc = _guncelleanliz.epc,
                                guncellemezamani = DateTime.Now,
                                id = Guid.NewGuid().ToString().ToUpper(),
                                islemturu = _IslemKodu,
                                kullanici = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                                lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                                maktx = _guncelleanliz.maktx,
                                matnr = _guncelleanliz.matnr,
                                sernr = _guncelleanliz.sernr
                            };
                            _ArsivTemp.Save();

                            if (_IslemKodu == "100")
                            {
                                _ArsivTemp.aktif = 0;
                                _ArsivTemp.Save();

                                List<tbl01eklecikararsiv> _Eski = session.Query<tbl01eklecikararsiv>().Where(w => w.aufnr.Equals(_ArsivTemp.aufnr) && w.gelenepc.Equals(_ArsivTemp.gelenepc) && w.sernr.Equals(_ArsivTemp.sernr)).ToList();

                                foreach (var item in _Eski)
                                {
                                    item.aktif = 0;
                                    item.Save();
                                }


                            }


                            BilesenGuncelleRequest v_Giden = new BilesenGuncelleRequest();
                            v_Giden.zaufnr = v_Gelen.zaufnr;

                            fn_BilesenGuncelle(v_Giden);

                            
                        }


                        #endregion




                        #endregion


                        _guncelleanliz.Save();

                       

                    }

                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                }


               
            }
            catch (Exception ex)
            {

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }
            return _Cevap;
        }

        //veriçekme
        internal SipariseEkleResponse fn_SipariseEkle(SipariseEkleRequest v_Gelen)
        {
            #region Değişkenler

            SipariseEkleResponse _Cevap = new SipariseEkleResponse();
            string _SiparisListesi = "";


            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<AcikSiparisListesiView> _PmSiparisListe = session.Query<tblzrfidorderlistreponse>().Where(t => t.aktif == 1).Select(t => new AcikSiparisListesiView()
                    {
                        zaufnr = t.aufnr
                    }).Distinct().ToList();

                    _SiparisListesi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in _PmSiparisListe)
                    {
                        _SiparisListesi += "<option value='" + item.zaufnr + "'>" + item.zaufnr + "</option>";
                    }

                    tbl06analiz _analiz = session.Query<tbl06analiz>().FirstOrDefault(e => e.aktif == 1 && e.id.Equals(v_Gelen.zid));

                    _Cevap.zid = _analiz.id;
                    _Cevap.zaufnrlistesi = _SiparisListesi;
                    _Cevap.zepc = _analiz.epc;
                    _Cevap.zmaktx = _analiz.maktx;
                    _Cevap.zmatnr = _analiz.matnr;
                    _Cevap.zsern = _analiz.sernr;
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                }


            }
            catch (Exception)
            {

                _Cevap.zaufnrlistesi = "";
                _Cevap.zepc = "";
                _Cevap.zmaktx = "";
                _Cevap.zmatnr = "";
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";


            }

            return _Cevap;
        }
    }
}