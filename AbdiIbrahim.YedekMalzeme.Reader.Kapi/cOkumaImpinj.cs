using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Impinj.OctaneSdk;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace AbdiIbrahim.YedekMalzeme.Reader.Kapi
{
    public class cOkumaImpinj
    {
        public static ICollection<ReadView> _OkumaListesi = new List<ReadView>();

        static ImpinjReader reader = new ImpinjReader();

        static ILog _LogDosyasi = LogManager.GetLogger(typeof(cOkumaImpinj));


        internal class TagDataView
        {
            public string _Epc { get; set; }
            public DateTime _SonOkumaZamani { get; set; }
        }


        private TagDataView GetTagDataView(IEnumerable<ReadView> data)
        {
            return new TagDataView()
            {
                _SonOkumaZamani = data.Max(m => m._Zaman)
            };
        }

        private Dictionary<string, TagDataView> GetCollectTagsData()
        {
            Dictionary<string, TagDataView> collectData = new Dictionary<string, TagDataView>();

            try
            {
                collectData = _OkumaListesi.ToList().GroupBy(g => g._Epc).ToDictionary(d => d.Key,
                    d => GetTagDataView(d.Select(s => s)));
            }
            catch (Exception ex)
            {
                Thread.Sleep(100);

                return GetCollectTagsData();
            }

            return collectData;
        }

        private List<KeyValuePair<string, TagDataView>> GetCollectTagData()
        {
            List<KeyValuePair<string, TagDataView>> collectTagData = new List<KeyValuePair<string, TagDataView>>();

            try
            {
                Dictionary<string, TagDataView> collectTagsData = GetCollectTagsData();

                var itemTemp = collectTagsData.ToList();

                var items = collectTagsData.ToList();

                foreach (var item in items)
                {
                    collectTagData.Add(new KeyValuePair<string, TagDataView>(item.Key, new TagDataView()
                    {
                        _SonOkumaZamani = item.Value._SonOkumaZamani,
                        _Epc = item.Value._Epc
                    }));
                }
            }
            catch (Exception ex)
            {
                _LogDosyasi.Info(ex.ToString());
            }

            return collectTagData;
        }

        internal void fn_Sirala(object state)
        {
            #region Değişkenler
            //string _ReaderId = ConfigurationSettings.AppSettings["ReaderIpAdresi"].ToString().Trim();

            int _Sayac = 0;
            string kapiip = "";
            _LogDosyasi.Info("Servis Başladı");
            #endregion

            while (true)
            {
                try
                {
                    
                    Thread.Sleep(TimeSpan.FromSeconds(2));

                    _Sayac = _Sayac + 1;

                    //Parallel.ForEach(GetCollectTagData(), data =>
                    //{


                    foreach (var item in GetCollectTagData())
                    {
                        try
                        {
                           // _LogDosyasi.Error(item.Key);

                            using (Session session = XpoManager.Instance.GetNewSession())
                            {
                                if (_Sayac == 10)
                                {

                                    //List<tblreaderkimliklendirme> _ItemDizi = session.Query<tblreaderkimliklendirme>().Where(w => DateTime.Now.Subtract(w.sonokuma).TotalSeconds >= 15).ToList();

                                    List<tbl03kapireader> _ItemDizi = session.Query<tbl03kapireader>().ToList();

                                    tblreaderkapiparam _param = session.Query<tblreaderkapiparam>().FirstOrDefault(p => p.aktif == 1);

                                    
                                    if (kapiip !=_param.kapiip)
                                    {

                                        _LogDosyasi.Info("Kapı İp ="+_param.kapiip);
                                        kapiip = _param.kapiip;
                                    }

                                    foreach (var _Item in _ItemDizi)
                                    {
                                        if (DateTime.Now.Subtract(_Item.okumabitis).TotalSeconds >= 15)
                                        {

                                            //okuma bitti
                                            _Item.okumabittimi = 1;
                                            _Item.Save();

                                            if (_Item.gelenepc == _param.kapiepc)
                                            {
                                                _LogDosyasi.Error("Reader okumayı durduru !!!");
                                            }
                                        }
                                    }
                                    _Sayac = 0;
                                }




                                tbl03kapireader _Temp = session.Query<tbl03kapireader>().FirstOrDefault(w => w.gelenepc.Equals(item.Key) && w.okumabittimi == 0);
                                
                                if (_Temp != null)
                                {
                                    _Temp.guncellemezamani = DateTime.Now;
                                    _Temp.okumabitis = item.Value._SonOkumaZamani;
                                    _Temp.lastupdateuser = "windows.service";
                                    _Temp.Save();

                                    _LogDosyasi.Error(_Temp.gelenepc);
                                }

                                else
                                {
                                    new tbl03kapireader(session)
                                    {
                                        aktif = 1,
                                        okumabittimi = 0,
                                        okumabitis = item.Value._SonOkumaZamani,
                                        createuser = "windows.service",
                                        databasekayitzamani = DateTime.Now,
                                        okumabaslama = item.Value._SonOkumaZamani,
                                        gelenepc = item.Key,
                                        guncellemezamani = DateTime.Now,
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        lastupdateuser = "windows.service",
                                        aktarim=0,
                                        buzzer=0,
                                    }.Save();
                                }

                                tbl06analiz _guncelanaliz = session.Query<tbl06analiz>().FirstOrDefault(g => g.aktif ==1 && g.epc.Equals(item.Key));
                                if (_guncelanaliz !=null)
                                {

                                    _guncelanaliz.kapireader = 1;
                                    _guncelanaliz.okumabaslangic = _Temp.okumabaslama;
                                    _guncelanaliz.okumabitis = _Temp.okumabitis;
                                    _guncelanaliz.lastupdateuser = "windows.service";
                                    _guncelanaliz.guncellemezamani = DateTime.Now;
                                    _guncelanaliz.Save();
                                }
                                else
                                {
                                    new tbl06analiz(session)
                                    {
                                        aktif = 1,
                                        alarmdurum = 1,
                                        alarmkapatan = " ",
                                        alarmkapatmaaciklama = "",
                                        id = Guid.NewGuid().ToString().ToUpper(),
                                        createuser = "windows.service",
                                        lastupdateuser = "windows.service",
                                        databasekayitzamani = DateTime.Now,
                                        guncellemezamani = DateTime.Now,
                                        epc = item.Key,
                                        sernr = "yok",
                                        aufnr = "ilişkisiz",
                                        matnr = "yok",
                                        maktx = "yok",
                                        iliskilendiren = "yok",
                                        iliskiiptaleden = "yok",
                                        iliskiyeri = 0,
                                        kimliklendiren = "kimliksiz",
                                        kimlikiptaleden = "yok",
                                        tuketim = 0,
                                        gecisizni = 0,
                                        okumabaslangic = Convert.ToDateTime(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss.fffffffK")),
                                        okumabitis = Convert.ToDateTime(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss.fffffffK")),
                                        alarmkapatmatarih = Convert.ToDateTime(DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss.fffffffK")),
                                        kapireader = 1,

                                    }.Save();
                                }

                                

                            }


                            //DpyReaderOkumaArsivView view = new DpyReaderOkumaArsivView();

                            //view.pTag = item.Key;
                            //view.pOkumaZamani = item.Value._SonOkumaZamani;
                            //arsiv.ReaderOkumaArsivRegistration(view);

                        }
                        catch (Exception ex)
                        {
                            _LogDosyasi.Info(ex.ToString() + "-" + item.Key);
                        }
                    }


                    //});

                }
                catch (Exception ex)
                {
                    _LogDosyasi.Info(ex.ToString());
                }
            }


        }

        private void fn_Start()
        {
            try
            {


                // Assign a name to the reader. 
                // This will be used in tag reports. 
                reader.Name = "My Reader #1";

                // Connect to the reader.
                ConnectToReader();


                // Get the default settings.
                // We'll use these as a starting point
                // and then modify the settings we're 
                // interested in.
                Settings settings = reader.QueryDefaultSettings();

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirmeparam _param = session.Query<tblreaderkimliklendirmeparam>().FirstOrDefault(x => x.aktif == 1);
                    for (int intSayac = 1; intSayac <= settings.Antennas.Length; intSayac++)
                    {
                        //if (settings.Antennas.GetAntenna(ushort.Parse(intSayac.ToString())).IsEnabled)
                        //{
                        settings.Antennas.GetAntenna(ushort.Parse(intSayac.ToString())).TxPowerInDbm = Convert.ToDouble(_param.readerokumagucu);
                        //}
                    }
                }


                // Console.WriteLine(settings.Antennas.Length+"");


                // Start the reader as soon as it's configured.
                // This will allow it to run without a client connected.
                settings.AutoStart.Mode = AutoStartMode.Immediate;
                settings.AutoStop.Mode = AutoStopMode.None;

                // Use Advanced GPO to set GPO #1 
                // when an client (LLRP) connection is present.
                settings.Gpos.GetGpo(1).Mode = GpoMode.LLRPConnectionStatus;

                // Tell the reader to include the timestamp in all tag reports.
                settings.Report.IncludeFirstSeenTime = true;
                settings.Report.IncludeLastSeenTime = true;
                settings.Report.IncludeSeenCount = true;

                // If this application disconnects from the 
                // reader, hold all tag reports and events.
                settings.HoldReportsOnDisconnect = true;

                // Enable keepalives.
                settings.Keepalives.Enabled = true;
                settings.Keepalives.PeriodInMs = 5000;

                // Enable link monitor mode.
                // If our application fails to reply to
                // five consecutive keepalive messages,
                // the reader will close the network connection.
                settings.Keepalives.EnableLinkMonitorMode = true;
                settings.Keepalives.LinkDownThreshold = 5;

                // Assign an event handler that will be called
                // when keepalive messages are received.
                reader.KeepaliveReceived += OnKeepaliveReceived;

                // Assign an event handler that will be called
                // if the reader stops sending keepalives.
                reader.ConnectionLost += OnConnectionLost;

                // Apply the newly modified settings.
                reader.ApplySettings(settings);

                // Save the settings to the reader's 
                // non-volatile memory. This will
                // allow the settings to persist
                // through a power cycle.
                reader.SaveSettings();

                // Assign the TagsReported event handler.
                // This specifies which method to call
                // when tags reports are available.
                reader.TagsReported += OnTagsReported;
            }
            catch (Exception ex)
            {
                _LogDosyasi.Error(ex.ToString());

                Thread.Sleep(TimeSpan.FromSeconds(15));

                fn_Start();
            }
        }


        internal void fn_OkumaBasla(object state)
        {
            fn_Start();
        }

        static void OnTagsReported(ImpinjReader sender, TagReport report)
        {


            // This event handler is called asynchronously 
            // when tag reports are available.
            // Loop through each tag in the report 
            // and print the data.
            try
            {
                foreach (Tag tag in report)
                {
                    _OkumaListesi.Add(new ReadView() { _Epc = tag.Epc.ToString().Replace(" ", ""), _Zaman = DateTime.Now });

                    try
                    {
                        foreach (var item in _OkumaListesi.Where(w => DateTime.Now.Subtract(w._Zaman).TotalSeconds >= 5).ToList())
                        {
                            _OkumaListesi.Remove(item);
                        }
                    }
                    catch (Exception ex)
                    {
                        _LogDosyasi.Info(ex.ToString());
                    }

                    //_LogDosyasi.Info("EPC : " + tag.Epc + " Timestamp : " + tag.LastSeenTime + "");
                }
            }
            catch (Exception ex)
            {
                _LogDosyasi.Info(ex.ToString());
            }
        }


        static void OnConnectionLost(ImpinjReader reader)
        {
            // This event handler is called if the reader  
            // stops sending keepalive messages (connection lost).
            _LogDosyasi.Info("Connection lost : " + reader.Name + " (" + reader.Address + ")");

            // Cleanup
            reader.Disconnect();

            // Try reconnecting to the reader
            ConnectToReader();
        }

        static void OnKeepaliveReceived(ImpinjReader reader)
        {
            // This event handler is called when a keepalive 
            // message is received from the reader.
            // _LogDosyasi.Info("Keepalive received from "+ reader.Name + " ("+ reader.Address + ")" );
        }

        static void ConnectToReader()
        {
            try
            {
                ConfigurationManager.RefreshSection("appSettings");

                //  string READER_HOSTNAME = ConfigurationManager.AppSettings["ReaderIpAdresi"].ToString().Trim();
                string READER_HOSTNAME = "";

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    tblreaderkimliklendirmeparam _Param = session.Query<tblreaderkimliklendirmeparam>().FirstOrDefault(m => m.aktif == 1);

                    READER_HOSTNAME = _Param.readerip;

                }
                // The maximum number of connection attempts
                // before throwing an exception.
                // reader.MaxConnectionAttempts = 15;
                // Number of milliseconds before a 
                // connection attempt times out.
                reader.ConnectTimeout = 6000;
                // Connect to the reader.
                // Change the ReaderHostname constant in SolutionConstants.cs 
                // to the IP address or hostname of your reader.
                reader.Connect(READER_HOSTNAME);
                //Console.WriteLine("Successfully connected.");

                // Tell the reader to send us any tag reports and 
                // events we missed while we were disconnected.
                reader.ResumeEventsAndReports();
            }
            catch (OctaneSdkException e)
            {
                //Console.WriteLine("Failed to connect.");
                throw e;
            }
        }

    }
}
