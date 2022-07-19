
using AbdiIbrahim.Servis.YedekMalzeme.MalzemeBelgeListesi.ServiceReference2;
using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AbdiIbrahim.Servis.YedekMalzeme.MalzemeBelgeListesi
{
    public class cMalzemeBelgeListesiIslem
    {

        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cMalzemeBelgeListesiIslem));

        internal void fn_MalzemeBelgeListesiCek(object state)
        {
            #region Değişkenler
            //Sürekli veri çekme ile uğraşmasın diye
            int _BeklemeSuresi = 45;
            //Şifre için
            string UserName_d = "";
            string Password_d = "";

            //İnputlar girilsin diye
            string _ParamBudatLow = "";
            string _ParamBudatHigh = "";
            string _ParamIwerk = "";
            string _ParamAufnr = "";
            
            //guncellendiğindeki kayıt için
            bool _LogAlindimi = false;

            #endregion

            try
            {
                //Servis açık olduğu sürece çalışsın diye
                while (true)
                {
                    _LogAlindimi = false;

                    #region Çalışmadığının loglaması
                    try
                    {
                        ConfigurationManager.RefreshSection("appSettings");

                        _BeklemeSuresi = int.Parse(ConfigurationManager.AppSettings["MalzemeBelgeListesiBeklemeSuresi"].ToString().Trim());
                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Error("Bekleme Süresi Değeri Çekilemedi =" + ex.ToString());
                    }

                    #endregion

                    #region Parametre Çekme

                    try
                    {
                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            tblmalzemebelgelistesiparam _Temp = session.Query<tblmalzemebelgelistesiparam>().FirstOrDefault(w => w.aktif == 1);

                            if (_Temp == null)
                            {
                                _LogDosyasi.Error(" Aktif Parametre Değerleri Bulunamadı= ");

                                //Aktif parametre değeri yoksa diye bakılacak
                                continue;

                            }
                            else
                            {

                                _ParamAufnr = _Temp.aufnr;
                                _ParamBudatHigh = _Temp.budathigh;
                                _ParamBudatLow = _Temp.budatlow;
                                _ParamIwerk = _Temp.iwerk;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(TimeSpan.FromMinutes(_BeklemeSuresi));

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


                        ZrfidStrMaterialDocument _Parametre = new ZrfidStrMaterialDocument();

                        #region İlk Değerler
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
                        #endregion


                        ZrfidStrMaterialDocument[] _DiziParametre = new ZrfidStrMaterialDocument[1];
                        _DiziParametre[0] = _Parametre;

                        ZrfidMaterialDocument _RequestParametre = new ZrfidMaterialDocument();
                        _RequestParametre.EtMaterialDocument = _DiziParametre;
                        #endregion

                        ZrfidMaterialDocumentResponse _Cevap = _Client.ZrfidMaterialDocument(_RequestParametre);

                        #region Önce Tüm Malzeme Belge Listesi Pasif Yap   ?                   

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {
                            List<tblmalzemebelgelistesiresponse> _Temp = session.Query<tblmalzemebelgelistesiresponse>().Where(w => w.aktif == 1).ToList();

                            foreach (var item in _Temp)
                            {
                                item.aktif = 0;
                                item.Save();
                            }
                        }
                        #endregion

                        #region Belge Listesini Çek

                        using (Session session = XpoManager.Instance.GetNewSession())
                        {

                            #region MyRegion
                            //for (int _veriSayac = 0; _veriSayac < _Cevap.EtMaterialDocument.Length; _veriSayac++)
                            //{
                            //    #region gelenDeğerler

                            //    string _gelenaufnr = _Cevap.EtMaterialDocument[_veriSayac].Aufnr;
                            //    string _gelenbudat = _Cevap.EtMaterialDocument[_veriSayac].Budat;
                            //    string _gelenbwart = _Cevap.EtMaterialDocument[_veriSayac].Bwart;
                            //    string _gelenlgort = _Cevap.EtMaterialDocument[_veriSayac].Lgort;
                            //    string _gelenmaktx = _Cevap.EtMaterialDocument[_veriSayac].Maktx;
                            //    string _gelenmatnr = _Cevap.EtMaterialDocument[_veriSayac].Matnr;
                            //    string _gelenmblnr = _Cevap.EtMaterialDocument[_veriSayac].Mblnr;
                            //    string _gelenmblnr2 = _Cevap.EtMaterialDocument[_veriSayac].Mblnr2;
                            //    string _gelenmeins = _Cevap.EtMaterialDocument[_veriSayac].Meins;
                            //    string _gelenmjahr2 = _Cevap.EtMaterialDocument[_veriSayac].Mjahr2;
                            //    string _gelensernr = _Cevap.EtMaterialDocument[_veriSayac].Sernr;
                            //    string _gelenvornr = _Cevap.EtMaterialDocument[_veriSayac].Vornr;
                            //    string _gelenzeile = _Cevap.EtMaterialDocument[_veriSayac].Zeile;
                            //    string _gelenzeile2 = _Cevap.EtMaterialDocument[_veriSayac].Zeile2;
                            //    string _gelenmjahr = _Cevap.EtMaterialDocument[_veriSayac].Mjahr;
                            //    string _gelenmenge = _Cevap.EtMaterialDocument[_veriSayac].Menge.ToString();
                            //    #endregion


                            //    tblmalzemebelgelistesiresponse _Temp = session.Query<tblmalzemebelgelistesiresponse>().FirstOrDefault(w => w.matnr.Equals(_gelenmatnr) && w.aktif == 1);

                            //    if (_Temp == null || _gelenmatnr != null)
                            //    {
                            //        new tblmalzemebelgelistesiresponse(session)
                            //        {
                            //            aktif = 1,
                            //            databasekayitzamani = DateTime.Now,
                            //            guncellemezamani = DateTime.Now,
                            //            id = Guid.NewGuid().ToString().ToUpper(),
                            //            createuser = "service",
                            //            lastupdateuser = "service",
                            //            aufnr = _gelenaufnr,
                            //            budat = _gelenbudat,
                            //            bwart = _gelenbwart,
                            //            lgort = _gelenlgort,
                            //            maktx = _gelenmaktx,
                            //            matnr = _gelenmatnr,
                            //            mblnr = _gelenmblnr,
                            //            mblnr2 = _gelenmblnr2,
                            //            meins = _gelenmeins,
                            //            mjahr2 = _gelenmjahr2,
                            //            sernr = _gelensernr,
                            //            vornr = _gelenvornr,
                            //            zeile = _gelenzeile,
                            //            zeile2 = _gelenzeile2,
                            //            mjahr = _gelenmjahr,
                            //            menge = _gelenmenge,

                            //        }.Save();

                            //        tbl06analiz _guncelanaliz = session.Query<tbl06analiz>().FirstOrDefault(g => g.aktif == 1 && g.matnr.Equals(_gelenmatnr) && g.maktx.Equals(_gelenmaktx) && g.sernr.Equals(_gelensernr));

                            //        if (_guncelanaliz != null)
                            //        {

                            //            _guncelanaliz.tuketim = 1;
                            //            _guncelanaliz.lastupdateuser = "service";
                            //            _guncelanaliz.guncellemezamani = DateTime.Now;
                            //            _guncelanaliz.Save();
                            //        }


                            //    }
                            //    else
                            //    {

                            //        if (_Temp.aufnr.Equals(_gelenaufnr) == false)
                            //        {
                            //            if (_LogAlindimi == false)
                            //            {
                            //                new tblarsivmalzemebelgelistesiresponse(session)
                            //                {
                            //                    aktif = _Temp.aktif,
                            //                    databasekayitzamani = _Temp.databasekayitzamani,
                            //                    createuser = "service",
                            //                    guncellemezamani = DateTime.Now,
                            //                    degisenalan = "aufnr",
                            //                    aufnr = _Temp.aufnr,
                            //                    id = Guid.NewGuid().ToString().ToUpper(),
                            //                    lastupdateuser = "service",
                            //                    lgort = _Temp.lgort,
                            //                    maktx = _Temp.maktx,
                            //                    matnr = _Temp.matnr,
                            //                    meins = _Temp.meins,
                            //                    vornr = _Temp.vornr,
                            //                    budat = _Temp.budat,
                            //                    bwart = _Temp.bwart,
                            //                    mblnr = _Temp.mblnr,
                            //                    mblnr2 = _Temp.mblnr2,
                            //                    menge = _Temp.menge,
                            //                    mjahr = _Temp.mjahr,
                            //                    mjahr2 = _Temp.mjahr2,
                            //                    sernr = _Temp.sernr,
                            //                    zeile = _Temp.zeile,
                            //                    zeile2 = _Temp.zeile2

                            //                }.Save();

                            //                _LogAlindimi = true;
                            //            }


                            //            _Temp.aufnr = _gelenaufnr;
                            //            _Temp.lastupdateuser = "service";
                            //            _Temp.guncellemezamani = DateTime.Now;
                            //            _Temp.Save();
                            //        }

                            //        if (_Temp.matnr.Equals(_gelenmatnr) == false)
                            //        {
                            //            if (_LogAlindimi == false)
                            //            {
                            //                new tblarsivmalzemebelgelistesiresponse(session)
                            //                {
                            //                    aktif = _Temp.aktif,
                            //                    databasekayitzamani = _Temp.databasekayitzamani,
                            //                    createuser = "service",
                            //                    guncellemezamani = DateTime.Now,
                            //                    degisenalan = "matnr",
                            //                    aufnr = _Temp.aufnr,
                            //                    id = Guid.NewGuid().ToString().ToUpper(),
                            //                    lastupdateuser = "service",
                            //                    lgort = _Temp.lgort,
                            //                    maktx = _Temp.maktx,
                            //                    matnr = _Temp.matnr,
                            //                    meins = _Temp.meins,
                            //                    vornr = _Temp.vornr,
                            //                    budat = _Temp.budat,
                            //                    bwart = _Temp.bwart,
                            //                    mblnr = _Temp.mblnr,
                            //                    mblnr2 = _Temp.mblnr2,
                            //                    menge = _Temp.menge,
                            //                    mjahr = _Temp.mjahr,
                            //                    mjahr2 = _Temp.mjahr2,
                            //                    sernr = _Temp.sernr,
                            //                    zeile = _Temp.zeile,
                            //                    zeile2 = _Temp.zeile2

                            //                }.Save();

                            //                _LogAlindimi = true;
                            //            }
                            //            _Temp.matnr = _gelenmatnr;
                            //            _Temp.lastupdateuser = "service";
                            //            _Temp.guncellemezamani = DateTime.Now;
                            //            _Temp.Save();
                            //        }

                            //        if (_Temp.sernr.Equals(_gelensernr) == false)
                            //        {
                            //            if (_LogAlindimi == false)
                            //            {
                            //                new tblarsivmalzemebelgelistesiresponse(session)
                            //                {
                            //                    aktif = _Temp.aktif,
                            //                    databasekayitzamani = _Temp.databasekayitzamani,
                            //                    createuser = "service",
                            //                    guncellemezamani = DateTime.Now,
                            //                    degisenalan = "sernr",
                            //                    aufnr = _Temp.aufnr,
                            //                    id = Guid.NewGuid().ToString().ToUpper(),
                            //                    lastupdateuser = "service",
                            //                    lgort = _Temp.lgort,
                            //                    maktx = _Temp.maktx,
                            //                    matnr = _Temp.matnr,
                            //                    meins = _Temp.meins,
                            //                    vornr = _Temp.vornr,
                            //                    budat = _Temp.budat,
                            //                    bwart = _Temp.bwart,
                            //                    mblnr = _Temp.mblnr,
                            //                    mblnr2 = _Temp.mblnr2,
                            //                    menge = _Temp.menge,
                            //                    mjahr = _Temp.mjahr,
                            //                    mjahr2 = _Temp.mjahr2,
                            //                    sernr = _Temp.sernr,
                            //                    zeile = _Temp.zeile,
                            //                    zeile2 = _Temp.zeile2

                            //                }.Save();

                            //                _LogAlindimi = true;
                            //            }

                            //            _Temp.sernr = _gelensernr;
                            //            _Temp.lastupdateuser = "service";
                            //            _Temp.guncellemezamani = DateTime.Now;
                            //            _Temp.Save();
                            //        }

                            //        if (_Temp.meins.Equals(_gelenmeins) == false)
                            //        {
                            //            if (_LogAlindimi == false)
                            //            {
                            //                new tblarsivmalzemebelgelistesiresponse(session)
                            //                {
                            //                    aktif = _Temp.aktif,
                            //                    databasekayitzamani = _Temp.databasekayitzamani,
                            //                    createuser = "service",
                            //                    guncellemezamani = DateTime.Now,
                            //                    degisenalan = "meins",
                            //                    aufnr = _Temp.aufnr,
                            //                    id = Guid.NewGuid().ToString().ToUpper(),
                            //                    lastupdateuser = "service",
                            //                    lgort = _Temp.lgort,
                            //                    maktx = _Temp.maktx,
                            //                    matnr = _Temp.matnr,
                            //                    meins = _Temp.meins,
                            //                    vornr = _Temp.vornr,
                            //                    budat = _Temp.budat,
                            //                    bwart = _Temp.bwart,
                            //                    mblnr = _Temp.mblnr,
                            //                    mblnr2 = _Temp.mblnr2,
                            //                    menge = _Temp.menge,
                            //                    mjahr = _Temp.mjahr,
                            //                    mjahr2 = _Temp.mjahr2,
                            //                    sernr = _Temp.sernr,
                            //                    zeile = _Temp.zeile,
                            //                    zeile2 = _Temp.zeile2

                            //                }.Save();

                            //                _LogAlindimi = true;
                            //            }
                            //            _Temp.meins = _gelenmeins;
                            //            _Temp.lastupdateuser = "service";
                            //            _Temp.guncellemezamani = DateTime.Now;
                            //            _Temp.Save();
                            //        }

                            //        if (_Temp.menge.Equals(_gelenmenge) == false)
                            //        {
                            //            if (_LogAlindimi == false)
                            //            {
                            //                new tblarsivmalzemebelgelistesiresponse(session)
                            //                {
                            //                    aktif = _Temp.aktif,
                            //                    databasekayitzamani = _Temp.databasekayitzamani,
                            //                    createuser = "service",
                            //                    guncellemezamani = DateTime.Now,
                            //                    degisenalan = "menge",
                            //                    aufnr = _Temp.aufnr,
                            //                    id = Guid.NewGuid().ToString().ToUpper(),
                            //                    lastupdateuser = "service",
                            //                    lgort = _Temp.lgort,
                            //                    maktx = _Temp.maktx,
                            //                    matnr = _Temp.matnr,
                            //                    meins = _Temp.meins,
                            //                    vornr = _Temp.vornr,
                            //                    budat = _Temp.budat,
                            //                    bwart = _Temp.bwart,
                            //                    mblnr = _Temp.mblnr,
                            //                    mblnr2 = _Temp.mblnr2,
                            //                    menge = _Temp.menge,
                            //                    mjahr = _Temp.mjahr,
                            //                    mjahr2 = _Temp.mjahr2,
                            //                    sernr = _Temp.sernr,
                            //                    zeile = _Temp.zeile,
                            //                    zeile2 = _Temp.zeile2

                            //                }.Save();

                            //                _LogAlindimi = true;
                            //            }
                            //            _Temp.menge = _gelenmenge;
                            //            _Temp.lastupdateuser = "service";
                            //            _Temp.guncellemezamani = DateTime.Now;
                            //            _Temp.Save();
                            //        }

                            //        if (_Temp.vornr.Equals(_gelenvornr) == false)
                            //        {
                            //            if (_LogAlindimi == false)
                            //            {
                            //                new tblarsivmalzemebelgelistesiresponse(session)
                            //                {
                            //                    aktif = _Temp.aktif,
                            //                    databasekayitzamani = _Temp.databasekayitzamani,
                            //                    createuser = "service",
                            //                    guncellemezamani = DateTime.Now,
                            //                    degisenalan = "vornr",
                            //                    aufnr = _Temp.aufnr,
                            //                    id = Guid.NewGuid().ToString().ToUpper(),
                            //                    lastupdateuser = "service",
                            //                    lgort = _Temp.lgort,
                            //                    maktx = _Temp.maktx,
                            //                    matnr = _Temp.matnr,
                            //                    meins = _Temp.meins,
                            //                    vornr = _Temp.vornr,
                            //                    budat = _Temp.budat,
                            //                    bwart = _Temp.bwart,
                            //                    mblnr = _Temp.mblnr,
                            //                    mblnr2 = _Temp.mblnr2,
                            //                    menge = _Temp.menge,
                            //                    mjahr = _Temp.mjahr,
                            //                    mjahr2 = _Temp.mjahr2,
                            //                    sernr = _Temp.sernr,
                            //                    zeile = _Temp.zeile,
                            //                    zeile2 = _Temp.zeile2

                            //                }.Save();

                            //                _LogAlindimi = true;
                            //            }
                            //            _Temp.vornr = _gelenvornr;
                            //            _Temp.lastupdateuser = "service";
                            //            _Temp.guncellemezamani = DateTime.Now;
                            //            _Temp.Save();
                            //        }

                            //        //Bakılacak
                            //        _Temp.aktif = 1;
                            //        _Temp.Save();

                            //    }


                            //}

                            #endregion



                            for (int _veriSayac = 0; _veriSayac < _Cevap.EtMaterialDocument.Length; _veriSayac++)
                            {
                                #region gelenDeğerler

                                string _gelenaufnr = _Cevap.EtMaterialDocument[_veriSayac].Aufnr;
                                string _gelenbudat = _Cevap.EtMaterialDocument[_veriSayac].Budat;
                                string _gelenbwart = _Cevap.EtMaterialDocument[_veriSayac].Bwart;
                                string _gelenlgort = _Cevap.EtMaterialDocument[_veriSayac].Lgort;
                                string _gelenmaktx = _Cevap.EtMaterialDocument[_veriSayac].Maktx;
                                string _gelenmatnr = _Cevap.EtMaterialDocument[_veriSayac].Matnr;
                                string _gelenmblnr = _Cevap.EtMaterialDocument[_veriSayac].Mblnr;
                                string _gelenmblnr2 = _Cevap.EtMaterialDocument[_veriSayac].Mblnr2;
                                string _gelenmeins = _Cevap.EtMaterialDocument[_veriSayac].Meins;
                                string _gelenmjahr2 = _Cevap.EtMaterialDocument[_veriSayac].Mjahr2;
                                string _gelensernr = _Cevap.EtMaterialDocument[_veriSayac].Sernr;
                                string _gelenvornr = _Cevap.EtMaterialDocument[_veriSayac].Vornr;
                                string _gelenzeile = _Cevap.EtMaterialDocument[_veriSayac].Zeile;
                                string _gelenzeile2 = _Cevap.EtMaterialDocument[_veriSayac].Zeile2;
                                string _gelenmjahr = _Cevap.EtMaterialDocument[_veriSayac].Mjahr;
                                string _gelenmenge = _Cevap.EtMaterialDocument[_veriSayac].Menge.ToString();
                                #endregion


                                tblmalzemebelgelistesiresponse _Temp = session.Query<tblmalzemebelgelistesiresponse>().FirstOrDefault(w =>  w.aktif == 1 && w.matnr.Equals(_gelenmatnr) && w.maktx.Equals(_gelenmaktx) && w.sernr.Equals(_gelensernr) && w.aufnr.Equals(_gelenaufnr));


                                if (_Temp == null || _gelenmatnr != null)
                                {
                                    new tblmalzemebelgelistesiresponse(session)
                                    {
                                        aktif = 1,
                                        databasekayitzamani = DateTime.Now,
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        createuser = "service",
                                        lastupdateuser = "service",
                                        aufnr = _gelenaufnr,
                                        budat = _gelenbudat,
                                        bwart = _gelenbwart,
                                        lgort = _gelenlgort,
                                        maktx = _gelenmaktx,
                                        matnr = _gelenmatnr,
                                        mblnr = _gelenmblnr,
                                        mblnr2 = _gelenmblnr2,
                                        meins = _gelenmeins,
                                        mjahr2 = _gelenmjahr2,
                                        sernr = _gelensernr,
                                        vornr = _gelenvornr,
                                        zeile = _gelenzeile,
                                        zeile2 = _gelenzeile2,
                                        mjahr = _gelenmjahr,
                                        menge = _gelenmenge,

                                    }.Save();

                                    tbl06analiz _guncelanaliz = session.Query<tbl06analiz>().FirstOrDefault(g => g.aktif == 1 && g.aufnr.Equals(_gelenaufnr) &&g.matnr.Equals(_gelenmatnr) && g.maktx.Equals(_gelenmaktx) && g.sernr.Equals(_gelensernr));

                                    if (_guncelanaliz != null)
                                    {

                                        _guncelanaliz.tuketim = 1;
                                        _guncelanaliz.lastupdateuser = "service";
                                        _guncelanaliz.guncellemezamani = DateTime.Now;
                                        _guncelanaliz.Save();

                                        tblkimliklendirme _kimlikiptal = session.Query<tblkimliklendirme>().FirstOrDefault(k => k.aktif == 1 && k.matnr.Equals(_gelenmatnr) && k.maktx.Equals(_gelenmaktx) && k.sernr.Equals(_gelensernr));

                                        if (_kimlikiptal !=null)
                                        {
                                            _kimlikiptal.aktif = 0;
                                            _kimlikiptal.guncellemezamani = DateTime.Now;
                                            _kimlikiptal.lastupdateuser = "service";
                                            _kimlikiptal.Save();
                                        }

                                        tbl01eklecikararsiv _ArsivIptal = session.Query<tbl01eklecikararsiv>().FirstOrDefault(w => w.aktif == 1 &&w.aufnr.Equals(_gelenaufnr) && w.matnr.Equals(_gelenmatnr) &&w.maktx.Equals(_gelenmaktx) && w.sernr.Equals(_gelensernr));

                                        if (_ArsivIptal !=null)
                                        {
                                            _ArsivIptal.aktif = 0;
                                            _ArsivIptal.guncellemezamani = DateTime.Now;
                                            _ArsivIptal.lastupdateuser = "service";
                                            _ArsivIptal.Save();
                                        }
                                    }


                                }
                                else
                                {
                                    

                                    if (_Temp.meins.Equals(_gelenmeins) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemebelgelistesiresponse(session)
                                            {
                                                aktif = _Temp.aktif,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                createuser = "service",
                                                guncellemezamani = DateTime.Now,
                                                degisenalan = "meins",
                                                aufnr = _Temp.aufnr,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = "service",
                                                lgort = _Temp.lgort,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                vornr = _Temp.vornr,
                                                budat = _Temp.budat,
                                                bwart = _Temp.bwart,
                                                mblnr = _Temp.mblnr,
                                                mblnr2 = _Temp.mblnr2,
                                                menge = _Temp.menge,
                                                mjahr = _Temp.mjahr,
                                                mjahr2 = _Temp.mjahr2,
                                                sernr = _Temp.sernr,
                                                zeile = _Temp.zeile,
                                                zeile2 = _Temp.zeile2

                                            }.Save();

                                            _LogAlindimi = true;
                                        }
                                        _Temp.meins = _gelenmeins;
                                        _Temp.lastupdateuser = "service";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }

                                    if (_Temp.menge.Equals(_gelenmenge) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemebelgelistesiresponse(session)
                                            {
                                                aktif = _Temp.aktif,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                createuser = "service",
                                                guncellemezamani = DateTime.Now,
                                                degisenalan = "menge",
                                                aufnr = _Temp.aufnr,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = "service",
                                                lgort = _Temp.lgort,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                vornr = _Temp.vornr,
                                                budat = _Temp.budat,
                                                bwart = _Temp.bwart,
                                                mblnr = _Temp.mblnr,
                                                mblnr2 = _Temp.mblnr2,
                                                menge = _Temp.menge,
                                                mjahr = _Temp.mjahr,
                                                mjahr2 = _Temp.mjahr2,
                                                sernr = _Temp.sernr,
                                                zeile = _Temp.zeile,
                                                zeile2 = _Temp.zeile2

                                            }.Save();

                                            _LogAlindimi = true;
                                        }
                                        _Temp.menge = _gelenmenge;
                                        _Temp.lastupdateuser = "service";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }

                                    if (_Temp.vornr.Equals(_gelenvornr) == false)
                                    {
                                        if (_LogAlindimi == false)
                                        {
                                            new tblarsivmalzemebelgelistesiresponse(session)
                                            {
                                                aktif = _Temp.aktif,
                                                databasekayitzamani = _Temp.databasekayitzamani,
                                                createuser = "service",
                                                guncellemezamani = DateTime.Now,
                                                degisenalan = "vornr",
                                                aufnr = _Temp.aufnr,
                                                id = Guid.NewGuid().ToString().ToUpper(),
                                                lastupdateuser = "service",
                                                lgort = _Temp.lgort,
                                                maktx = _Temp.maktx,
                                                matnr = _Temp.matnr,
                                                meins = _Temp.meins,
                                                vornr = _Temp.vornr,
                                                budat = _Temp.budat,
                                                bwart = _Temp.bwart,
                                                mblnr = _Temp.mblnr,
                                                mblnr2 = _Temp.mblnr2,
                                                menge = _Temp.menge,
                                                mjahr = _Temp.mjahr,
                                                mjahr2 = _Temp.mjahr2,
                                                sernr = _Temp.sernr,
                                                zeile = _Temp.zeile,
                                                zeile2 = _Temp.zeile2

                                            }.Save();

                                            _LogAlindimi = true;
                                        }
                                        _Temp.vornr = _gelenvornr;
                                        _Temp.lastupdateuser = "service";
                                        _Temp.guncellemezamani = DateTime.Now;
                                        _Temp.Save();
                                    }

                                    //Bakılacak
                                    _Temp.aktif = 1;
                                    _Temp.Save();

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
                                    new tblmalzemebelgelistesireturn(session)
                                    {
                                        aktif = 1,
                                        createuser = "service",
                                        databasekayitzamani = DateTime.Now,
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        lastupdateuser = "service",
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
                            List<tblmalzemebelgelistesiresponse> _Liste = session.Query<tblmalzemebelgelistesiresponse>().Where(w => w.aktif == 0).ToList();

                            foreach (var item in _Liste)
                            {
                                item.lastupdateuser = "service";
                                item.guncellemezamani = DateTime.Now;
                                item.Save();
                            }
                        }
                        #endregion



                        _Client.Close();

                        _LogDosyasi.Error("Malzeme Begeleri Listelendi");

                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Error(ex.ToString());
                    }

                    //Multithread çalışıp beklesin diye
                    Thread.Sleep(TimeSpan.FromMinutes(_BeklemeSuresi));


                }
            }
            catch (Exception ex)
            {
                Thread.Sleep(TimeSpan.FromMinutes(_BeklemeSuresi));

                _LogDosyasi.Error(ex.ToString());
            }
        }
    }
}
