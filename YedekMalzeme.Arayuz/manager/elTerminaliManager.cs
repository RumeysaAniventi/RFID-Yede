using DevExpress.Xpo;
using Entity.YedekMalzemeTakip.EntityFramework;
using Entity.YedekMalzemeTakip.Important;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YedekMalzeme.Arayuz.request;
using YedekMalzeme.Arayuz.response;

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


                    foreach (var item in v_Gelen)
                    {
                      
                            tbl06analiz _analizList = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1).FirstOrDefault();

                            if (_analizList == null)
                            {
                                //kimliksiz
                                elTerminaliResponse.zepc = item.zepc;
                                elTerminaliResponse.zaufnr = "";
                                elTerminaliResponse.zmatnr = "";
                                elTerminaliResponse.zmaktx = "";
                                elTerminaliResponse.zdurumMesaj = "Kimliksiz ürün!";
                                elTerminaliResponse.zAciklama = "";
                                elTerminaliResponse.zSonuc = 1;



                            }
                            else
                            {
                                //stok kimliklendiren!='kimliksiz' && iliskilendiren=='ilişkisiz' 
                                tbl06analiz _analizListStok = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 && p.kimliklendiren!="kimliksiz" && p.iliskilendiren=="ilişkisiz").FirstOrDefault();

                                if (_analizListStok!=null)
                                {
                                    elTerminaliResponse.zepc = item.zepc;
                                    elTerminaliResponse.zmatnr = _analizListStok.matnr;
                                    elTerminaliResponse.zmaktx = _analizListStok.maktx;
                                    elTerminaliResponse.zdurumMesaj = "Stok ürünü";
                                    elTerminaliResponse.zAciklama = "";
                                    elTerminaliResponse.zSonuc = 1;
                                }

                                //ilişkili iliskilendiren != 'iliskisiz'
                                tbl06analiz _analizListIliskili = session.Query<tbl06analiz>().Where(p => p.epc == item.zepc && p.aktif == 1 && p.iliskilendiren != "ilişkisiz").FirstOrDefault();

                                if (_analizListIliskili != null)
                                {
                                    elTerminaliResponse.zepc = item.zepc;
                                    elTerminaliResponse.zaufnr = item.zepc;

                                    elTerminaliResponse.zmatnr = _analizListStok.matnr;
                                    elTerminaliResponse.zmaktx = _analizListStok.maktx;
                                    elTerminaliResponse.zdurumMesaj = "İlişkili ürün!";
                                    elTerminaliResponse.zAciklama = "";
                                    elTerminaliResponse.zSonuc = 1;
                                }


                            }

                        
                    }

                }
            }
            catch
            {

            }

            return elTerminaliResponse;
        }
    }
}