using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;
using YedekMalzeme.Arayuz.View;

namespace YedekMalzeme.Arayuz.manager
{
    public class StokRaporManager
    {
        internal DepoListeleResponse fn_DepoListele(DepoListeleRequest v_Gelen)
        {
            DepoListeleResponse _Cevap=new DepoListeleResponse();
          
            try
            {
                using (Session session =XpoManager.Instance.GetNewSession())
                {
                    List<tbl06analiz> _depo = session.Query<tbl06analiz>().Where(d => d.aktif == 1 && d.aufnr.Equals("ilişkisiz") &&d.kimliklendiren!="kimliksiz").ToList();

                    _Cevap.zdizi = new List<DepoListeleView>();

                    foreach (var item in _depo)
                    {
                        _Cevap.zdizi.Add(new DepoListeleView
                        {
                            zsernr = item.sernr,
                            zmaktx = item.maktx,
                            zmatnr = item.matnr,
                        });
                    }

                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                }
                
            }
            catch (Exception)
            {
                _Cevap.zdizi = new List<DepoListeleView>();
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal TuketimListeleResponse fn_TuketimListele(TuketimListeleRequest v_Gelen)
        {
            TuketimListeleResponse _Cevap = new TuketimListeleResponse();
            try
            {
                using (Session session =XpoManager.Instance.GetNewSession())
                {
                    List<tbl06analiz> tuketim = session.Query<tbl06analiz>().Where(d => d.aktif == 1 && d.tuketim == 1).ToList();


                    _Cevap.zdizi = new List<TuketimListeleView>();

                    foreach (var item in tuketim)
                    {
                        _Cevap.zdizi.Add(new TuketimListeleView
                        {
                            zaufnr = item.aufnr,
                            zsernr = item.sernr,
                            zmaktx = item.maktx,
                            zmatnr = item.matnr
                        });
                    }
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                }
            }
            catch (Exception)
            {

                _Cevap.zdizi = new List<TuketimListeleView>();
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }
            return _Cevap;
        }

        internal KoltukDepoListeleResponse fn_KoltukDepoListele(KoltukDepoListeleRequest v_Gelen)
        {
            KoltukDepoListeleResponse _Cevap = new KoltukDepoListeleResponse();
      
            try
            {
                using (Session session=XpoManager.Instance.GetNewSession())
                {

                    List< tbl06analiz> koltukdepo = session.Query<tbl06analiz>().Where(d => d.aktif == 1 && d.aufnr != ("ilişkisiz") && d.kimliklendiren != ("kimliksiz")).ToList();


                    _Cevap.zdizi = new List<KoltukDepoListeleView>();

                    foreach (var item in koltukdepo)
                    {
                        _Cevap.zdizi.Add(new KoltukDepoListeleView
                        {
                            zaufnr = item.aufnr,
                            zsernr = item.sernr,
                            zmaktx = item.maktx,
                            zmatnr = item.matnr
                        });
                    }
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;

                }

            }
            catch (Exception)
            {

                _Cevap.zdizi = new List<KoltukDepoListeleView>();
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }

            return _Cevap;
        }

        internal StokSayResponse fn_StokSay(StokSayRequest v_Gelen)
        {
            StokSayResponse _Cevap = new StokSayResponse();

            try
            {
                using (Session session =XpoManager.Instance.GetNewSession())
                {
                    string depo=session.Query<tbl06analiz>().Where(d => d.aktif == 1 && d.aufnr.Equals("ilişkisiz") && d.kimliklendiren != "kimliksiz").Count().ToString();

                    string koltukdepo = session.Query<tbl06analiz>().Where(d => d.aktif == 1 && d.aufnr !=( "ilişkisiz") && d.kimliklendiren !=( "kimliksiz")).Count().ToString();
                        
                    string tuketim= session.Query<tbl06analiz>().Where(d => d.aktif == 1 && d.tuketim==1).Count().ToString();

                    _Cevap.zdepo = depo;
                    _Cevap.zkoltukdepo = koltukdepo;
                    _Cevap.ztuketim = tuketim;
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                }

                
            }
            catch (Exception)
            {

                _Cevap.zdepo = "";
                _Cevap.zkoltukdepo = "";
                _Cevap.ztuketim = "";
                _Cevap.zAciklama = "";
                _Cevap.zSonuc = -1;
            }
            return _Cevap;
        }
    }
}