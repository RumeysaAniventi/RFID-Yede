$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    fn_DegerleriListele();

});


function fn_BilesenEkle(v_Durum)
{
    var v_zepc= $('#txtepcdegeri').val();
    var v_zmatnr = $('#txtkod').val();
    var v_zmaktx = $('#txtad').val();
    var v_zsernr = $('#txtseri').val();
    var v_zaufnr = $('#txtsiparisno').val();
    var v_zeklecikar = $('#txteklecikar').val();

    var v_Yazi = "";

    if (v_Durum == '1')
    {
        v_Yazi = "" + v_zaufnr + " nolu siparişe " + v_zmatnr + " - " + v_zmaktx + " malzemmesini eklemek istediğinize emin misiniz?";
    }
    else {
        if (v_Durum == '-1')
        {
            v_Yazi = "" + v_zaufnr + " nolu siparişe " + v_zmatnr + " - " + v_zmaktx + " malzemmesini çıkarmak istediğinize emin misiniz?";
        }
    }



    swal({
        buttons: {
            cancel: "Vazgeç",
            confirm: "TAMAM"
        },
        title: "UYARI",
        html: true,
        text: v_Yazi,
        icon: "info",
        dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {

                $.ajax({
                    type: "POST",
                    url: "api/SiparisBilesenDegistir",
                    data: JSON.stringify
                        ({
                            zdeger: '1',
                            zepc: v_zepc,
                            zmatnr: v_zmatnr,
                            zmaktx: v_zmaktx,
                            zsernr: v_zsernr,
                            zaufnr: v_zaufnr
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
                            swal({
                                buttons: {
                                    confirm: "TAMAM"
                                },
                                title: "İşlem Tamamlandı",
                                html: true,
                                text: "Sipariş Bileşenleri Başarıyla Güncellendi",
                                icon: "success",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    window.location.href = 'pmlistesiparis.aspx';
                                });
                        }
                        else {

                            swal({
                                buttons: {
                                    confirm: "TAMAM"
                                },
                                title: "Hata",
                                html: true,
                                text: "İşlem sırasında bir hata oluştu \n" + msg.zAciklama,
                                icon: "error",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    window.location.href = 'pmlistesiparis.aspx';
                                });

                            //UyariMesajiVer('Sistemsel bir hata oluştu');
                        }

                    },
                    complete: function () {

                      
                    }
                });


                //

            }

        });

}




function js_MalzemeEkleCikar(v_aufnr)
{
    $('#txtsiparisno').val(v_aufnr);

 


    $.ajax({
        type: "POST",
        url: "api/EpcKimlikBilgileri",
        data: JSON.stringify
            ({
                zdeger: '1',
                zaufnr: v_aufnr
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

                //txtepcdegeri
                $('#txtepcdegeri').val(msg.zepc);
                $('#txtkod').val(msg.zmatnr);
                $('#txtad').val(msg.zmaktx);
                $('#txtseri').val(msg.zsernr);
                //debugger;
                if (msg.zeklesil == '1') {
                    $("#divCikar").css("display", "none");
                    $("#divEkle").css("display", "block");

                    $('#txteklecikar').val(msg.zeklesil);
                }
                else
                {
                    $("#divEkle").css("display", "none");
                    $("#divCikar").css("display", "block");

                    $('#txteklecikar').val(msg.zeklesil);
                }

            }
            else {
                UyariMesajiVer('Standın üzerinde malzeme olduğundan emin olunuz !!!');
            }
        },

        complete: function () {


            $('#m_modal_2').modal({
                show: true,
                keyboard: false,
                backdrop: 'static'
            });
        }
    });


}


function jsBilesenGoruntule(v_aufnr)
{
    $.ajax({
        type: "POST",
        url: "api/SiparisMalzemeListesi",
        data: JSON.stringify
            ({
                zdeger: '1',
                zaufnr: v_aufnr
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

                $('#m_table_bilesen tbody').html(msg.ztabloyazisi);

            }
            else {
                UyariMesajiVer('Standın üzerinde malzeme olduğundan emin olunuz !!!');
            }
        },

        complete: function () {

            $('#m_modal_1').modal('show');
        }
    });
}

function js_SarfEt()
{

}


function fn_DegerleriListele() {

    $.ajax({
        type: "POST",
        url: "api/AcikPmListele",
        data: JSON.stringify
            ({
                zdeger: '1',
                zdepo:'0'
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            if ($.fn.dataTable.isDataTable('#tabloVeriler')) {
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

                $('#m_table_1 tbody').html(msg.zListeYazisi);

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

            settings.language={
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
                                <a class="dropdown-item" href="#" onclick="jsBilesenGoruntule('`+ full[0] + `');"><i class="la la-edit"></i>Bileşenleri Görüntüle</a>
                                <a class="dropdown-item" href="#" onclick="js_MalzemeEkleCikar('`+ full[0] + `');"><i class="la la-edit"></i>Malzeme Ekle Çıkar</a>
                        
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


