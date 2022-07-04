using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

namespace YedekMalzeme.Arayuz.manager
{
    public class MalzemeSecimiManager
    {
        internal MalzemeAraListeleResponse fn_MalzemeAraListele(MalzemeAraListeleRequest v_Gelen)
        {
            #region Değişkenler
            string _ListeYazisi = "";

            MalzemeAraListeleResponse _Cevap = new MalzemeAraListeleResponse();
            #endregion

            using (Session session = XpoManager.Instance.GetNewSession())
            {
                try
                {
                    List<tblmalzemelistesiresponse> _Dizim = session.Query<tblmalzemelistesiresponse>().Where(w => w.matnr.ToString().StartsWith(v_Gelen.zMatnr) && w.aktif == 1).OrderBy(w=>w.maktx).ToList();

                    _ListeYazisi = "";
                    _ListeYazisi+= "<option value='-1'>SEÇİNİZ</option>";


                    foreach (var item in _Dizim)
                    {
                        _ListeYazisi += "<option value='"+item.matnr+"'>"+item.maktx+"</option>";
                    }

                    _Cevap = new MalzemeAraListeleResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                    _Cevap.zListeYazisi = _ListeYazisi;

                }
                catch (Exception ex)
                {
                    _Cevap = new MalzemeAraListeleResponse();
                    _Cevap.zSonuc = -1;
                    _Cevap.zAciklama = "";
                    _Cevap.zListeYazisi = "";

                }
            }

                return _Cevap;
        }

        internal MalzemeUrunListeleResponse fn_MalzemeUrunListele(MalzemeUrunListeleRequest v_Gelen)
        {
            #region Değişkenler
            string _ListeYazisi = "";

            MalzemeUrunListeleResponse _Cevap = new MalzemeUrunListeleResponse();
            #endregion

            using (Session session = XpoManager.Instance.GetNewSession())
            {
                try
                {
                    List<tblmalzemeserialstoklistesi> _Dizim = session.Query<tblmalzemeserialstoklistesi>().Where(w => w.matnr.ToString().Equals(v_Gelen.zmatnr) && w.aktif == 1).OrderBy(w => w.maktx).ToList();

                    _ListeYazisi = "";
                    _ListeYazisi += "<option value='-1'>SEÇİNİZ</option>";


                    foreach (var item in _Dizim)
                    {
                        _ListeYazisi += "<option value='" + item.sernr + "'>" + item.sernr + "</option>";
                    }

                    _Cevap = new MalzemeUrunListeleResponse();
                    _Cevap.zSonuc = 1;
                    _Cevap.zAciklama = "";
                    _Cevap.zListeYazisi = _ListeYazisi;

                }
                catch (Exception ex)
                {
                    _Cevap = new MalzemeUrunListeleResponse();
                    _Cevap.zSonuc = -1;
                    _Cevap.zAciklama = "";
                    _Cevap.zListeYazisi = "";

                }
            }

            return _Cevap;
        }
    }
}