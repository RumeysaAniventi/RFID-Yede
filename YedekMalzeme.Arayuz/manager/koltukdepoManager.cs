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
    public class koltukdepoManager
    {
        internal BilesenMalzemeListesiResponse fn_BilesenMalzemeListesi(BilesenMalzemeListesiRequest v_Gelen)
        {
            #region Değişkenler
            BilesenMalzemeListesiResponse _Cevap = new BilesenMalzemeListesiResponse();
            #endregion

            try
            {
                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    List<tbl01eklecikararsiv> _ItemDizi = session.Query<tbl01eklecikararsiv>().Where(w => w.aktif == 1 && w.aufnr.Equals(v_Gelen.zaufnr)).ToList();

                    _Cevap = new BilesenMalzemeListesiResponse();
                    _Cevap.zAciklama = "";
                    _Cevap.zSonuc = 1;
                    _Cevap.zDizi = new List<BilesenMalzemeListesiView>();

                    foreach (var item in _ItemDizi)
                    {
                        tblmalzemebelgelistesiresponse _belgelistesi = session.Query<tblmalzemebelgelistesiresponse>().FirstOrDefault(a => a.aktif == 1 && a.sernr.Equals(item.sernr));
                        if (_belgelistesi==null)
                        {
                            _Cevap.zDizi.Add(new BilesenMalzemeListesiView()
                            {
                                zkullanici = item.kullanici,
                                zmaktx = item.maktx,
                                zmatnr = item.matnr,
                                zsernr = item.sernr
                            });
                        }
                        
                    }

                    return _Cevap;

                }
            }
            catch (Exception ex)
            {
                _Cevap = new BilesenMalzemeListesiResponse();
                _Cevap.zSonuc = -1;
                _Cevap.zAciklama = "Hata " + ex.ToString();
            }

            return _Cevap;
        }
    }
}