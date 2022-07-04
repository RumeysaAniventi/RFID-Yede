using Arayuz.Request;
using Arayuz.Response;
using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using Entity.YedekMalzemeTakip.Md5;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace Arayuz.Manager
{
    public class BildirimEkraniManager
    {

        internal YedekMalzemeBildirimResponse fn_YedekMalzemeBildirim(YedekMalzemeBildirimRequest v_Gelen)
        {
            #region Değişkenler

            YedekMalzemeBildirimResponse _Cevap = new YedekMalzemeBildirimResponse();

            cVeriTabani _veriTabani = new cVeriTabani();
          
            string _IpAdresi = "";
            #endregion


            using (Session session = XpoManager.Instance.GetNewSession())
            {
                v_Gelen.Sifre = EncryptionHelper.ToMD5(v_Gelen.Sifre).ToUpper();

                _IpAdresi = GetUserIP();

                tblkullanici _Temp = session.Query<tblkullanici>().FirstOrDefault(w => w.kullaniciadi.Equals(v_Gelen.KullaniciAdi) && w.sifre.Equals(v_Gelen.Sifre));

                if (_Temp == null)
                {
                    new tblgelenyedekmalzemelog(session)
                    {
                        aktif = 1,
                        databasekayitzamani = DateTime.Now,
                        guncellemezamani = DateTime.Now,
                        id = Guid.NewGuid().ToString().ToUpper(),
                        ip = _IpAdresi,
                        sonuc = -99,
                        aciklama = "Hatalı Kullanıcı veya Şifre"
                    }.Save();

                    _Cevap = new YedekMalzemeBildirimResponse();
                    _Cevap.Aciklama = "Hatalı Kullanıcı veya Şifre";
                    _Cevap.KayitNo = "-1";
                    _Cevap.Sonuc = -99;
                    return _Cevap;
                }
                else
                {
                    tblgelenyedekmalzemelog _Log = new tblgelenyedekmalzemelog(session)
                    {
                        aktif = 1,
                        databasekayitzamani = DateTime.Now,
                        guncellemezamani = DateTime.Now,
                        id = Guid.NewGuid().ToString().ToUpper(),
                        ip = _IpAdresi,
                        aciklama = "",
                        sonuc = 0                    
                    };

                    _Log.Save();

                    try
                    {
                        new tblgelenyedekmalzeme(session)
                        {
                            aktif = 1,
                            aufnr = v_Gelen.aufnr,
                            budat = v_Gelen.budat,
                            bwart = v_Gelen.bwart,
                            databasekayitzamani = DateTime.Now,
                            guncellemezamani = DateTime.Now,
                            id = _Log.id,
                            lgort = v_Gelen.lgort,
                            maktx = v_Gelen.maktx,
                            matnr = v_Gelen.matnr,
                            mblnr = v_Gelen.mblnr,
                            mblnr2 = v_Gelen.mblnr2,
                            meins = v_Gelen.meins,
                            menge = v_Gelen.menge,
                            mjahr = v_Gelen.mjahr,
                            sernr = v_Gelen.sernr,
                            vornr = v_Gelen.vornr,
                            zeile = v_Gelen.zeile
                        }.Save();

                        _Cevap = new YedekMalzemeBildirimResponse();
                        _Cevap.Aciklama = "İşlem Başarılı";                       
                        _Cevap.Sonuc = 200;
                        _Cevap.KayitNo = _Log.id;

                        _Log.sonuc = 200;
                        _Log.aciklama = "İşlem Başarılı";
                        _Log.hataciklama = "";
                        _Log.Save();


                        return _Cevap;
                    }
                    catch (Exception ex)
                    {
                        _Log.sonuc = -200;
                        _Log.aciklama = "Sistem bir hata oluştu. Lütfen Daha sonra tekrar deneyiniz";
                        _Log.hataciklama = ex.ToString();
                        _Log.Save();
                    }
                }
            }



                return _Cevap;
        }

        internal BildirimSonucView fn_BildirimEkle(RequestBildirimTuru v_Gelen)
        {
            #region Değişkenler
            BildirimSonucView _Cevap = new BildirimSonucView();
            cVeriTabani _veriTabani = new cVeriTabani();
            string _Sql = "";
            DataTable _dTable = new DataTable();
            NpgsqlCommand _Komut = new NpgsqlCommand();
            DataTable _dTable1 = new DataTable();
            NpgsqlCommand _Komut1 = new NpgsqlCommand();
            string exMessage = "";
            #endregion

            _Sql = "select * from tblkullanici where kullaniciadi = @kullaniciadi and sifre = @sifre and aktif = 1";
            _Komut = new NpgsqlCommand(_Sql);
            _Komut.Parameters.Clear();
            string sifre = EncryptionHelper.ToMD5(v_Gelen.Sifre).ToUpper();
            _Komut.Parameters.AddWithValue("@kullaniciadi", v_Gelen.KullaniciAdi);
            _Komut.Parameters.AddWithValue("@sifre",sifre);

            _dTable1 = _veriTabani._fnDataTable(_Komut);

            if (_dTable1.Rows.Count <= 0)
            {
                _Cevap = new BildirimSonucView();
                _Cevap.Aciklama = "Hatalı Kullanıcı veya Şifre";
                _Cevap.Sonuc = -99;
                return _Cevap;
            }
            else
            {
                if (v_Gelen.BildirimTuru != "yedekparca" && v_Gelen.BildirimTuru != "bakimsiparis")
                {
                    _Cevap = new BildirimSonucView();
                    _Cevap.Aciklama = "Hatalı Bildirim Türü";
                    _Cevap.Sonuc = -98;
                    return _Cevap;

                }



                if ((v_Gelen.BildirimTuru == "yedekparca" || v_Gelen.BildirimTuru == "bakimsiparis"))
                {

                    try
                    {
                        
                        string ip = GetUserIP();

                        #region Arsiv
                        _Sql = "insert into tblbildirimarsiv (id,bildirimturu,databasekayitzamani,aktif,ip) values (@id,@bildirimturu,@databasekayitzamani,@aktif,@ip) ";

                        _Komut = new NpgsqlCommand(_Sql);
                        _Komut.Parameters.Clear();
                        _Komut.Parameters.AddWithValue("@id", Guid.NewGuid());
                        _Komut.Parameters.AddWithValue("@bildirimturu", v_Gelen.BildirimTuru);
                        _Komut.Parameters.AddWithValue("@aktif", 1);
                        _Komut.Parameters.AddWithValue("@databasekayitzamani", DateTime.Now);
                        _Komut.Parameters.AddWithValue("@ip", ip);

                        _veriTabani._fnSqlCalistir(_Komut);
                        #endregion

                        _Sql = "select *  from tblbildirim";

                        _dTable = _veriTabani._fnDataTable(_Sql);

                        if (_dTable.Rows.Count <= 0)
                        {
                            _Sql = "insert into tblbildirim (id,bildirimturu,bildirimzamani,databasekayitzamani,aktif) values (@id,@bildirimturu,@bildirimzamani,@databasekayitzamani,@aktif) ";

                            _Komut = new NpgsqlCommand(_Sql);
                            _Komut.Parameters.Clear();
                            _Komut.Parameters.AddWithValue("@id", Guid.NewGuid());
                            _Komut.Parameters.AddWithValue("@bildirimturu", v_Gelen.BildirimTuru);
                            _Komut.Parameters.AddWithValue("@bildirimzamani", DateTime.Now);
                            _Komut.Parameters.AddWithValue("@aktif", 1);
                            _Komut.Parameters.AddWithValue("@databasekayitzamani", DateTime.Now);

                            _veriTabani._fnSqlCalistir(_Komut);
                        }
                        else
                        {
                            string id = _dTable.Rows[0]["id"].ToString();
                            _Sql = "UPDATE tblbildirim SET bildirimzamani = @bildirimzamani, bildirimturu = @bildirimturu WHERE id = @id";

                            _Komut = new NpgsqlCommand(_Sql);
                            _Komut.Parameters.Clear();
                            _Komut.Parameters.AddWithValue("@bildirimturu", v_Gelen.BildirimTuru);
                            _Komut.Parameters.AddWithValue("@bildirimzamani", DateTime.Now);
                            _Komut.Parameters.AddWithValue("@id", id);

                            _veriTabani._fnSqlCalistir(_Komut);
                        }


                        _Cevap = new BildirimSonucView();
                        _Cevap.Sonuc = 200;
                        _Cevap.Aciklama = "Kayıt Başarılı";

                        //if (v_Gelen.BildirimTuru == "yedekparca")
                        //{
                        //    yedekparcalistesi();
                        //}
                        //else if (v_Gelen.BildirimTuru == "bakimsiparis")
                        //{
                        //    bakimsiparislistesi();
                        //}
                    }
                    catch (Exception e)
                    {
                        _Cevap = new BildirimSonucView();
                        _Cevap.Sonuc = -1;
                        _Cevap.Aciklama = "Başarısız Güncelleme/Kaydetme İşlemi";
                        exMessage = e.Message;
                        return _Cevap;
                    }

                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        new tblbildirimsonuc(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,
                            hatakodu=_Cevap.Sonuc,
                            aciklama=_Cevap.Aciklama,
                            exmessage=exMessage,

                            id = Guid.NewGuid().ToString()
                        }.Save();

                    }
                    return _Cevap;
                }

            }

            
            return _Cevap;
        }

       

        public string GetUserIP()
        {
            string strIP = String.Empty;
            HttpRequest httpReq = HttpContext.Current.Request;

            if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            
            else if
            (
                (httpReq.UserHostAddress.Length != 0)
                &&
               
                ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost"))
            )
            {
                strIP = httpReq.UserHostAddress;
            }
           
            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strIP = sr.ReadToEnd();
                }
                
                int i1 = strIP.IndexOf("Address: ") + 9;
                int i2 = strIP.LastIndexOf("</body>");
                strIP = strIP.Substring(i1, i2 - i1);
            }
            return strIP;
        }


        private BildirimSonucView yedekparcalistesi()
        {
            
            #region Değişkenler

            string _Sql = "";
            cVeriTabani _veriTabani = new cVeriTabani();
            DataTable _dTable = new DataTable();
            NpgsqlCommand _Komut = new NpgsqlCommand();
            BildirimSonucView _Cevap = new BildirimSonucView();

            #endregion


            try
            {
                _Sql = "truncate table tblyedekparca";
                _Komut = new NpgsqlCommand(_Sql);
                _veriTabani._fnSqlCalistir(_Komut);


                //SAP'ye istek gönder verileri çek


                //aktarma

                List<tblyedekparca> tblyedekparcaList = new List<tblyedekparca>();
                for (int i = 0; i < tblyedekparcaList.Count; i++)
                {
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        new tblyedekparca(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,

                            id = Guid.NewGuid().ToString()
                        }.Save();

                    }
                }
                _Cevap.Sonuc = 52;
                _Cevap.Aciklama = "Başarılı Yedek Parça Listesi Çekme";

            }
            catch(Exception ex)
            {
                _Cevap.Sonuc = 82;
                _Cevap.Aciklama = "Başarısız Yedek Parça Listesi Çekme";
            }

            return _Cevap;
        }

        private BildirimSonucView bakimsiparislistesi()
        {

            #region Değişkenler

            string _Sql = "";
            cVeriTabani _veriTabani = new cVeriTabani();
            DataTable _dTable = new DataTable();
            NpgsqlCommand _Komut = new NpgsqlCommand();
            BildirimSonucView _Cevap = new BildirimSonucView();
            #endregion


            try
            {
                _Sql = "truncate table tblbakimsiparis";
                _Komut = new NpgsqlCommand(_Sql);
                _veriTabani._fnSqlCalistir(_Komut);


                //SAP'ye istek gönder verileri çek


                //aktarma

                List<tblbakimsiparis> tblbakimsiparisList = new List<tblbakimsiparis>();
                for (int i = 0; i < tblbakimsiparisList.Count; i++)
                {
                    using (Session session = XpoManager.Instance.GetNewSession())
                    {
                        new tblbakimsiparis(session)
                        {
                            aktif = 1,
                            databasekayitzamani = DateTime.Now,

                            id = Guid.NewGuid().ToString()
                        }.Save();

                    }
                }
                _Cevap.Sonuc = 64;
                _Cevap.Aciklama = "Başarılı Bakım Sipariş Listesi Çekme";

            }
            catch (Exception )
            {
                _Cevap.Sonuc = 18;
                _Cevap.Aciklama = "Başarısız Bakım Sipariş  Listesi Çekme";
            }

            return _Cevap;

        }
    }
}