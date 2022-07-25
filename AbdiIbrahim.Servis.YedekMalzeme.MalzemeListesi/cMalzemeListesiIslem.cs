using AbdiIbrahim.Servis.YedekMalzeme.MalzemeListesi.ServiceReference1;
using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace AbdiIbrahim.Servis.YedekMalzeme.MalzemeListesi
{
    public class cMalzemeListesiIslem
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cMalzemeListesiIslem));

        internal void fn_MalzemeListesiCek(object state)
        {
            #region
            int _BeklemeSuresi = 45;

            string UserName_d = "";
            string Password_d = "";

            string _Paramtart = "";
            string _Paramwerks = "";

            bool _LogAlindimi = false;

            #endregion

            try
            {
                while (true)
                {
                    _LogAlindimi = false;

                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");

                        _BeklemeSuresi = int.Parse(ConfigurationManager.AppSettings["MalzemeListesiBeklemeSuresi"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Error("Bekleme Süresi Değeri Çekilemedi =" + ex.ToString());
                    }


                    #region Paramatreleri Çek

                    try
                    {
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            tblmalzemelistesiparam _Temp = session.Query<tblmalzemelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                            _Paramtart = _Temp.mtart;

                            _Paramwerks = _Temp.werks;

                        }
                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Error("Parametre Değerleri Çekilemedi =" + ex.ToString());
                    }
                    #endregion

                    try
                    {
                        UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                        Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

                        ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                        _Client.ClientCredentials.UserName.UserName = UserName_d;
                        _Client.ClientCredentials.UserName.Password = Password_d;
                        _Client.Open();

                        ZrfidStrMaterialList _ParamMetarialList = new ZrfidStrMaterialList();

                        _ParamMetarialList.Maktx = "";
                        _ParamMetarialList.Matnr = "";
                        _ParamMetarialList.Meins = "";
                        _ParamMetarialList.Mtart = "";
                        _ParamMetarialList.Mtbez = "";
                        _ParamMetarialList.Werks = "";


                        #region iwerk
                        ZRFID_STR_IWERK _Eleman_01 = new ZRFID_STR_IWERK();
                        _Eleman_01.IWERK = _Paramwerks;

                        ZRFID_STR_IWERK[] _Dizi_01 = new ZRFID_STR_IWERK[1];
                        _Dizi_01[0] = _Eleman_01;
                        #endregion

                        #region mtart
                        ZrfidStrMtart _Eleman_02 = new ZrfidStrMtart();
                        _Eleman_02.Mtart = _Paramtart;

                        ZrfidStrMtart[] _Dizi_02 = new ZrfidStrMtart[1];
                        _Dizi_02[0] = _Eleman_02;
                        #endregion



                        ZrfidStrMaterialList[] _DiziParamMetarialList = new ZrfidStrMaterialList[1];
                        _DiziParamMetarialList[0] = _ParamMetarialList;

                        ZrfidMaterialList _Request = new ZrfidMaterialList();
                        _Request.EtMaterialList = _DiziParamMetarialList;
                        _Request.ItIwerk = _Dizi_01;
                        _Request.ItMtart = _Dizi_02;

                        ZrfidMaterialListResponse _Cevap = _Client.ZrfidMaterialList(_Request);

                        #region Önce Tüm Malzeme Listesi Pasif Yap                      

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemelistesiresponse> _Temp = session.Query<tblmalzemelistesiresponse>().Where(w => w.aktif == 1).ToList();

                            foreach (var item in _Temp)
                            {
                                item.aktif = 0;
                                item.Save();
                            }
                        }
                        #endregion

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            for (int _veriSayac = 0; _veriSayac < _Cevap.EtMaterialList.Length; _veriSayac++)
                            {

                                string _gelenMatnr = _Cevap.EtMaterialList[_veriSayac].Matnr;
                                string _gelenMeins = _Cevap.EtMaterialList[_veriSayac].Meins;
                                string _gelenMaktx = _Cevap.EtMaterialList[_veriSayac].Maktx;
                                string _gelenMtart = _Cevap.EtMaterialList[_veriSayac].Mtart;
                                string _gelenMtbez = _Cevap.EtMaterialList[_veriSayac].Mtbez;
                                string _gelenWerks = _Cevap.EtMaterialList[_veriSayac].Werks;

                                tblmalzemelistesiresponse _Temp = session.Query<tblmalzemelistesiresponse>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));

                                if (_Temp == null)
                                {
                                    new tblmalzemelistesiresponse(session)
                                    {

                                        aktif = 1,
                                        databasekayitzamani = DateTime.Now,
                                        createuser = "servis",
                                        lastupdateuser = "servis",
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString(),
                                        maktx = _gelenMaktx,
                                        matnr = _gelenMatnr,
                                        meins = _gelenMeins,
                                        mtart = _gelenMtart,
                                        mtbez = _gelenMtbez,
                                        werks = _gelenWerks

                                    }.Save();

                                }
                                else
                                {
                                    _Temp.aktif = 1;
                                    _Temp.Save();

                                    if (_Temp.maktx.Equals(_gelenMaktx) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemelistesiresponse(session)
                                            {
                                                aktif = 1,
                                                createuser = _Temp.createuser,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                guncellemezamani = DateTime.Now,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = _Temp.lastupdateuser,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                mtart = _Temp.mtart,
                                                mtbez = _Temp.mtbez,
                                                werks = _Temp.werks,
                                                degisenalan = "maktx"
                                            }.Save();
                                        }

                                        _Temp.maktx = _gelenMaktx;
                                        _Temp.lastupdateuser = "servis";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }

                                    if (_Temp.meins.Equals(_gelenMeins) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemelistesiresponse(session)
                                            {
                                                aktif = 1,
                                                createuser = _Temp.createuser,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                guncellemezamani = DateTime.Now,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = _Temp.lastupdateuser,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                mtart = _Temp.mtart,
                                                mtbez = _Temp.mtbez,
                                                werks = _Temp.werks,
                                                degisenalan = "meins"
                                            }.Save();
                                        }

                                        _Temp.meins = _gelenMeins;
                                        _Temp.lastupdateuser = "servis";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }

                                    if (_Temp.mtart.Equals(_gelenMtart) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemelistesiresponse(session)
                                            {
                                                aktif = 1,
                                                createuser = _Temp.createuser,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                guncellemezamani = DateTime.Now,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = _Temp.lastupdateuser,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                mtart = _Temp.mtart,
                                                mtbez = _Temp.mtbez,
                                                werks = _Temp.werks,
                                                degisenalan = "mtart"
                                            }.Save();
                                        }

                                        _Temp.mtart = _gelenMtart;
                                        _Temp.lastupdateuser = "servis";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }

                                    if (_Temp.mtbez.Equals(_gelenMtbez) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemelistesiresponse(session)
                                            {
                                                aktif = 1,
                                                createuser = _Temp.createuser,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                guncellemezamani = DateTime.Now,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = _Temp.lastupdateuser,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                mtart = _Temp.mtart,
                                                mtbez = _Temp.mtbez,
                                                werks = _Temp.werks,
                                                degisenalan = "mtbez"
                                            }.Save();
                                        }

                                        _Temp.mtbez = _gelenMtbez;
                                        _Temp.lastupdateuser = "servis";
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
                                    new tblmalzemelistesireturn(session)
                                    {
                                        aktif = 1,
                                        createuser = "servis,",
                                        databasekayitzamani = DateTime.Now,
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        lastupdateuser = "servis,",
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


                        #region aktif 0 olanların son güncellenme zamanının Yaz

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemelistesiresponse> _Temp = session.Query<tblmalzemelistesiresponse>().Where(w => w.aktif == 0).ToList();

                            foreach (var item in _Temp)
                            {
                                item.lastupdateuser = "servis";
                                item.guncellemezamani = DateTime.Now;
                                item.Save();
                            }
                        }

                        #endregion

                        _Client.Close();

                        _LogDosyasi.Error("Aktarım Tamamlandı");

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
