using AbdiIbrahim.Servis.YedekMalzeme.AcikPmSiparis.ServiceReference1;
using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AbdiIbrahim.Servis.YedekMalzeme.AcikPmSiparis
{
    public class cAcikPmSiparisiIslem
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cAcikPmSiparisiIslem));




       


        internal void fn_OrderList(object state)
        {
            #region Değişkenler


            int _BeklemeSuresi = 45;

            string UserName_d = "";
            string Password_d = "";

            string _ParamErdatLow = "";
            string _ParamErdatHigh = "";
            string _ParamIwerk = "";
            string _ParamAufnr = "";

            bool _LogAlindimi = false;
            int _iTemp = 0;



            #endregion

            try
            {

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    XpoManager.Instance.InitXpo();
                }

                while (true)
                {
                    _LogAlindimi = false;

                    #region Çalışmadığının loglaması
                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");

                        _BeklemeSuresi = int.Parse(ConfigurationManager.AppSettings["AcikPmSiparisiSuresi"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Error("Bekleme Süresi Değeri Çekilemedi =" + ex.ToString());
                    }

                    #endregion


                    #region Parametre Çek
                    try
                    {
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            tblzrfidorderlistparam _Temp = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w => w.aktif == 1);

                            if (_Temp == null)
                            {
                                _LogDosyasi.Error(" Aktif Parametre Değerleri Bulunamadı= ");

                                //Aktif parametre değeri yoksa diye bakılacak
                                continue;

                            }
                            else
                            {

                                if (_Temp.beklemesuresi == null || _Temp.beklemesuresi.Trim().Length == 0 || int.TryParse(_Temp.beklemesuresi, out _BeklemeSuresi) == false)
                                {
                                    _BeklemeSuresi = 45;
                                }
                                else
                                {
                                    _BeklemeSuresi = int.Parse(_Temp.beklemesuresi);
                                }


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

                        _LogDosyasi.Error("Parametre Değerleri Çekilmedi= " + ex.ToString());
                    }


                    #endregion


                    try
                    {

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
                            List<tblzrfidorderlistreponse> _Temp = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif == 1).ToList();

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

                        #region tbl06analiz ve tblzrfidorderlistreponse karşılaştırması
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            string aufnrthis = "";
                            int _countsap = 0;
                            int _countiliskili = 0;
                            //Sipariş numarası farklı olanları al.
                            List<tblzrfidorderlistreponse> _Liste2 = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif == 1).GroupBy(x => x.aufnr).Select(y => y.First()).ToList();

                            foreach (var item in _Liste2)
                            {

                                _countsap = session.Query<tblzrfidorderlistreponse>().Where(w => w.aktif==1 && w.aufnr==item.aufnr).ToList().Count;
                                List<tbl06analiz> _Tempanaliz = session.Query<tbl06analiz>().Where(w =>w.aktif == 1 && w.aufnr==item.aufnr).ToList();
                                _countiliskili = _Tempanaliz.Count;
                                aufnrthis = item.aufnr;

                                if (_countsap!=_countiliskili)
                                {

                                    List<tbl07sapfark> _Tempfark = session.Query<tbl07sapfark>().Where(w => w.aktif == 1 && w.aufnr == item.aufnr).ToList();

                                    if (_Tempfark.Count==0)
                                    {
                                        new tbl07sapfark(session)
                                        {
                                            aktif = 1,
                                            createuser = "aniventi",
                                            databasekayitzamani = DateTime.Now,
                                            guncellemezamani = DateTime.Now,
                                            id = Guid.NewGuid().ToString().ToUpper(),
                                            lastupdateuser = "aniventi",
                                            aufnr = aufnrthis,
                                            iliskiliSayisi = _countiliskili,
                                            toplamSayi = _countsap,
                                            emailgonderimi = 0

                                        }.Save();
                                    }
                                    else
                                    {

                                        _Tempfark.FirstOrDefault().iliskiliSayisi = _countiliskili;
                                        _Tempfark.FirstOrDefault().toplamSayi = _countsap;
                                        _Tempfark.FirstOrDefault().databasekayitzamani = DateTime.Now;
                                        _Tempfark.FirstOrDefault().guncellemezamani = DateTime.Now;
                                        _Tempfark.FirstOrDefault().emailgonderimi = 0;
                                        _Tempfark.FirstOrDefault().Save();

                                    }
                                    
                                }
                                else
                                {
                                    List<tbl07sapfark> _Tempfark = session.Query<tbl07sapfark>().Where(w => w.aktif == 1 && w.aufnr == item.aufnr).ToList();

                                    if (_Tempfark.Count!=0)
                                    {
                                        _Tempfark.FirstOrDefault().iliskiliSayisi = _countiliskili;
                                        _Tempfark.FirstOrDefault().toplamSayi = _countsap;
                                        _Tempfark.FirstOrDefault().databasekayitzamani = DateTime.Now;
                                        _Tempfark.FirstOrDefault().guncellemezamani = DateTime.Now;
                                        _Tempfark.FirstOrDefault().emailgonderimi = 1;
                                        _Tempfark.FirstOrDefault().aktif = 0;
                                        _Tempfark.FirstOrDefault().Save();
                                    }
                                }
                                
                            }
                        }
                        #endregion

                        _Client.Close();

                        _LogDosyasi.Error("Açık pm siparişleri Listelendi");
                    }
                    catch (Exception ex)
                    {

                        _LogDosyasi.Error(ex.ToString());
                    }

                    Thread.Sleep(TimeSpan.FromMinutes(_BeklemeSuresi));

                }

            }
            catch (Exception ex)
            {

                _LogDosyasi.Error(ex.ToString());
            }
        }
    }
}
