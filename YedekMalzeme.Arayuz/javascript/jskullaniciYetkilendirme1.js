$(document).ready(function () {

    fn_DegerleriListele1();

   

});

function jsSifreYenileme(v_zid) {
    swal({
        buttons: {
            cancel: "iPTAL",
            confirm: "TAMAM"
        },
        title: "Onay?",
        text: " Kullanıcının şifresi değiştirilsin mi?",
        icon: "warning",
        dangerMode: false
    })
        .then((willDelete) => {
            if (willDelete) {
                $.ajax({
                    type: "POST",
                    url: "api/SifreYenileme",
                    data: JSON.stringify
                        ({
                            zid: v_zid,
                            
                        }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    error: function (request, status, error) {
                        UyariMesajiVer('Sistemsel Hata Oluştu');
                    },
                    success: function (msg) {

                        if (msg.zSonuc == "1") {

                            

                            swal({
                                buttons: {
                                    confirm: "TAMAM"
                                },
                                title: "İşlem Tamamlandı",
                                text: "Kullanıcının şifresi değiştirildi.\n Lütfen not ediniz== "+ msg.zYenisifre ,
                                icon: "success",
                                dangerMode: false
                            })
                                //.then((willDelete) => {
                                //    window.location.href = 'kullaniciYetkilendirme1.aspx';
                                //});
                        }
                    },

                    complete: function () {
                      

                    }
                });
            }
        });
}

function JsGuncelleModalAc(v_zid) {


    $.ajax({
        type: "POST",
        url: "api/YetkiGuncelleModalAc",
        data: JSON.stringify
            ({
                zdeger: '1',
                zid: v_zid
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {



        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {

            if (msg.zSonuc == "1") {
                $('#txtid').val(msg.zid);
                $('#txtGuncelAd').val(msg.zAdi);
                $('#txtGuncelSoyAd').val(msg.zSoyAdi);
                $('#txtGuncelKullaniciAdi').val(msg.zKullaniciAdi);
                $('#selectGuncelYetki').val(msg.zYetki);



            }
            else {
                UyariMesajiVer(msg.zAciklama);
            }
        },

        complete: function () {

         //   v_zid = $('#txtid').val();
           
            $('#m_modal_2').modal({
                show: true,
                keyboard: false,
                backdrop: 'static'
            });
        }
    });

}

function JsGuncelle() {

    var v_zad = $('#txtGuncelAd').val();
    var v_zzoyad = $('#txtGuncelSoyAd').val();
    var v_zkullaniciadi = $('#txtGuncelKullaniciAdi').val();
    var v_zyetki = $('#selectGuncelYetki').val();
    var v_zid = $('#txtid').val();

    $.ajax({
        type: "POST",
        url: "api/YetkiGuncelle",
        data: JSON.stringify
            ({
                zdeger: '1',
                zid: v_zid,
                zad: v_zad,
                zsoyad: v_zzoyad,
                zkullaniciadi: v_zkullaniciadi,
                zyetki: v_zyetki

            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            

            $('#m_modal_2').modal({
                show: true,
                keyboard: false,
                backdrop: 'static'
            });
        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {
            
            if (msg.zSonuc == "1") {

                BasariliIslem('Kullanıcı Güncellendi');

                $('#m_modal_2').modal('hide');
             
            }
            else {
                UyariMesajiVer(msg.zAciklama + ' Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            fn_DegerleriListele1();
        }
    });
}

function jsYetkiSil(v_zid) {

    swal({
        buttons: {
            cancel: "İPTAL ET",
            confirm: "SİL"
        },
        title: "UYARI",
        html: true,
        text: "Kullanıcı kalıcı olarak silinecektir. Devam etmek istiyor musunuz?",
        icon: "warning",
        dangerMode: true
    })
        .then((willDelete) => {



            if (willDelete) {


                $.ajax({
                    type: "POST",
                    url: "api/YetkiSil",
                    data: JSON.stringify
                        ({
                            zdeger: '1',
                            zid: v_zid
                        }),

                    contentType: "application/json; charset=utf-8",

                    dataType: "json",

                    beforeSend: function () {



                    },
                    error: function (request, status, error) {

                        UyariMesajiVer('Sistemsel bir hata oluştu');
                    },
                    success: function (msg) {



                        if (msg.zSonuc == "1") {

                            BasariliIslem('Kullanıcı Silindi.')

                        }
                        else {
                            UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
                        }
                    },

                    complete: function () {

                        fn_DegerleriListele1()
                    }
                });
            

            }

        });


}

function jsKisiKaydet() {
 
    var v_zkullaniciadi = $('#txtkullaniciAdi').val();
    var v_zadi = $('#txtAdi').val();
    var v_zsoyadi = $('#txtSoyAdi').val();
    var v_zsifre = $('#txtSifre').val();
    var v_zyetki = $('#selectYetki').val();

    $.ajax({
        type: "POST",
        url: "api/KisiKaydet",
        data: JSON.stringify
            ({
               zdeger: '1',
               zkullaniciadi: v_zkullaniciadi,
               zadi: v_zadi,
               zsoyadi: v_zsoyadi,
               zsifre: v_zsifre,
               zyetki: v_zyetki

            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            

        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {

            if (msg.zSonuc == "1") {

                BasariliIslem('Kullanıcı Kaydedildi');

            }
            else {
                UyariMesajiVer(msg.zAciklama );
            }
        },

        complete: function () {

        }  
    });
}



function fn_DegerleriListele1() {

    $.ajax({
        type: "POST",
        url: "api/KisiListele1",
        data: JSON.stringify
            ({
                zdeger: '1',
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            if ($.fn.dataTable.isDataTable('#m_table_1')) {
                table = $('#m_table_1').DataTable();

                table.clear();
                table.destroy();
            }

        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {



            if (msg.zSonuc == "1") {

                $('#m_table_1 tbody').html(msg.ztabloyazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            var v_Yazi = 1;

            var settings = {};
            settings.pageLength = 10;
            settings.paging = true;
            settings.dom = 'Bfrtip';
            settings.searching = true;
            settings.ordering = true;

            settings.lengthMenu = [5, 10, 15];

            settings.language = {
                "url": "https://cdn.datatables.net/plug-ins/1.10.25/i18n/Turkish.json"
            },

                settings.columnDefs = [
                    {
                        targets: -1,
                        data: 'name',
                        title: '',
                        orderable: false,
                        render: function (data, type, full, meta) {



                            return `
                        <span class="dropdown" style='cursor:pointer !important;'>
                            <a href="#" style='cursor:pointer !important;' class="btn m-btn m-btn--hover-success m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="#" onclick="JsGuncelleModalAc('`+full[0]+ `');"><i class="la la-edit"></i>Kullanıcı Güncelle</a>
                                <a class="dropdown-item" href="#" onclick="jsYetkiSil('`+full[0]+ `');"><i class="la la-edit"></i>Kullanıcı Sil</a>
                                <a class="dropdown-item" href="#" onclick="jsSifreYenileme('`+full[0]+ `');"><i class="la la-edit"></i>Şifre Resetle</a>
                               
                            </div>
                        </span>
                        `;
                        }
                    }],


                settings.buttons = [
                    //{
                    //    extend: 'pdf',
                    //    customize: function (doc) {
                    //        doc.content[1].table.widths =
                    //            Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                    //    }
                    //},

                    //{
                    //    extend: 'excel',
                    //    exportOptions: {
                    //        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    //    }
                    //}
                ];

            $('#m_table_1').DataTable(settings);
        }
    });
}



