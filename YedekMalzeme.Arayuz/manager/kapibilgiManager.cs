using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using YedekMalzeme.Arayuz.ServiceReference1;
using YedekMalzeme.Arayuz.View;

namespace YedekMalzeme.Arayuz.manager
{
    public class kapibilgiManager
    {
        internal KapiReaderListesiResponse fn_KapiReaderListesi(KapiReaderListesiRequest v_Gelen)
        {
            KapiReaderListesiResponse _Cevap = new KapiReaderListesiResponse();
            string _TabloYazisi = "";


            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    //tüm geçenler
                    List<tbl03kapireader> _kapiOkumaListesi = session.Query<tbl03kapireader>().Where(w => w.aktif == 1).OrderByDescending(d => d.okumabitis).ToList();


                    foreach (var _Item_readerListesi in _kapiOkumaListesi)
                    {


                        //tüm ilişkili olanlar
                        tbl01eklecikararsiv _siparisliListe = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(_Item_readerListesi.gelenepc));


                        // List<tbl02istektakip> _kimlikliListe = session.Query<tbl02istektakip>().Where(w => w.aktif == 1 && w.gelenepc.Equals(_Item_readerListesi.gelenepc)).ToList();
                        if (_siparisliListe != null)
                        {
                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.gelenepc + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.okumabaslama + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.okumabitis + "</td>";

                            _TabloYazisi += "<td style='text-align:center;'>" + _siparisliListe.aufnr + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _siparisliListe.matnr + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _siparisliListe.maktx + "</td>";
                            if (_Item_readerListesi.gecisturu == 0)

                            {

                                _TabloYazisi += " <td style='text - align: right'> <img src='resimler/0.png'  title='Resim' > izinsiz </ td > ";

                            }
                            else
                            {

                                _TabloYazisi += " <td style='text - align: right'> <img src='resimler/1.png'  title='Resim' > izinli </ td > ";

                            }
                            if (_Item_readerListesi.alarm == 0)
                            {

                                _TabloYazisi += "<td> <span class='m-badge  m-badge--success m-badge--wide'>ALARM YOK</span></td>";
                            }
                            if (_Item_readerListesi.alarm == 1)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--danger m-badge--wide'>ALARM VAR</span></td>";

                            }
                            if (_Item_readerListesi.alarm == 2)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--metal  m-badge--wide'>ALARM KAPANDI</span></td>";

                            }
                            _TabloYazisi += "<td style='text-align:center;'>" + "" + "</td>";
                            _TabloYazisi += "</tr>";


                        }

                        tblkimliklendirme _iliskizikimliki = session.Query<tblkimliklendirme>().FirstOrDefault(p => p.aktif == 1 && p.gelenepc.Equals(_Item_readerListesi.gelenepc));


                        if (_iliskizikimliki != null)
                        {

                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.gelenepc + "</td>";
                            _TabloYazisi += "<td style='text-align:center;' >" + _Item_readerListesi.okumabaslama + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.okumabitis + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + "İlişkisiz" + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _iliskizikimliki.matnr + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _iliskizikimliki.maktx + "</td>";
                            if (_Item_readerListesi.gecisturu == 0)
                            {
                                _TabloYazisi += " <td 'text-align:center'> <img src='resimler/0.png' title='Resim'alt='İZİNSİZ GEÇİŞ' > izinsiz </ td > ";
                            }
                            else
                            {
                                _TabloYazisi += " <td style='text - align: right'> <img src='resimler/1.png' title='Resim' > izinli </ td > ";
                            }
                            if (_Item_readerListesi.alarm == 0)
                            {

                                _TabloYazisi += "<td> <span class='m-badge  m-badge--success m-badge--wide'>ALARM YOK</span></td>";
                            }
                            if (_Item_readerListesi.alarm == 1)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--danger m-badge--wide'>ALARM VAR</span></td>";

                            }
                            if (_Item_readerListesi.alarm == 2)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--metal  m-badge--wide'>ALARM KAPANDI</span></td>";

                            }

                            _TabloYazisi += "<td style='text-align:center;'>" + "" + "</td>";

                            _TabloYazisi += "</tr>";

                        }
                        else
                        {
                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.gelenepc + "</td>";
                            _TabloYazisi += "<td style='text-align:center;' >" + _Item_readerListesi.okumabaslama + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item_readerListesi.okumabitis + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + "İlişkisiz" + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + "Kimliksiz" + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + "Kimliksiz" + "</td>";
                            if (_Item_readerListesi.gecisturu == 0)
                            {
                                _TabloYazisi += " <td 'text-align:center'> <img src='resimler/0.png' title='Resim'alt='İZİNSİZ GEÇİŞ' > izinsiz </ td > ";

                            }
                            else
                            {
                                _TabloYazisi += " <td style='text - align: right'> <img src='resimler/1.png' title='Resim' > izinli </ td > ";

                            }
                            if (_Item_readerListesi.alarm == 0)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--success m-badge--wide'>ALARM YOK</span></td>";
                            }
                            if (_Item_readerListesi.alarm == 1)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--danger m-badge--wide'>ALARM VAR</span></td>";

                            }
                            if (_Item_readerListesi.alarm == 2)
                            {
                                _TabloYazisi += "<td> <span class='m-badge  m-badge--metal  m-badge--wide'>ALARM KAPANDI</span></td>";

                            }

                            _TabloYazisi += "<td style='text-align:center;'>" + "" + "</td>";
                            _TabloYazisi += "</tr>";
                        }

                    }
                }


                _Cevap.zAciklama = "";
                _Cevap.zListeYazisi = _TabloYazisi;
                _Cevap.zSonuc = 1;


            }
            catch (Exception ex)
            {

                _Cevap.zAciklama = "";
                _Cevap.zListeYazisi = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal SipariseBilesenEkleResponse fn_SipariseBilesenEkle(SipariseBilesenEkleRequest v_Gelen)
        {

            #region Değişkenler

            string UserName_d = "";
            string Password_d = "";

            string _ParamErdatLow = "";
            string _ParamErdatHigh = "";
            string _ParamIwerk = "";
            string _ParamAufnr = "";

            bool _LogAlindimi = false;

            SipariseBilesenEkleResponse _Cevap = new SipariseBilesenEkleResponse();

            #endregion
            try
            {


                #region Parametre Çek
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



                    ZrfidOrderListResponse Response = _Client.ZrfidOrderList(_Request);

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
                        for (int _veriSayac = 0; _veriSayac < Response.EtOrderList.Length; _veriSayac++)
                        {
                            #region Gelen Degerler
                            string _gelenAufnr = Response.EtOrderList[_veriSayac].Aufnr;
                            string _gelenPosnr = Response.EtOrderList[_veriSayac].Posnr;
                            string _gelenMatnr = Response.EtOrderList[_veriSayac].Matnr;
                            string _gelenMaktx = Response.EtOrderList[_veriSayac].Maktx;
                            decimal _gelenBdmng = Response.EtOrderList[_veriSayac].Bdmng;
                            decimal _gelenEnmng = Response.EtOrderList[_veriSayac].Enmng;
                            string _gelenKzear = Response.EtOrderList[_veriSayac].Kzear;
                            string _gelenMeins = Response.EtOrderList[_veriSayac].Meins;
                            string _gelenWerks = Response.EtOrderList[_veriSayac].Werks;
                            string _gelenLgort = Response.EtOrderList[_veriSayac].Lgort;
                            string _gelenVornr = Response.EtOrderList[_veriSayac].Vornr;
                            string _gelenErdat = Response.EtOrderList[_veriSayac].Erdat;
                            string _gelenRsnum = Response.EtOrderList[_veriSayac].Rsnum;
                            string _gelenRspos = Response.EtOrderList[_veriSayac].Rspos;



                            #endregion


                            tblzrfidorderlistreponse _Temp = session.Query<tblzrfidorderlistreponse>().FirstOrDefault(w => w.aufnr.Equals(Response.EtOrderList[_veriSayac].Aufnr) && (w.aktif == 1) && (w.matnr.Equals(Response.EtOrderList[_veriSayac].Matnr)));


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


                        for (int verisayac1 = 0; verisayac1 < Response.EtReturn.Length; verisayac1++)
                        {
                            #region Gelen Değerler

                            string _gelenMessage = Response.EtReturn[verisayac1].Message;
                            string _gelenfield = Response.EtReturn[verisayac1].Field;
                            string _gelenlogmsgno = Response.EtReturn[verisayac1].LogMsgNo;
                            string _gelenlogno = Response.EtReturn[verisayac1].LogNo;
                            string _gelenmessage = Response.EtReturn[verisayac1].Message;
                            string _gelenmessagev1 = Response.EtReturn[verisayac1].MessageV1;
                            string _gelenmessagev2 = Response.EtReturn[verisayac1].MessageV2;
                            string _gelenmessagev3 = Response.EtReturn[verisayac1].MessageV3;
                            string _gelenmessagev4 = Response.EtReturn[verisayac1].MessageV4;
                            string _gelennumber = Response.EtReturn[verisayac1].Number;
                            string _gelenparameter = Response.EtReturn[verisayac1].Parameter;
                            string _gelenrow = Response.EtReturn[verisayac1].Row.ToString();
                            string _gelensystem = Response.EtReturn[verisayac1].System;
                            string _gelentype = Response.EtReturn[verisayac1].Type;

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

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;


            }
            catch (Exception)
            {

                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "";
            }
            return _Cevap;
        }

        internal SipariseEkleResponse fn_SipariseEkle(SipariseEkleRequest v_Gelen)
        {
            #region Değişkenler

            SipariseEkleResponse _Cevap = new SipariseEkleResponse();
            string _SiparisListesi = "";
         

            #endregion

            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    List<AcikSiparisListesiView> _PmSiparisListe = session.Query<tblzrfidorderlistreponse>().Where(t => t.aktif == 1).Select(t => new AcikSiparisListesiView()
                    {
                        zaufnr = t.aufnr
                    }).Distinct().ToList();
                    
                    _SiparisListesi += "<option value='-1'>SEÇİNİZ</option>";

                    foreach (var item in _PmSiparisListe)
                    {
                        _SiparisListesi += "<option value='"+item.zaufnr+"'>"+item.zaufnr+"</option>";
                    }

                    tblkimliklendirme _Epc = session.Query<tblkimliklendirme>().FirstOrDefault(e => e.aktif == 1 && e.gelenepc.Equals(v_Gelen.zepc));

                    _Cevap.zaufnrlistesi = _SiparisListesi;
                    _Cevap.zepc = v_Gelen.zepc;
                    _Cevap.zmaktx = _Epc.maktx;
                    _Cevap.zmatnr = _Epc.matnr;
                    _Cevap.zsern = _Epc.sernr;
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama="";
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

        internal AlarmKapatResponse fn_AlarmKapat(AlarmKapatRequest v_Gelen)
        {
            AlarmKapatResponse _Cevap = new AlarmKapatResponse();
            try
            {
                if (v_Gelen.zaciklama=="")
                {
                    _Cevap.zAciklama = "Açıklama Boş olamaz";
                    _Cevap.zSonuc = -1;
                }
                else
                {
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        tbl03kapireader tbl03 = session.Query<tbl03kapireader>().FirstOrDefault(t => t.aktif == 1 && t.gelenepc.Equals(v_Gelen.zeps));
                        tbl03.alarm = 2;
                        tbl03.alarmkapatmaaciklama = v_Gelen.zaciklama;
                        tbl03.alarmkapatankullanici = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        tbl03.alarmkapatmatarihi = DateTime.Now;
                        tbl03.guncellemezamani = DateTime.Now;
                        tbl03.lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        tbl03.Save();

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

        internal AlarmGoruntuleResponse fn_AlarmGoruntule(AlarmGoruntuleRequest v_Gelen)
        {
            AlarmGoruntuleResponse _Cevap = new AlarmGoruntuleResponse();
            string _TabloYazisi = "";
            string _TabloBasligi = "";


            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tbl03kapireader _Temp = session.Query<tbl03kapireader>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(v_Gelen.zeps));
                    
                    if (_Temp != null)
                    {
                        _TabloYazisi += "<tr>";
                        _TabloBasligi += "<tr>";


                        //_TabloBasligi += "<th style ='text-align: center'> Alarm Durumu </ th >";
                        //_TabloYazisi += "<td style='text-align:center;'>" + _Temp.alarm + "</td>";

                        _TabloBasligi += "<th style ='text-align: center'> Etiket Değeri </ th >";
                        _TabloYazisi += "<td id='epc' name='epc' style='text-align:center'  > " + "1"+_Temp.gelenepc + "</td>";


                        if (_Temp.alarm == 2)
                        {
                            _TabloBasligi += "<th style ='text-align: center'> Alarmı Kapatan </ th >";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Temp.alarmkapatankullanici + "</td>";
                            _TabloBasligi += "<th style ='text-align: center'> Alarmı Kapatma Tarihi </ th >";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Temp.alarmkapatmatarihi + "</td>";

                          
                        }

                        tblkimliklendirme _Temp2 = session.Query<tblkimliklendirme>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(v_Gelen.zeps));
                        if (_Temp2 != null)
                        {
                            _TabloBasligi += "<th style ='text-align: center'> Malzeme Adı </ th >";
                            _TabloBasligi += "<th style ='text-align: center'> Malzeme Kodu </ th >";
                            _TabloBasligi += "<th style ='text-align: center'> Seri No </ th >";


                            _TabloYazisi += "<td style='text-align:center;'>" + _Temp2.matnr + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Temp2.maktx + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Temp2.sernr + "</td>";
                            //   _TabloYazisi += " <td <button type='button' class='btn btn-outline-warning m-btn m-btn--icon m-btn--icon-only m-btn--custom m-btn--outline-2x m-btn--pill m-btn--air' onclick='fn_AlarmKapat(" + v_Gelen.zeps + ");'><i class='la la-folder'> </i></button></td>";


                        }

                        if (_Temp.alarm == 1 )
                        {
                            _TabloBasligi += "<th style ='text-align: center'> AÇIKLAMA</ th >";
                            //_TabloYazisi += " <td <input type='text' class='form - control m - input--air' /> AÇIKLAMA: </td>";
                            _TabloYazisi += " <td><input type = 'text' id = 'aciklama'autocomplete='off' name = 'aciklama' style='width:100% ;border:3px' ></td>";

                            _TabloBasligi += "<th style ='text-align: center'> Alarm Kapat </ th >";
                            _TabloYazisi += "<td style='text-align:center;'><a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick  = fn_AlarmKapat('" + v_Gelen.zeps + "');>Alarm Kapat</a></td>";

                            if (v_Gelen.zaufnr=="ilişkisiz" || v_Gelen.zmatnr!="Kimliksiz")
                            {
                                _TabloBasligi += "<th style ='text-align: center'> Siparişe Ekle </ th >";
                                _TabloYazisi += "<td style='text-align:center;'><a class='m-linkm-badge  m-badge--danger m-badge--wide' href=# onclick  = fn_SipariseEkle('" + v_Gelen.zeps + "');>Siparişe Ekle</a></td>";

                            }

                        }
                        _TabloYazisi += "</tr>";
                    }


                }

                _Cevap.zAciklama = "";
                _Cevap.zSonuc = 1;
                _Cevap.ztabloyazisi = _TabloYazisi;
                _Cevap.ztablobasligi = _TabloBasligi;

            }
            catch (Exception ex)
            {

                _Cevap.zAciklama = ex.ToString();
                _Cevap.zSonuc = -1;
                _Cevap.ztabloyazisi = "";
                _Cevap.ztablobasligi = "";
            }

            return _Cevap;
        }


    }
}