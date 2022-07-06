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

namespace YedekMalzeme.Arayuz.manager
{
    public class KimliklendirmeIptalManager
    {
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

        internal KimlikMalzemeIptalResponse fn_KimlikMalzemeIptal(KimlikMalzemeIptalRequest v_gelen)
        {
          #region Değişkenler

          KimlikMalzemeIptalResponse _cevap = new KimlikMalzemeIptalResponse();
            int _EkliMalzemeSayisi = 0;

            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {

                    tbl01eklecikararsiv _eklecikarTemp = session.Query<tbl01eklecikararsiv>().FirstOrDefault(a => a.aktif == 1 && a.sernr.Equals(v_gelen.zsernr));
                    if (_eklecikarTemp != null)
                    {

                        new tbl01eklecikararsiv(session)
                        {
                            aktif = 0,
                            sernr = _eklecikarTemp.sernr,
                            aufnr = _eklecikarTemp.aufnr,
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            gelenepc = _eklecikarTemp.gelenepc,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            islemturu = "100",
                            kullanici = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = _eklecikarTemp.maktx,
                            matnr = _eklecikarTemp.matnr
                        }.Save();

                        _eklecikarTemp.aktif = 0;
                        _eklecikarTemp.Save();

                        

                        tblzrfidorderlistreponse _siparisdetay = session.Query<tblzrfidorderlistreponse>().FirstOrDefault(s => s.aktif == 1 && s.aufnr.Equals(_eklecikarTemp.aufnr) && s.matnr.Equals(_eklecikarTemp.matnr));
                        if (_siparisdetay != null)
                        {

                            #region Ekli Malzeme Sayısı
                            List<tbl01eklecikararsiv> _EpcList = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 && w.aufnr.Equals(_siparisdetay.aufnr) && w.matnr.Equals(_siparisdetay.matnr)).ToList();
                            _EkliMalzemeSayisi = _EpcList.Count;
                            #endregion

                            string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                            string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

                            ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                            _Client.ClientCredentials.UserName.UserName = UserName_d;
                            _Client.ClientCredentials.UserName.Password = Password_d;
                            _Client.Open();


                            ZrfidStrComponentUpdate component = new ZrfidStrComponentUpdate();
                            component.Ablad = "";
                            component.Lgort = _siparisdetay.lgort;
                            component.Matnr = _siparisdetay.matnr;
                            component.Meins = _siparisdetay.meins;
                            component.Posnr = _siparisdetay.posnr;
                            component.Postp = "";
                            component.Rgekz = "";
                            component.Rsnum = _siparisdetay.rsnum;
                            component.Rspos = _siparisdetay.rspos;
                            component.Vornr = _siparisdetay.vornr;
                            component.Wempf = "rfid_" + HttpContext.Current.Session["KullaniciAdi"].ToString();
                            component.Werks = _siparisdetay.werks;




                            if (_EkliMalzemeSayisi > 1)
                            {
                                component.Bdmng = _EkliMalzemeSayisi;
                                component.Indc = "C";
                            }

                            if (_EkliMalzemeSayisi == 1)
                            {
                                component.Bdmng = _EkliMalzemeSayisi;
                                component.Indc = "D";
                            }


                            ZrfidStrComponentUpdate[] _diziComponent = new ZrfidStrComponentUpdate[1];
                            _diziComponent[0] = component;

                            ZrfidComponentUpdate request = new ZrfidComponentUpdate();
                            request.ItComponent = _diziComponent;
                            ZrfidComponentUpdateResponse _response = _Client.ZrfidComponentUpdate(request);

                            _Client.Close();
                        }


                        BilesenGuncelleRequest v_giden = new BilesenGuncelleRequest();
                        v_giden.zaufnr = _eklecikarTemp.aufnr;
                        fn_BilesenGuncelle(v_giden);








                }

                    tblkimliklendirme _tempkimliklendirme = session.Query<tblkimliklendirme>().FirstOrDefault(b => b.aktif == 1 && b.gelenepc.Equals(v_gelen.zepc) && b.matnr.Equals(v_gelen.zmatnr) && b.sernr.Equals(v_gelen.zsernr) && b.maktx.Equals(v_gelen.zmaktx));
                    if (_tempkimliklendirme != null)
                    {
                        new tbl04arsivkimliklendirmeiptal(session)
                        {
                            aktif = 1,
                            createuser = _tempkimliklendirme.createuser,
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            kullaniciadi = _tempkimliklendirme.kullaniciadi,
                            gelenepc = _tempkimliklendirme.gelenepc,
                            lastupdateuser = _tempkimliklendirme.lastupdateuser,
                            maktx = _tempkimliklendirme.maktx,
                            matnr = _tempkimliklendirme.matnr,
                            sernr = _tempkimliklendirme.sernr
                        }.Save();

                        tbl06analiz _analizguncelle = session.Query<tbl06analiz>().FirstOrDefault(a => a.aktif == 1 && a.epc.Equals(v_gelen.zepc) && a.matnr.Equals(v_gelen.zmatnr) && a.maktx.Equals(v_gelen.zmaktx));

                        _analizguncelle.kimlikiptaleden = HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _analizguncelle.lastupdateuser= HttpContext.Current.Session["KullaniciAdi"].ToString();
                        _analizguncelle.guncellemezamani = DateTime.Now;
                        _analizguncelle.aktif = 0;
                        _analizguncelle.Save();


                        _tempkimliklendirme.aktif = 0;
                        _tempkimliklendirme.Save();

                    }
                }

                _cevap.zSonuc = 1;
                _cevap.zAciklama = "";
            }
            catch (Exception)
            {
                _cevap.zSonuc = -1;
                _cevap.zAciklama = "";

            }
            return _cevap;
        }

        internal KimlikIptalMalzemeGetirResponse fn_KimlikIptalMalzemeGetir(KimlikIptalMalzemeGetirRequest v_gelen)
        {
            #region Değişkenler

            KimlikIptalMalzemeGetirResponse _cevap = new KimlikIptalMalzemeGetirResponse();

            #endregion

            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {
                    tblkimliklendirme _kimliklendirme = session.Query<tblkimliklendirme>().FirstOrDefault(v => v.aktif == 1 && v.gelenepc.Equals(v_gelen.zepc));
                    if (_kimliklendirme!=null)
                    {
                        _cevap.zmatnr = _kimliklendirme.matnr;
                        _cevap.zmaktx = _kimliklendirme.maktx;
                        _cevap.zsernr = _kimliklendirme.sernr;
                    }

                    else
                    {
                        _cevap.zmatnr = "";
                        _cevap.zmaktx = "";
                        _cevap.zsernr = "";
                    }

                    _cevap.zAciklama = "";
                    _cevap.zSonuc = 1;
                }
            }
            catch (Exception)
            {
                _cevap.zmaktx = "";
                _cevap.zmatnr = "";
                _cevap.zsernr = "";
                _cevap.zSonuc = -1;
                _cevap.zAciklama = "";
            }

            return _cevap;
        }

        internal KimlikIptalEpcGetirResponse fn_KimlikIptalEpcGetir(KimlikIptalEpcGetirRequest v_gelen)
        {
            #region Değişkenler

            KimlikIptalEpcGetirResponse _Cevap = new KimlikIptalEpcGetirResponse();

            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirme _Temp = session.Query<tblreaderkimliklendirme>().Where(w => w.aktif == 1).OrderByDescending(w => w.sonokuma).FirstOrDefault();

                    if (_Temp != null)
                    {
                        _Cevap = new KimlikIptalEpcGetirResponse();
                        _Cevap.zepc = _Temp.gelenepc;
                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "";
                    }

                    else
                    {
                        _Cevap = new KimlikIptalEpcGetirResponse();
                        _Cevap.zepc = "";
                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "";
                    }
                }
            }
            catch (Exception ex)
            {
                _Cevap = new KimlikIptalEpcGetirResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }


            return _Cevap;
        }
    }
}