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

namespace YedekMalzeme.SAPServis.MalzemeListesi
{
    public class cIslem
    {
        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cIslem));

        internal void fn_MalzemeListesiCek(object state)
        {
            #region Değişkeler

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
                        _LogDosyasi.Error("Bekleme Süresi Değeri Çekilemedi ="+ ex.ToString());
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
                                        createuser="aniventi",
                                        lastupdateuser="aniventi",
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString(),
                                        maktx = _Cevap.EtMaterialList[_veriSayac].Maktx,
                                        matnr = _Cevap.EtMaterialList[_veriSayac].Matnr,
                                        meins = _Cevap.EtMaterialList[_veriSayac].Meins,
                                        mtart = _Cevap.EtMaterialList[_veriSayac].Mtart,
                                        mtbez = _Cevap.EtMaterialList[_veriSayac].Mtbez,
                                        werks = _Cevap.EtMaterialList[_veriSayac].Werks

                                    }.Save();


                                }
                                else
                                {
                                    _Temp.aktif = 1;
                                    _Temp.Save();

                                    if (_Temp.maktx.Equals(_gelenMatnr) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivzrfidorderlistreponse(session)
                                            {
                                                aktif = _Temp.aktif,
                                                aufnr = _Temp.maktx,
                                                bdmng = _Temp.mtart,
                                                createuser = _Temp.createuser
                                            }.Save();


                                            _LogAlindimi = true;
                                        }

                                        _Temp.maktx = _gelenMaktx;
                                        _Temp.lastupdateuser = "aniventi";
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
                                                aufnr = _Temp.maktx,
                                                bdmng = _Temp.mtart,
                                                createuser = _Temp.createuser
                                            }.Save();


                                            _LogAlindimi = true;
                                        }

                                        _Temp.meins = _gelenMeins;
                                        _Temp.lastupdateuser = "aniventi";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }
                                }

                            }
                        }


                        #region aktif 0 olanların son güncellenme zamanının Yaz

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemelistesiresponse> _Temp = session.Query<tblmalzemelistesiresponse>().Where(w => w.aktif == 0).ToList();

                            foreach (var item in _Temp)
                            {
                                item.lastupdateuser = "aniventi";
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
