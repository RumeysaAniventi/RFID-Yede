using Npgsql;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Web.Configuration;

namespace YedekMalzeme.Arayuz
{
    public class cVeriTabaniIslem
    {
        public cVeriTabaniIslem()
        {

        }

        public string _BaglantiString()
        {
            string _Sonuc = "";
            string password = "";
            //try
            //{

            var section = WebConfigurationManager.GetSection("secureAppSettings") as NameValueCollection;

            if (section != null && section["BaglantiSifre"] != null)
            {
                password = section["BaglantiSifre"];
            }


            // "Server=localhost;User ID=postgres;password=Ankara123;Database=Artieles";
            _Sonuc = "Server=" + ConfigurationManager.AppSettings["BaglantiSunucuIp"] +
            ";User ID=" + ConfigurationManager.AppSettings["BaglantiKullaniciAdi"] +
            ";password=" + password +
            ";Database=" + ConfigurationManager.AppSettings["BaglantiDatabase"].ToString() + "";

            //}
            //catch (Exception ex)
            //{
            //    _Sonuc = "";
            //}

            return _Sonuc;
        }

        public void _fnSqlCalistir(string _Sql, string FonksiyonAdi)
        {
            #region Değişkenler
            NpgsqlConnection _baglanti;
            NpgsqlCommand _Komut;
            #endregion

            _baglanti = new NpgsqlConnection();

            //try
            //{

            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut = new NpgsqlCommand(_Sql);
            _Komut.Connection = _baglanti;
            _Komut.Parameters.Clear();

            _Komut.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //}

            //finally
            //{
            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }
            //}       
        }

        public void _fnSqlCalistir(NpgsqlCommand _Komut)
        {
            #region Değişkenler
            NpgsqlConnection _baglanti;
            #endregion

            _baglanti = new NpgsqlConnection();
            //try
            //{

            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut.Connection = _baglanti;

            _Komut.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{

            //}

            //finally
            //{
            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }
            //}
        }

        public DataTable _fnDataTable(string _Sql)
        {
            #region Değişkenler
            DataTable _dTable;
            NpgsqlConnection _baglanti;
            NpgsqlCommand _Komut;
            NpgsqlDataAdapter _adapter;
            #endregion

            _baglanti = new NpgsqlConnection();

            _dTable = new DataTable();


            //try
            //{
            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut = new NpgsqlCommand(_Sql);
            _Komut.Connection = _baglanti;
            _Komut.Parameters.Clear();

            _adapter = new NpgsqlDataAdapter(_Komut);
            _adapter.Fill(_dTable);
            //}
            //catch (Exception ex)
            //{

            //}

            //finally
            //{
            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }
            //}
            return _dTable;
        }

        public DataTable _fnDataTable(NpgsqlCommand _Komut)
        {
            #region Değişkenler
            DataTable _dTable;
            NpgsqlConnection _baglanti;
            NpgsqlDataAdapter _adapter;
            #endregion

            _baglanti = new NpgsqlConnection();

            _dTable = new DataTable();

            //try
            //{           
            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

           
            _Komut.Connection = _baglanti;

            _adapter = new NpgsqlDataAdapter(_Komut);

            _adapter.Fill(_dTable);

            //}
            //catch (Exception ex)
            //{

            //}

            //finally
            //{
            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }
            //}
            return _dTable;
        }


       
    }
}