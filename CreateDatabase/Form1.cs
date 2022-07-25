using CreateDatabase.ServiceReference1;
using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Entity.YedekMalzemeTakip.Md5;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace CreateDatabase
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }


        private void btnVeriTabani_Click(object sender, EventArgs e)
        {
            fnVeriTabaniOlustur();
        }


        #region Butonlar

        private void button1_Click(object sender, EventArgs e)
        {
            fn_KullaniciOlustur();

        }


        private void button2_Click(object sender, EventArgs e)
        {
            fn_MalzemeListesiCek();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            fn_MalzemeStokListesiCek();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            fn_MalzemeBelgeListesiCek();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            fn_SiparisBilesenleriDegistir();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            fn_OrderList();
        }




        #endregion

        #region Fonksiyonlar



        private void fnVeriTabaniOlustur()
        {
            try
            {
                XpoManager.Instance.InitXpo();

                //tblmalzemestoklistesiparam


                #region tblzrfidorderlistparam Kontrol

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblzrfidorderlistparam _Temp = session.Query<tblzrfidorderlistparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblzrfidorderlistparam(session)
                        {
                            aktif = 1,
                            aufnr = "",
                            createuser = "aniventi",
                            databasekayitzamani = DateTime.Now,
                            erdathigh = "",
                            erdatlow = "",
                            guncellemezamani = DateTime.Now,
                            lastupdateuser = "aniventi",
                            id = Guid.NewGuid().ToString().ToUpper()
                        }.Save();
                    }
                }
                #endregion

                #region tblmalzemelistesiparam Kontrol

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemelistesiparam _Temp = session.Query<tblmalzemelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblmalzemelistesiparam(session)
                        {
                            mtart = "",
                            werks = "",
                            aktif = 1,
                            createuser = "aniventi",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            lastupdateuser = "aniventi",
                            id = Guid.NewGuid().ToString().ToUpper()
                        }.Save();
                    }
                }
                #endregion

                #region tblmalzemestoklistesiparam Kontrol

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblmalzemestoklistesiparam _Temp = session.Query<tblmalzemestoklistesiparam>().FirstOrDefault(w => w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblmalzemestoklistesiparam(session)
                        {
                            mtart = "",
                            werks = "",
                            lgort = "",
                            aktif = 1,
                            createuser = "aniventi",
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            lastupdateuser = "aniventi",
                            id = Guid.NewGuid().ToString().ToUpper()
                        }.Save();
                    }
                }
                #endregion


                MessageBox.Show("İşlem tamam");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void fn_KullaniciOlustur()
        {
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    new tblkullanici(session)
                    {
                        aktif = 1,
                        databasekayitzamani = DateTime.Now,
                        kullaniciadi = "Admin",
                        sifre = EncryptionHelper.ToMD5("AdminX.123"),
                        id = Guid.NewGuid().ToString()
                    }.Save();


                    MessageBox.Show("İşlem Başarılı");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("İşlem Başarısız");
            }
        }

        private void fn_OrderList()
        {
            int i = 0;
            try
            {
                string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

                ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                _Client.ClientCredentials.UserName.UserName = UserName_d;
                _Client.ClientCredentials.UserName.Password = Password_d;
                _Client.Open();

                ZrfidStrOrderList _orderList = new ZrfidStrOrderList();

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



                ZrfidStrOrderList[] _DiziOrderList = new ZrfidStrOrderList[1];
                _DiziOrderList[0] = _orderList;



                ZrfidOrderList _Request = new ZrfidOrderList();
                _Request.EtOrderList = _DiziOrderList;



                Bapiret2 _tmp2 = new Bapiret2();
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

                _Request.EtReturn = _tmp2Dizi;






                ZrfidOrderListResponse _Cevap = _Client.ZrfidOrderList(_Request);

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    for (int _veriSayac = 0; _veriSayac <= 100; _veriSayac++)
                    {
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
                        string _gelenResponse = _Cevap.EtReturn[_veriSayac].Message;
                        string _gelenResponse2 = _Cevap.EtReturn[_veriSayac].MessageV1;



                        tblzrfidorderlistreponse _Temp = session.Query<tblzrfidorderlistreponse>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));


                        if (_Temp == null)
                        {

                            new tblzrfidorderlistreponse(session)
                            {


                                aktif = 1,
                                databasekayitzamani = DateTime.Now,
                                guncellemezamani = DateTime.Now,
                                id = Guid.NewGuid().ToString(),
                                aufnr = _Cevap.EtOrderList[_veriSayac].Aufnr,
                                posnr = _Cevap.EtOrderList[_veriSayac].Posnr,
                                matnr = _Cevap.EtOrderList[_veriSayac].Matnr,
                                maktx = _Cevap.EtOrderList[_veriSayac].Maktx,
                                bdmng = _Cevap.EtOrderList[_veriSayac].Bdmng.ToString(),
                                enmng = _Cevap.EtOrderList[_veriSayac].Enmng.ToString(),
                                kzear = _Cevap.EtOrderList[_veriSayac].Kzear,
                                meins = _Cevap.EtOrderList[_veriSayac].Meins,
                                werks = _Cevap.EtOrderList[_veriSayac].Aufnr,
                                lgort = _Cevap.EtOrderList[_veriSayac].Lgort,
                                vornr = _Cevap.EtOrderList[_veriSayac].Vornr,
                                erdat = _Cevap.EtOrderList[_veriSayac].Erdat,
                                rsnum = _Cevap.EtOrderList[_veriSayac].Rsnum,
                                rspos = _Cevap.EtOrderList[_veriSayac].Rspos

                            }.Save();




                        }
                        else
                        {
                            if (_Temp.maktx.Equals(_gelenMatnr) == false)
                            {
                                _Temp.maktx = _gelenMaktx;
                                _Temp.guncellemezamani = DateTime.Now;
                                _Temp.Save();
                            }

                            if (_Temp.meins.Equals(_gelenMeins) == false)
                            {
                                _Temp.meins = _gelenMeins;
                                _Temp.guncellemezamani = DateTime.Now;
                                _Temp.Save();
                            }
                        }

                    }
                    MessageBox.Show("Kayıt Bitti");
                }



                _Client.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }



        }


        private void fn_MalzemeListesiCek()
        {
            try
            {
                string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

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


                ZrfidStrMaterialList[] _DiziParamMetarialList = new ZrfidStrMaterialList[1];
                _DiziParamMetarialList[0] = _ParamMetarialList;

                ZrfidMaterialList _Request = new ZrfidMaterialList();
                _Request.EtMaterialList = _DiziParamMetarialList;

                ZrfidMaterialListResponse _Cevap = _Client.ZrfidMaterialList(_Request);

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    for (int _veriSayac = 0; _veriSayac <= 100; _veriSayac++)
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
                                guncellemezamani = DateTime.Now,
                                id = Guid.NewGuid().ToString(),
                                maktx = _Cevap.EtMaterialList[_veriSayac].Maktx,
                                matnr = _Cevap.EtMaterialList[_veriSayac].Matnr,
                                meins = _Cevap.EtMaterialList[_veriSayac].Meins,
                                mtart = _Cevap.EtMaterialList[_veriSayac].Mtart,
                                mtbez = _Cevap.EtMaterialList[_veriSayac].Mtbez,
                                werks = _Cevap.EtMaterialList[_veriSayac].Werks,



                            }.Save();


                        }
                        else
                        {
                            if (_Temp.maktx.Equals(_gelenMatnr) == false)
                            {
                                _Temp.maktx = _gelenMaktx;
                                _Temp.guncellemezamani = DateTime.Now;
                                _Temp.Save();
                            }

                            if (_Temp.meins.Equals(_gelenMeins) == false)
                            {
                                _Temp.meins = _gelenMeins;
                                _Temp.guncellemezamani = DateTime.Now;
                                _Temp.Save();
                            }
                        }

                    }
                    MessageBox.Show("Kayıt Bitti");
                }



                _Client.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }


        }
        //Bakılacak
        private void fn_MalzemeStokListesiCek()
        {
            try
            {
                string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();

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
                _Eleman_01.Mtart = "Z022";

                ZrfidStrMtart[] _Dizi_01 = new ZrfidStrMtart[1];
                _Dizi_01[0] = _Eleman_01;
                #endregion


                #region ItIwerk
                ZrfidStrIwerk _Eleman_02 = new ZrfidStrIwerk();
                _Eleman_02.Iwerk = "1210";

                ZrfidStrIwerk[] _Dizi_02 = new ZrfidStrIwerk[1];
                _Dizi_02[0] = _Eleman_02;


                #endregion

                #region ItLgort
                ZrfidStrLgort _Eleman_03 = new ZrfidStrLgort();
                _Eleman_03.Lgort = "1910";


                ZrfidStrLgort[] _Dizi_03 = new ZrfidStrLgort[1];
                _Dizi_03[0] = _Eleman_03;
                #endregion

                ZrfidMaterialStockList _RequestStoke = new ZrfidMaterialStockList();

                _RequestStoke.ItIwerk = _Dizi_02;
                _RequestStoke.ItLgort = _Dizi_03;
                _RequestStoke.ItMtart = _Dizi_01;

                _RequestStoke.EtMaterialstock = _DiziParamzrfidStrMaterialStock;



                #region Durul Deneme
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

                    List<tblmalzemeserialstoklistesi> _Liste2 = session.Query<tblmalzemeserialstoklistesi>().Where(w => w.aktif == 1).ToList();

                    foreach (var item in _Liste2)
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
                        #region GelenDegerler

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
                        #endregion


                        tblmalzemestoklistesiresponse _Temp = session.Query<tblmalzemestoklistesiresponse>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));

                        if (_Temp != null)
                        {
                            _Temp.aktif = 1;
                            _Temp.guncellemezamani = DateTime.Now;
                            _Temp.Save();
                        }

                        else
                        {
                            new tblmalzemestoklistesiresponse(session)
                            {
                                labst = _gelenLabst,
                                lgort = _gelenLgort,
                                lgpbe = _gelenLgpbe,
                                speme = _gelenSpeme,
                                aktif = 1,
                                createuser = "form",
                                databasekayitzamani = DateTime.Now,
                                guncellemezamani = DateTime.Now,
                                id = Guid.NewGuid().ToString().ToUpper(),
                                lastupdateuser = "form",
                                maktx = _gelenMaktx,
                                matnr = _gelenMatnr,
                                meins = _gelenMeins,
                                mtart = _gelenMtart,
                                mtbez = _gelenMtbez,
                                werks = _gelenWerks

                            }.Save();
                        }


                        //new tblmalzemestoklistesi(session)
                        //{
                        //    aktif = 1,
                        //    databasekayitzamani = DateTime.Now,
                        //    guncellemezamani = DateTime.Now,
                        //    id = Guid.NewGuid().ToString(),
                        //    matnr = _CevapStoke.EtSerialnumber[_veriSayac].Matnr,
                        //    maktx=_CevapStoke.EtSerialnumber[_veriSayac].Maktx,
                        //    blager=_CevapStoke.EtSerialnumber[_veriSayac].BLager,
                        //    bwerk=_CevapStoke.EtSerialnumber[_veriSayac].BWerk,
                        //    sernr=_CevapStoke.EtSerialnumber[_veriSayac].Sernr,
                        //    aciklama=_CevapStoke.EtSerialnumber[_veriSayac].Aciklama




                        //}.Save();

                    }



                    //for (int _veriSayac = 0; _veriSayac <= _CevapStoke.EtMaterialstock.Length; _veriSayac++)
                    //{
                    //    string _gelenMatnr = _CevapStoke.EtMaterialstock[_veriSayac].Matnr;
                    //    string _gelenLgort = _CevapStoke.EtMaterialstock[_veriSayac].Lgort;
                    //    string _gelenMaktx = _CevapStoke.EtMaterialstock[_veriSayac].Maktx;
                    //    string _gelenMeins = _CevapStoke.EtMaterialstock[_veriSayac].Meins;
                    //    string _gelenMtart = _CevapStoke.EtMaterialstock[_veriSayac].Mtart;
                    //    string _gelenMtbez = _CevapStoke.EtMaterialstock[_veriSayac].Mtbez;
                    //    string _gelenWerks = _CevapStoke.EtMaterialstock[_veriSayac].Werks;
                    //    decimal _gelenLabst = _CevapStoke.EtMaterialstock[_veriSayac].Labst;
                    //    decimal _gelenSpeme = _CevapStoke.EtMaterialstock[_veriSayac].Speme;
                    //    string _gelenLgpbe = _CevapStoke.EtMaterialstock[_veriSayac].Lgpbe;

                    //    tblmalzemestoklistesiresponse _Temp = session.Query<tblmalzemestoklistesiresponse>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));

                    //    //if (_Temp != null)
                    //    //{
                    //    //    if (_Temp.werks.Equals(_CevapStoke.EtMaterialstock[_veriSayac].Werks) == false)
                    //    //    {
                    //    //        _Temp.werks = _CevapStoke.EtMaterialstock[_veriSayac].Werks;
                    //    //        _Temp.guncellemezamani = DateTime.Now;
                    //    //        _Temp.lastupdateuser = "servis";
                    //    //        _Temp.Save();
                    //    //    }
                    //    //}



                    //    //   if (_Temp == null)
                    //    // {
                    //    new tblmalzemelistesiresponse(session)
                    //    {
                    //        aktif = 1,
                    //        createuser = "servis",
                    //        databasekayitzamani = DateTime.Now,
                    //        guncellemezamani = DateTime.Now,
                    //        id = Guid.NewGuid().ToString().ToUpper(),
                    //        lastupdateuser = "servis",
                    //        maktx = _CevapStoke.EtMaterialstock[_veriSayac].Maktx,
                    //        matnr = _CevapStoke.EtMaterialstock[_veriSayac].Matnr,
                    //        meins = _CevapStoke.EtMaterialstock[_veriSayac].Meins,
                    //        mtart = _CevapStoke.EtMaterialstock[_veriSayac].Mtart,
                    //        mtbez = _CevapStoke.EtMaterialstock[_veriSayac].Mtbez,
                    //        werks = _CevapStoke.EtMaterialstock[_veriSayac].Werks
                    //    }.Save();




                    //    //   }


                    //    //new tblmalzemestoklistesi(session)
                    //    //{
                    //    //    aktif = 1,
                    //    //    databasekayitzamani = DateTime.Now,
                    //    //    guncellemezamani = DateTime.Now,
                    //    //    id = Guid.NewGuid().ToString(),
                    //    //    matnr = _CevapStoke.EtSerialnumber[_veriSayac].Matnr,
                    //    //    maktx=_CevapStoke.EtSerialnumber[_veriSayac].Maktx,
                    //    //    blager=_CevapStoke.EtSerialnumber[_veriSayac].BLager,
                    //    //    bwerk=_CevapStoke.EtSerialnumber[_veriSayac].BWerk,
                    //    //    sernr=_CevapStoke.EtSerialnumber[_veriSayac].Sernr,
                    //    //    aciklama=_CevapStoke.EtSerialnumber[_veriSayac].Aciklama




                    //    //}.Save();



                    //    MessageBox.Show("İşlem Bitti");
                    //}


                   



                }

                #region Pasif kayıtların son güncellenme zamanını tut

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<tblmalzemestoklistesiresponse> _Liste = session.Query<tblmalzemestoklistesiresponse>().Where(w => w.aktif == 0).ToList();

                    foreach (var item in _Liste)
                    {
                        item.lastupdateuser = "servis";
                        item.guncellemezamani = DateTime.Now;
                        item.Save();
                    }

                }

                #endregion
                _Client.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }



        private void fn_MalzemeBelgeListesiCek()
        {
            try
            {

                string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();


                ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                _Client.ClientCredentials.UserName.UserName = UserName_d;
                _Client.ClientCredentials.UserName.Password = Password_d;
                _Client.Open();


                ZrfidStrMaterialDocument _Parametre = new ZrfidStrMaterialDocument();

                _Parametre.Aufnr = "";
                _Parametre.Budat = "";
                _Parametre.Bwart = "";
                _Parametre.Lgort = "";
                _Parametre.Maktx = "";
                _Parametre.Matnr = "";
                _Parametre.Mblnr = "";
                _Parametre.Mblnr2 = "";
                _Parametre.Meins = "";
                _Parametre.Menge = 0;
                _Parametre.Mjahr = "";
                _Parametre.Mjahr2 = "";
                _Parametre.Sernr = "";
                _Parametre.Vornr = "";
                _Parametre.Zeile = "";
                _Parametre.Zeile2 = "";

                ZrfidStrMaterialDocument[] _DiziParametre = new ZrfidStrMaterialDocument[1];
                _DiziParametre[0] = _Parametre;

                ZrfidMaterialDocument _RequestParametre = new ZrfidMaterialDocument();
                _RequestParametre.EtMaterialDocument = _DiziParametre;

                ZrfidMaterialDocumentResponse _Cevap = _Client.ZrfidMaterialDocument(_RequestParametre);


                using (Session session = XpoManager.Instance.GetNewSession())
                {


                    for (int _veriSayac = 0; _veriSayac <= 100; _veriSayac++)
                    {

                        string _gelenMatnr = _Cevap.EtMaterialDocument[_veriSayac].Matnr;

                        tblmalzemebelgelistesiresponse _Temp = session.Query<tblmalzemebelgelistesiresponse>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));

                        if (_Temp == null)
                        {
                            new tblmalzemebelgelistesiresponse(session)
                            {
                                aktif = 1,
                                databasekayitzamani = DateTime.Now,
                                guncellemezamani = DateTime.Now,
                                id = Guid.NewGuid().ToString(),
                                aufnr = _Cevap.EtMaterialDocument[_veriSayac].Aufnr,
                                budat = _Cevap.EtMaterialDocument[_veriSayac].Budat,
                                bwart = _Cevap.EtMaterialDocument[_veriSayac].Bwart,
                                lgort = _Cevap.EtMaterialDocument[_veriSayac].Lgort,
                                maktx = _Cevap.EtMaterialDocument[_veriSayac].Maktx,
                                matnr = _Cevap.EtMaterialDocument[_veriSayac].Matnr,
                                mblnr = _Cevap.EtMaterialDocument[_veriSayac].Mblnr,
                                mblnr2 = _Cevap.EtMaterialDocument[_veriSayac].Mblnr2,
                                meins = _Cevap.EtMaterialDocument[_veriSayac].Meins,
                                mjahr2 = _Cevap.EtMaterialDocument[_veriSayac].Mjahr2.ToString(),
                                sernr = _Cevap.EtMaterialDocument[_veriSayac].Sernr,
                                vornr = _Cevap.EtMaterialDocument[_veriSayac].Vornr,
                                zeile = _Cevap.EtMaterialDocument[_veriSayac].Zeile,
                                zeile2 = _Cevap.EtMaterialDocument[_veriSayac].Zeile2,
                                mjahr = _Cevap.EtMaterialDocument[_veriSayac].Mjahr,
                                menge = _Cevap.EtMaterialDocument[_veriSayac].Menge.ToString(),

                            }.Save();


                        }
                        else
                        {



                        }


                    }


                }


                _Client.Close();

                MessageBox.Show("Malzeme Begeleri Listelendi");


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void fn_SiparisBilesenleriDegistir()
        {
            try
            {
                string UserName_d = ConfigurationManager.AppSettings["ServisUserName"].ToString().Trim();
                string Password_d = ConfigurationManager.AppSettings["ServisPassWord"].ToString().Trim();


                ZRFIDClient _Client = new ZRFIDClient("ZRFID_SOAP12");
                _Client.ClientCredentials.UserName.UserName = UserName_d;
                _Client.ClientCredentials.UserName.Password = Password_d;
                _Client.Open();

                ZrfidStrComponentUpdate _ParamzrfidStrComponentUpdate = new ZrfidStrComponentUpdate();

                _ParamzrfidStrComponentUpdate.Ablad = "";
                _ParamzrfidStrComponentUpdate.Bdmng = 0;
                _ParamzrfidStrComponentUpdate.Indc = "";
                _ParamzrfidStrComponentUpdate.Lgort = "";
                _ParamzrfidStrComponentUpdate.Matnr = "";
                _ParamzrfidStrComponentUpdate.Meins = "";
                _ParamzrfidStrComponentUpdate.Posnr = "";
                _ParamzrfidStrComponentUpdate.Postp = "";
                _ParamzrfidStrComponentUpdate.Rgekz = "";
                _ParamzrfidStrComponentUpdate.Rsnum = "";
                _ParamzrfidStrComponentUpdate.Rspos = "";
                _ParamzrfidStrComponentUpdate.Vornr = "";
                _ParamzrfidStrComponentUpdate.Wempf = "";
                _ParamzrfidStrComponentUpdate.Werks = "";


                ZrfidStrComponentUpdate[] _DizizrfidStrComponentUpdates = new ZrfidStrComponentUpdate[1];
                _DizizrfidStrComponentUpdates[0] = _ParamzrfidStrComponentUpdate;

                ZrfidComponentUpdate _RequestzrfidComponentUpdate = new ZrfidComponentUpdate();
                _RequestzrfidComponentUpdate.ItComponent = _DizizrfidStrComponentUpdates;

                ZrfidComponentUpdateResponse _CevapzrfidComponentUpdateResponse = _Client.ZrfidComponentUpdate(_RequestzrfidComponentUpdate);



                using (Session session = XpoManager.Instance.GetNewSession())
                {

                    for (int _veriSayac = 0; _veriSayac <= 5; _veriSayac++)
                    {

                        string _gelenMatnr = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Matnr;

                        tblsiparisbilesenleridegistirme _Temp = session.Query<tblsiparisbilesenleridegistirme>().FirstOrDefault(w => w.matnr.Equals(_gelenMatnr));
                        if (_Temp == null)
                        {
                            new tblsiparisbilesenleridegistirme(session)
                            {
                                aktif = 1,
                                databasekayitzamani = DateTime.Now,
                                guncellemezamani = DateTime.Now,
                                id = Guid.NewGuid().ToString(),
                                ablad = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Ablad,
                                bdmng = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Bdmng.ToString(),
                                indc = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Indc,
                                lgort = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Indc,
                                matnr = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Matnr,
                                meins = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Meins,
                                posnr = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Meins,
                                postp = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Postp,
                                rgekz = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Rgekz,
                                rsnum = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Rsnum,
                                rspos = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Rspos,
                                vornr = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Vornr,
                                wempf = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Wempf,
                                werks = _CevapzrfidComponentUpdateResponse.ItComponent[_veriSayac].Werks


                            }.Save();
                        }
                        MessageBox.Show("Kayıt Bitti");


                    }
                }

                _Client.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }



        #endregion

        private void button7_Click(object sender, EventArgs e)
        {
            fn_DBOlustur();

            fn_AdminKullaniciOlustur();

            fn_ArayuzAdminKullaniciOlustur();
        }

        private void fn_ArayuzAdminKullaniciOlustur()
        {
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblarayuzkullanici _Temp = session.Query<tblarayuzkullanici>().FirstOrDefault(w => w.kullaniciadi.Equals("Admin") && w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblarayuzkullanici(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,
                            kullaniciadi = "Admin",
                            sifre = EncryptionHelper.ToMD5("Bursa123."),
                            id = Guid.NewGuid().ToString()
                        }.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void fn_DBOlustur()
        {
            try
            {
                XpoManager.Instance.InitXpo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show("Bitti");
        }

        private void fn_AdminKullaniciOlustur()
        {
            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblkullanici _Temp = session.Query<tblkullanici>().FirstOrDefault(w => w.kullaniciadi.Equals("Admin") && w.aktif == 1);

                    if (_Temp == null)
                    {
                        new tblkullanici(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,
                            kullaniciadi = "Admin",
                            sifre = EncryptionHelper.ToMD5("AdminX.123"),
                            id = Guid.NewGuid().ToString()
                        }.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            MessageBox.Show("Bitti");
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }
    }
}

