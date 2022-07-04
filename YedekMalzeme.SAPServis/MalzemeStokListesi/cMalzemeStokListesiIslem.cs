using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using YedekMalzeme.SAPServis.ServiceReference1;

namespace YedekMalzeme.SAPServis.MalzemeStokListesi
{
    public class cMalzemeStokListesiIslem
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cMalzemeStokListesiIslem));

        internal void fn_MalzemeStokListesiCek(object state)
        {
            #region
            int _BeklemeSuresi = 45;

            string UserName_d = "";
            string Password_d = "";
            string _Paramtart = "";
            string _Paramwerks = "";
            string _Paramlgort = "";
            #endregion

            try
            {
                while (true)
                {
                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");

                        _BeklemeSuresi = int.Parse(ConfigurationManager.AppSettings["MalzemeStokListesiBeklemeSuresi"].ToString().Trim());
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
                            tblmalzemestoklistesiparam _Temp = session.Query<tblmalzemestoklistesiparam>().FirstOrDefault(w => w.aktif == 1);

                            _Paramtart = _Temp.mtart;
                            _Paramwerks = _Temp.werks;
                            _Paramlgort = _Temp.lgort;

                        }
                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Error("Parametre Değerleri Çekilemedi =" + ex.ToString());
                    }
                    #endregion

                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");                


                        UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                        Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

                        ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                        _Client.ClientCredentials.UserName.UserName = UserName_d;
                        _Client.ClientCredentials.UserName.Password = Password_d;
                        _Client.Open();


                        ZrfidStrMaterialStock _ParamzrfidStrMaterialStock = new ZrfidStrMaterialStock();

                        _ParamzrfidStrMaterialStock.Lgort = "";
                        _ParamzrfidStrMaterialStock.Lgpbe = "";
                        _ParamzrfidStrMaterialStock.Maktx = "";
                        _ParamzrfidStrMaterialStock.Matnr = "";
                        _ParamzrfidStrMaterialStock.Meins = "";
                        _ParamzrfidStrMaterialStock.Mtart = "";
                        _ParamzrfidStrMaterialStock.Mtbez = "";
                        _ParamzrfidStrMaterialStock.Werks = "";
                        _ParamzrfidStrMaterialStock.Labst = 0;
                        _ParamzrfidStrMaterialStock.Speme = 0;

                        ZrfidStrMaterialStock[] _DiziParamzrfidStrMaterialStock = new ZrfidStrMaterialStock[1];
                        _DiziParamzrfidStrMaterialStock[0] = _ParamzrfidStrMaterialStock;

                        ZrfidStrMaterialStock[] _DiziParamzrfidStrMaterialStock2 = new ZrfidStrMaterialStock[1];
                        _DiziParamzrfidStrMaterialStock[0] = _ParamzrfidStrMaterialStock;


                        #region ItMtart
                        ZrfidStrMtart _Eleman_01 = new ZrfidStrMtart();
                        _Eleman_01.Mtart = _Paramtart;

                        ZrfidStrMtart[] _Dizi_01 = new ZrfidStrMtart[1];
                        _Dizi_01[0] = _Eleman_01;
                        #endregion


                        #region ItIwerk
                        ZrfidStrIwerk _Eleman_02 = new ZrfidStrIwerk();
                        _Eleman_02.Iwerk = _Paramwerks;

                        ZrfidStrIwerk[] _Dizi_02 = new ZrfidStrIwerk[1];
                        _Dizi_02[0] = _Eleman_02;


                        #endregion

                        #region ItLgort
                        ZrfidStrLgort _Eleman_03 = new ZrfidStrLgort();
                        _Eleman_03.Lgort = _Paramlgort;


                        ZrfidStrLgort[] _Dizi_03 = new ZrfidStrLgort[1];
                        _Dizi_03[0] = _Eleman_03;
                        #endregion

                        ZrfidMaterialStockList _RequestStoke = new ZrfidMaterialStockList();

                        _RequestStoke.ItIwerk = _Dizi_02;
                        _RequestStoke.ItLgort = _Dizi_03;
                        _RequestStoke.ItMtart = _Dizi_01;

                        _RequestStoke.EtMaterialstock = _DiziParamzrfidStrMaterialStock;



                        #region
                        ZrfidStrSerialNumber _Eleman = new ZrfidStrSerialNumber();
                        _Eleman.Aciklama = "";
                        _Eleman.BLager = "";
                        _Eleman.BWerk = "";
                        _Eleman.Maktx = "";
                        _Eleman.Matnr = "";
                        _Eleman.Sernr = "";

                        ZrfidStrSerialNumber[] _DizimSerial = new ZrfidStrSerialNumber[1];
                        _DizimSerial[0] = _Eleman;


                        _RequestStoke.EtSerialnumber = _DizimSerial;
                        #endregion



                        ZrfidMaterialStockListResponse _CevapStoke = _Client.ZrfidMaterialStockList(_RequestStoke);


                        #region Önce Hepsini pasif yap

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemestoklistesiresponse> _Liste = session.Query<tblmalzemestoklistesiresponse>().Where(w => w.aktif == 1).ToList();

                            foreach (var item in _Liste)
                            {
                                item.aktif = 0;
                                item.Save();
                            }
                          }
                        #endregion


                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            for (int _veriSayac = 0; _veriSayac < _CevapStoke.EtMaterialstock.Length; _veriSayac++)
                            {
                                string _gelenMatnr = _CevapStoke.EtMaterialstock[_veriSayac].Matnr;
                                string _gelenLgort = _CevapStoke.EtMaterialstock[_veriSayac].Lgort;
                                string _gelenMaktx = _CevapStoke.EtMaterialstock[_veriSayac].Maktx;
                                string _gelenMeins = _CevapStoke.EtMaterialstock[_veriSayac].Meins;
                                string _gelenMtart = _CevapStoke.EtMaterialstock[_veriSayac].Mtart;
                                string _gelenMtbez = _CevapStoke.EtMaterialstock[_veriSayac].Mtbez;
                                string _gelenWerks = _CevapStoke.EtMaterialstock[_veriSayac].Werks;
                                string _gelenLabst = _CevapStoke.EtMaterialstock[_veriSayac].Labst + "";
                                string _gelenSpeme = _CevapStoke.EtMaterialstock[_veriSayac].Speme + "";
                                string _gelenLgpbe = _CevapStoke.EtMaterialstock[_veriSayac].Lgpbe + "";

                                tblmalzemestoklistesiresponse _Temp = session.Query<tblmalzemestoklistesiresponse>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));

                                if (_Temp != null)
                                {
                                    _Temp.aktif = 1;
                                    _Temp.Save();
                                }

                                else
                                {
                                    new tblmalzemestoklistesiresponse(session)
                                    {
                                        labst= _gelenLabst,                                        
                                        lgort=_gelenLgort,
                                        lgpbe=_gelenLgpbe,
                                        speme=_gelenSpeme,
                                        aktif = 1,
                                        createuser = "aniventi",
                                        databasekayitzamani = DateTime.Now,
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        lastupdateuser = "aniventi",
                                        maktx = _gelenMaktx,
                                        matnr = _gelenMatnr,
                                        meins = _gelenMeins,
                                        mtart = _gelenMtart,
                                        mtbez = _gelenMtbez,
                                        werks = _gelenWerks
                                    }.Save();
                                }                                

                            }  
                            _Client.Close();
                        }

                        #region Pasif kayıtların son güncellenme zamanını tut
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemestoklistesiresponse> _Liste = session.Query<tblmalzemestoklistesiresponse>().Where(w => w.aktif == 0).ToList();

                            foreach (var item in _Liste)
                            {
                                item.lastupdateuser = "aniventi";
                                item.guncellemezamani = DateTime.Now;
                                item.Save();
                            }
                        }
                        #endregion


                        #region Seri numaralarını almaya başla

                        #region Önce Seri Numaralarını Pasif Et
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemeserialstoklistesi> _Liste = session.Query<tblmalzemeserialstoklistesi>().Where(w => w.aktif == 1).ToList();

                            foreach (var item in _Liste)
                            {
                                item.aktif = 0;
                                item.Save();
                            }
                        }
                        #endregion

                        #region Servisten gelenlerle güncelle
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            for (int _veriSayac = 0; _veriSayac < _CevapStoke.EtSerialnumber.Length; _veriSayac++)
                            {
                                string _gelenMatnr = _CevapStoke.EtSerialnumber[_veriSayac].Matnr;
                                string _gelenMaktx = _CevapStoke.EtSerialnumber[_veriSayac].Maktx;
                                string _Aciklama = _CevapStoke.EtSerialnumber[_veriSayac].Aciklama;
                                string _BLager = _CevapStoke.EtSerialnumber[_veriSayac].BLager;
                                string _BWerk = _CevapStoke.EtSerialnumber[_veriSayac].BWerk;
                                string _Sernr = _CevapStoke.EtSerialnumber[_veriSayac].Sernr;

                                tblmalzemeserialstoklistesi _Temp = session.Query<tblmalzemeserialstoklistesi>().FirstOrDefault(w => w.sernr.Equals(_Sernr));

                                if (_Temp != null)
                                {
                                    _Temp.aktif = 1;
                                    _Temp.Save();
                                }

                                else
                                {
                                    new tblmalzemeserialstoklistesi(session)
                                    {

                                        aktif = 1,
                                        createuser = "aniventi",
                                        databasekayitzamani = DateTime.Now,
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        lastupdateuser = "aniventi",
                                        maktx = _gelenMaktx,
                                        matnr = _gelenMatnr,
                                        aciklama = _Aciklama,
                                        sernr = _Sernr,
                                        blager = _BLager,
                                        bwerk = _BWerk
                                    }.Save();
                                }

                            }
                            _Client.Close();
                        }
                        #endregion

                        #region Pasif kayıtların son güncellenme zamanını tut
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemeserialstoklistesi> _Liste = session.Query<tblmalzemeserialstoklistesi>().Where(w => w.aktif == 0).ToList();

                            foreach (var item in _Liste)
                            {
                                item.lastupdateuser = "aniventi";
                                item.guncellemezamani = DateTime.Now;
                                item.Save();
                            }
                        }
                        #endregion

                        #endregion

                        _LogDosyasi.Error("MalzemeStokListesiIslemleri bitti");
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
