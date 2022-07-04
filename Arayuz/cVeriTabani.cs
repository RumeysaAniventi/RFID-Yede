using Npgsql;
using System.Configuration;
using System.Data;

namespace Arayuz
{
    public class cVeriTabani
    {
        public cVeriTabani()
        {

        }

        private string _BaglantiString()
        {
            string _Sonuc = "";
 
            _Sonuc = "Server=" + ConfigurationManager.AppSettings["BaglantiSunucuIp"] +
            ";User ID=" + ConfigurationManager.AppSettings["BaglantiKullaniciAdi"] +
            ";password=" + ConfigurationManager.AppSettings["BaglantiSifre"] +
            ";Database=" + ConfigurationManager.AppSettings["BaglantiDatabase"].ToString() + "";


            return _Sonuc;
        }

        public void _fnSqlCalistir(string _Sql, string FonksiyonAdi)
        {
            #region Değişkenler
            NpgsqlConnection _baglanti;
            NpgsqlCommand _Komut;
            #endregion

            _baglanti = new NpgsqlConnection();

            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut = new NpgsqlCommand(_Sql);
            _Komut.Connection = _baglanti;
            _Komut.Parameters.Clear();

            _Komut.ExecuteNonQuery();

            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }
  
        }

        public void _fnSqlCalistir(NpgsqlCommand _Komut)
        {
            #region Değişkenler
            NpgsqlConnection _baglanti;
            #endregion

            _baglanti = new NpgsqlConnection();


            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut.Connection = _baglanti;

            _Komut.ExecuteNonQuery();

            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }

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


            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut = new NpgsqlCommand(_Sql);
            _Komut.Connection = _baglanti;
            _Komut.Parameters.Clear();

            _adapter = new NpgsqlDataAdapter(_Komut);
            _adapter.Fill(_dTable);

            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }

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
        
            _baglanti.ConnectionString = _BaglantiString();
            _baglanti.Open();

            _Komut.Connection = _baglanti;

            _adapter = new NpgsqlDataAdapter(_Komut);
            _adapter.Fill(_dTable);

            if (_baglanti.State != ConnectionState.Closed)
            {
                _baglanti.Close();
            }

            return _dTable;
        }
    }
}
