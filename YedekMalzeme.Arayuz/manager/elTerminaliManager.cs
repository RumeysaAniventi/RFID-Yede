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
    public class elTerminaliManager
    {
        internal ElTerminaliResponse fn_ElTerminali(List<ElTerminaliRequest> v_Gelen)
        {
            ElTerminaliResponse elTerminaliResponse = new ElTerminaliResponse();

            try
            {

                using (Session session = XpoManager.Instance.GetNewSession())
                {
                    elTerminaliResponse.zdizi = new List<ElTerminaliView>();

                    foreach (var item in v_Gelen)
                    {

                        tbl06analiz _analizList = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 && p.tuketim == 0).FirstOrDefault();

                        if (_analizList == null)
                        {
                            //kimliksiz
                            elTerminaliResponse.zdizi.Add(new ElTerminaliView
                            {
                                zepc = item.zepc,
                                zsernr = "",
                                zaufnr = "",
                                zmatnr = "",
                                zmaktx = "",
                                zdurumMesaj = "Kimliksiz ürün!",
                            });

                        }
                        else
                        {
                            //stok kimliklendiren!='kimliksiz' && iliskilendiren=='ilişkisiz' 
                            tbl06analiz _analizListStok = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 && p.tuketim == 0 && p.kimliklendiren != "kimliksiz" && p.iliskilendiren == "ilişkisiz").FirstOrDefault();

                            if (_analizListStok != null)
                            {

                                elTerminaliResponse.zdizi.Add(new ElTerminaliView
                                {
                                    zid = _analizListStok.id,
                                    zepc = item.zepc,
                                    zsernr = _analizListStok.sernr,
                                    zaufnr = "",
                                    zmatnr = _analizListStok.matnr,
                                    zmaktx = _analizListStok.maktx,
                                    zdurumMesaj = "Stok ürünü",

                                });

                            }
                            tbl06analiz _analiz = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 ).FirstOrDefault();

                            if (_analiz != null)
                            {
                                if (_analiz.tuketim == 1)
                                {
                                    elTerminaliResponse.zdizi.Add(new ElTerminaliView
                                    {
                                        zepc = item.zepc,
                                        zsernr = _analiz.sernr,
                                        zaufnr = _analiz.aufnr,
                                        zmatnr = _analiz.matnr,
                                        zmaktx = _analiz.maktx,
                                        zdurumMesaj = "Bu ürün tüketilmiştir!",

                                    });
                                }
                                else if(_analiz.tuketim == 0 && _analiz.iliskilendiren != "ilişkisiz")
                                {
                                    elTerminaliResponse.zdizi.Add(new ElTerminaliView
                                    {
                                        zid = _analiz.id,
                                        zepc = item.zepc,
                                        zsernr = _analiz.sernr,
                                        zaufnr = _analiz.aufnr,
                                        zmatnr = _analiz.matnr,
                                        zmaktx = _analiz.maktx,
                                        zdurumMesaj = "İlişkili ürün(koltuk depo ürünü)!",

                                    });
                                }

                            }



                            //ilişkili iliskilendiren != 'iliskisiz'
                            //tbl06analiz _analizListIliskili = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 && p.tuketim == 0 && p.iliskilendiren != "ilişkisiz").FirstOrDefault();

                            //if (_analizListIliskili != null)
                            //{

                            //    elTerminaliResponse.zdizi.Add(new ElTerminaliView
                            //    {
                            //        zid= _analizListIliskili.id,
                            //        zepc = item.zepc,
                            //        zsernr = _analizListIliskili.sernr,
                            //        zaufnr = _analizListIliskili.aufnr,
                            //        zmatnr = _analizListIliskili.matnr,
                            //        zmaktx = _analizListIliskili.maktx,
                            //        zdurumMesaj = "İlişkili ürün(koltuk depo ürünü)!",

                            //    });
                            //}

                            //tbl06analiz _analizListTuketilen = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 && p.tuketim == 1).FirstOrDefault();

                            //if (_analizListIliskili != null)
                            //{
                            //    elTerminaliResponse.zdizi.Add(new ElTerminaliView
                            //    {
                            //        zepc = item.zepc,
                            //        zsernr = _analizListTuketilen.sernr,
                            //        zaufnr = _analizListTuketilen.aufnr,
                            //        zmatnr = _analizListTuketilen.matnr,
                            //        zmaktx = _analizListTuketilen.maktx,
                            //        zdurumMesaj = "Bu ürün tüketilmiştir!",

                            //    });
                            //    ;
                            //}


                        }


                    }

                    elTerminaliResponse.zAciklama = "";
                    elTerminaliResponse.zSonuc = 1;

                }
            }
            catch
            {
                elTerminaliResponse.zdizi = new List<ElTerminaliView>();
                elTerminaliResponse.zAciklama = "";
                elTerminaliResponse.zSonuc = -1;

            }

            return elTerminaliResponse;
        }
    }
}