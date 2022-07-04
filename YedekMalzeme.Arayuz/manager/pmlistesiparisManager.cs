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
    public class pmlistesiparisManager
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




        internal SiparisBilesenDegistirResponse fn_SiparisBilesenDegistir(SiparisBilesenDegistirRequest v_Gelen)
        {
            #region Değişkenler
            string _IslemKodu = "0";
            string _Posnr = "0";
            string _Rsnum = "";
            string _RsPos = "";
            int _EkliMalzemeSayisi = 0;
            string _IstekNo = "";

            SiparisBilesenDegistirResponse _Cevap = new SiparisBilesenDegistirResponse();
            #endregion
            try
            {

                if (v_Gelen.zaufnr.ToString().StartsWith("00") == false)
                {
                    //v_Gelen.zaufnr = "00" + v_Gelen.zaufnr;
                }
                
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    #region Sipariş Detayı
                    tblzrfidorderlistreponse _OrderList = session.Query<tblzrfidorderlistreponse>().FirstOrDefault(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr));
                    _Posnr = _OrderList.posnr;
                    _Rsnum = _OrderList.rsnum;
                    _RsPos = _OrderList.rspos;
                    #endregion


                    #region Ekli Malzeme Sayısı
                    List<tbl01eklecikararsiv> _EpcList = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr) && w.matnr.Equals(v_Gelen.zmatnr)).ToList();
                    _EkliMalzemeSayisi = _EpcList.Count;
                    #endregion

                    #region Önce EPC ekli mi kontrol

                    tbl01eklecikararsiv _TempEpc = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(v_Gelen.zepc) && w.aufnr.Equals(v_Gelen.zaufnr));

                    if (_TempEpc != null)
                    {
                        _IslemKodu = "100"; // epc ekli ise siparişten çıkar
                    }
                    else
                    {
                        _TempEpc = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr) && w.matnr.Equals(v_Gelen.zmatnr));

                        if (_TempEpc != null)
                        {
                            _IslemKodu = "101"; // aynı malzeme kodundan yine ekleyiorum
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
                        gelenepc = v_Gelen.zepc,
                        guncellemezamani = DateTime.Now,
                        id = Guid.NewGuid().ToString().ToUpper(),
                        islemkodu = _IslemKodu,
                        islemturu = _IslemKodu,
                        kullanici = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                        maktx = v_Gelen.zmaktx,
                        matnr = v_Gelen.zmatnr,
                        sernr = v_Gelen.zsernr
                    };

                    _Takip.Save();

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


                }

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
                        if (_Param.Bdmng >0)
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
                            using (Session session = XpoManager.Instance.GetNewSession())
                            {
                                List<tbl01eklecikararsiv> _arsivtemp = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 && w.sernr.Equals(v_Gelen.zsernr)).ToList();
                                if (_arsivtemp==null)
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
                }
                _Param.Lgort = "1910";
                _Param.Matnr = v_Gelen.zmatnr;
                _Param.Meins = "ADT";
                //_Param.Posnr = textBoxPosnr.Text;
                _Param.Postp = "";
                _Param.Rgekz = "";

                _Param.Vornr = "0010";
                _Param.Wempf ="rfid_"+ HttpContext.Current.Session["KullaniciAdi"].ToString();
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

                using (Session session = XpoManager.Instance.GetNewSession())
                {
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


                        // tblzrfidorderlistreturn _Return = session.Query<tblzrfidorderlistreturn>().FirstOrDefault(w => w.message.Equals(_Cevap.EtReturn[verisayac1].Message));

                        if (_gelenMessage != null && _gelenMessage != "")
                        {
                            new tblzrfidorderlistreturn(session)
                            {
                                istekno= _IstekNo,
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
                            _HataMesajlari += _iSayac +" -" +_Yanit.message + "\n";
                        }
                    }

                    if (_IslemBasarili == true)
                    {
                        _Cevap = new SiparisBilesenDegistirResponse();

                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "";

                        tbl01eklecikararsiv _ArsivTemp = new tbl01eklecikararsiv(session)
                        {
                            aktif = 1,
                            aufnr = v_Gelen.zaufnr,
                            createuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            databasekayitzamani = DateTime.Now,
                            gelenepc = v_Gelen.zepc,
                            guncellemezamani = DateTime.Now,
                            id = Guid.NewGuid().ToString().ToUpper(),
                            islemturu = _IslemKodu,
                            kullanici = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            lastupdateuser = HttpContext.Current.Session["KullaniciAdi"].ToString(),
                            maktx = v_Gelen.zmaktx,
                            matnr = v_Gelen.zmatnr,
                            sernr = v_Gelen.zsernr
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

                        return _Cevap;
                    }

                    else
                    {
                        _Cevap = new SiparisBilesenDegistirResponse();

                        _Cevap.zSonuc = -1;
                        _Cevap.zAciklama = _HataMesajlari;

                        return _Cevap;
                    }

                    #endregion


                }


                #endregion

            }
            catch (Exception ex)
            {
                _Cevap = new SiparisBilesenDegistirResponse();

                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }

        }


        internal EpcKimlikBilgileriResponse fn_EpcKimlikBilgileri(EpcKimlikBilgileriRequest v_Gelen)
        {
            #region Değişkenler
            EpcKimlikBilgileriResponse _Cevap = new EpcKimlikBilgileriResponse();

         
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirme _Temp = session.Query<tblreaderkimliklendirme>().Where(w => w.aktif == 1).OrderByDescending(w => w.sonokuma).FirstOrDefault();

                    if (_Temp == null)
                    {
                        _Cevap = new EpcKimlikBilgileriResponse();
                        _Cevap.zSonuc = -1;
                        _Cevap.zepc = "";
                        _Cevap.zmaktx = "";
                        _Cevap.zmatnr = "";
                        _Cevap.zsernr = "";
                        _Cevap.zAciklama = "";

                        return _Cevap;
                    }
                    else
                    {
                        tblkimliklendirme _TempKimlik = session.Query<tblkimliklendirme>().FirstOrDefault(w => w.aktif == 1 && w.gelenepc.Equals(_Temp.gelenepc));

                        if (_TempKimlik == null)
                        {
                            _Cevap = new EpcKimlikBilgileriResponse();
                            _Cevap.zSonuc = -1;
                            _Cevap.zepc = "";
                            _Cevap.zmaktx = "";
                            _Cevap.zmatnr = "";
                            _Cevap.zsernr = "";
                            _Cevap.zAciklama = "";

                            return _Cevap;
                        }
                        else
                        {
                            tbl01eklecikararsiv _ArsivTemp = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr) && w.gelenepc.Equals(_TempKimlik.gelenepc));

                            _Cevap = new EpcKimlikBilgileriResponse();
                            _Cevap.zSonuc = 1;
                            _Cevap.zepc = _TempKimlik.gelenepc;
                            _Cevap.zmaktx = _TempKimlik.maktx;
                            _Cevap.zmatnr = _TempKimlik.matnr;
                            _Cevap.zsernr = _TempKimlik.sernr;
                            _Cevap.zAciklama = "";

                            if (_ArsivTemp == null)
                            {
                                _Cevap.zeklesil = "1";
                            }
                            if (_ArsivTemp != null)
                            {
                                _Cevap.zeklesil = "-1";
                            }


                            return _Cevap;
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                _Cevap = new EpcKimlikBilgileriResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

            }
            #endregion

            return _Cevap;
        }

        

        internal AcikPmListeleResponse fn_AcikPmListele(AcikPmListeleRequest v_Gelen)
        {
            #region Değişkenler

            string _TabloYazisi = "";

            string _LinkYazisi = "";

            AcikPmListeleResponse _Cevap = new AcikPmListeleResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    // List<tblzrfidorderlistreponse> _ItemDizi = session.Query<tblzrfidorderlistreponse>().Select(w => w.aktif == 1).Distinct(w=>w) .ToList();

                    if (v_Gelen.zdepo.Equals("0"))
                    {

                        List<AcikPmListeleView> _ItemDizi = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif == 1 && w.aufnr.Trim().Length > 5).Select(w => new AcikPmListeleView()
                        {
                            zaufnr = w.aufnr,
                            zgiristarihi = w.erdat
                        }).Distinct().OrderBy(w => w.zaufnr).ToList();


                        _TabloYazisi = "";

                        foreach (var _Item in _ItemDizi)
                        {
                            _LinkYazisi = "<a class='m-link' href=# onclick =jsIncele('" + _Item.zaufnr + "') >İncele</a>";

                            _TabloYazisi += "<tr>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item.zaufnr + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _Item.zgiristarihi + "</td>";
                            _TabloYazisi += "<td style='text-align:center;'>" + _LinkYazisi + "</td>";
                            _TabloYazisi += "</tr>";
                        }

                        _Cevap = new AcikPmListeleResponse();
                        _Cevap.zSonuc = 1;
                        _Cevap.zAciklama = "İşlem Başarılı";
                        _Cevap.zListeYazisi = _TabloYazisi;
                    }
                    else
                    {
                        if (v_Gelen.zdepo.Equals("1"))
                        {

                            List<AcikPmListeleView> _ItemDizi = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 ).Select(w => new AcikPmListeleView()
                            {
                                zaufnr = w.aufnr
                            }).Distinct().OrderBy(w => w.zaufnr).ToList();


                            _TabloYazisi = "";

                            foreach (var _Item in _ItemDizi)
                            {
                                _LinkYazisi = "<a class='m-link' href=# onclick =jsIncele('" + _Item.zaufnr + "') >İncele</a>";

                                _TabloYazisi += "<tr>";
                                _TabloYazisi += "<td style='text-align:center;'>" + _Item.zaufnr + "</td>";
                                _TabloYazisi += "<td style='text-align:center;'>" + _Item.zgiristarihi + "</td>";
                                _TabloYazisi += "<td style='text-align:center;'>" + _LinkYazisi + "</td>";
                                _TabloYazisi += "</tr>";
                            }

                            _Cevap = new AcikPmListeleResponse();
                            _Cevap.zSonuc = 1;
                            _Cevap.zAciklama = "İşlem Başarılı";
                            _Cevap.zListeYazisi = _TabloYazisi;
                        }

                    }
                }



            }
            catch (Exception ex)
            {
                _Cevap = new AcikPmListeleResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "Sistemsel bir hata oluştu.";
                _Cevap.zListeYazisi = "";

                return _Cevap;
            }

            return _Cevap;
        }

        

        internal SiparisMalzemeListesiResponse fn_SiparisMalzemeListesi(SiparisMalzemeListesiRequest v_Gelen)
        {
            #region Değişkenler

            string _TabloYazisi = "";

            SiparisMalzemeListesiResponse _Cevap = new SiparisMalzemeListesiResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    _TabloYazisi = "";

                    List<tblzrfidorderlistreponse> _ItemDizi = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr)).OrderBy(w=>w.matnr).ToList();

                    foreach (var _Item in _ItemDizi)
                    {
                        _TabloYazisi += "<tr>";
                        _TabloYazisi += "<td style='text-align:center;'>"+ _Item.matnr + "</td>";
                        _TabloYazisi += "<td>"+ _Item.maktx + "</td>";
                        _TabloYazisi += "<td style='text-align:right;'>" + _Item.bdmng + "</td>";
                        _TabloYazisi += "<td>"+ _Item.meins + "</td>";
                        _TabloYazisi += "<td>"+ _Item.vornr + "</td>";
                        _TabloYazisi += "</tr>";
                    }

                    _Cevap = new SiparisMalzemeListesiResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.ztabloyazisi = _TabloYazisi;

                    return _Cevap;
                }
            }
            catch (Exception ex)
            {
                _Cevap = new SiparisMalzemeListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = ex.ToString();

                return _Cevap;
            }

            return _Cevap;
        }
    }
}