$(document).ready(function () {

    fn_EpcGetir();
});

function jsSecimiTamamla() {

    var v_secilimatnr = $('#listematnr').val();
    var v_secilimaktx = $('#listemaktx').val();

    $.ajax({
        type: "POST",
        url: "api/SecimiTamamla",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmaktx: v_secilimaktx,
                zmatnr: v_secilimatnr

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

                $('#txtkod').val(msg.zmatnr);
                $('#txtad').val(msg.zmaktx);
            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {



        }
    });
}
function jsmalzemeAdiBul() {

    var v_malzemekodu = $('#listematnr').val();


    $.ajax({
        type: "POST",
        url: "api/MalzemeAdiBul",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmatnr: v_malzemekodu

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

                //listemaktx.html(msg.zmaktx);
                $('#listemaktx').val(msg.zmaktx);
            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {



        }
    });
}

function jsmalzemeKuduBul() {

    var v_malzemeadi = $('#listemaktx').val();


    $.ajax({
        type: "POST",
        url: "api/MalzemeKoduBul",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmaktx: v_malzemeadi

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

                //listemaktx.html(msg.zmaktx);
                $('#listematnr').val(msg.zmatnr);
            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {



        }
    });
}

function jsMalzemelerListele() {

    var v_mazlemeadi = $('#listemaktx');
    var v_mazlemekodu = $('#listematnr');


    $.ajax({
        type: "POST",
        url: "api/MazlemeListele",
        data: JSON.stringify
            ({
                zdeger: '1',

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

                v_mazlemeadi.html(msg.zmalzemeListesi);
                v_mazlemekodu.html(msg.zmalzemkodulistesi);


                $('#m_modal_2').modal({
                    show: true,

                });


            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {



        }
    });
}

function fn_MalzemeAdiListele() {

    var v_gelenlistesiparis = $('#listematnr');

    $.ajax({
        type: "POST",
        url: "api/MalzemeAdiListele",
        data: JSON.stringify
            ({
                zdeger: '1',

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

                v_gelenlistesiparis.html(msg.zaufnrlistesi);


                $('#m_modal_2').modal({
                    show: true,

                });


            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {



        }
    });

}


function js_Tamamla() {
    var v_zepc = $('#txtepcdegeri').val();
    var v_zmatnr = $('#txtkod').val();
    var v_zmaktx = $('#txtad').val();
    var v_zsernr = $('#listeserino').val();

    if (v_zepc.length != 24) {
        swal({
            buttons: {
                confirm: "TAMAM"
            },
            title: "UYARI",
            html: true,
            text: "Etiket değeri bulunamadı",
            icon: "warning",
            dangerMode: true
        })
            .then((willDelete) => {
                return false;
            });

        return false;
    }

    else {
        if (v_zmatnr.length < 5) {
            swal({
                buttons: {
                    confirm: "TAMAM"
                },
                title: "UYARI",
                html: true,
                text: "Malzeme kodu bulunamadı. Lütfen malzeme seçimi yapınız",
                icon: "warning",
                dangerMode: true
            })
                .then((willDelete) => {
                    return false;
                });

            return false;
        }
        else {
            if (v_zmaktx.length < 5) {
                swal({
                    buttons: {
                        confirm: "TAMAM"
                    },
                    title: "UYARI",
                    html: true,
                    text: "Malzeme adı bulunamadı. Lütfen malzeme seçimi yapınız",
                    icon: "warning",
                    dangerMode: true
                })
                    .then((willDelete) => {
                        return false;
                    });

                return false;
            }
            else {
                if (v_zsernr == '-1') {
                    swal({
                        buttons: {
                            confirm: "TAMAM"
                        },
                        title: "UYARI",
                        html: true,
                        text: "Lütfen seri numarası seçiniz",
                        icon: "warning",
                        dangerMode: true
                    })
                        .then((willDelete) => {
                            $('#listeserino').focus();
                            return false;
                        });

                    return false;
                }
            }
        }
    }



    swal({
        buttons: {
            cancel: "Vazgeç",
            confirm: "TAMAM"
        },
        title: "UYARI",
        html: true,
        text: "Kimliklendirme işlemine devam etmek istiyor musunuz",
        icon: "info",
        dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {

                $.ajax({
                    type: "POST",
                    url: "api/KimlikKimliklendir",
                    data: JSON.stringify
                        ({
                            zdeger: '1',
                            zepc: v_zepc,
                            zmatnr: v_zmatnr,
                            zmaktx: v_zmaktx,
                            zsernr: v_zsernr,
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
                                text: "Kimliklendirme işlemi başarıyla tamamlandı",
                                icon: "success",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    window.location.href = 'kimliklendirmev2.aspx';
                                });
                        }
                        else {

                            swal({
                                buttons: {
                                    confirm: "TAMAM"
                                },
                                title: "Hata",
                                text: "İşlem sırasında bir hata oluştu :" + msg.zAciklama,
                                icon: "error",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    window.location.href = 'kimliklendirmev2.aspx';
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

function js_SeriNoListele(v_MalzemeKodu) {
    $.ajax({
        type: "POST",
        url: "api/KimlikSeriNoListe",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmatnr: v_MalzemeKodu
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

                var vYanitDizi = msg.zDizi;

                var _TabloYazisi = "<option value='-1'>SEÇİNİZ</option>";

                for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {

                    _TabloYazisi += "<option value='" + vYanitDizi[iSayac].zsernrkod + "'>" + vYanitDizi[iSayac].zsernrad + "</option>";
                }

                $('#listeserino').html(_TabloYazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {


        }
    });

}

function jsSecimSec(v_MalzemeKodu, v_MalzemeAdi) {

    v_MalzemeAdi = decodeURI(v_MalzemeAdi);

    $('#txtkod').val(v_MalzemeKodu);

    $('#txtad').val(v_MalzemeAdi);

    js_SeriNoListele(v_MalzemeKodu);


    $('#m_modal_1').modal('hide');

}

function fn_EpcGetir() {


    $.ajax({
        type: "POST",
        url: "api/KimlikEpcGetir",
        data: JSON.stringify
            ({
                zdeger: '1'
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

                $('#txtepcdegeri').val(msg.zepc);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {
            setTimeout("fn_EpcGetir()", 5000);

        }
    });
}


function jsModalAc1() {

    $.ajax({
        type: "POST",
        url: "api/Listele",
        data: JSON.stringify
            ({
                zdeger: '1',


            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {


            fn_MalzemeAdiListele();

        },

        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },

        success: function (msg) {

            if (msg.zSonuc == "1") {



                $('#m_modal_2').modal({
                    show: true,

                });


            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {



        }
    });



}

function jsModalAc() {
    $('#txtmalzemekodu').val('');

    $('#txtmalzemeadi').val('');

    $('#m_modal_1').modal({
        show: true,
        keyboard: false,
        backdrop: 'static'
    });
}


function jsMalzemeListesiAra() {

    var v_MalzemeKodu = $('#txtmalzemekodu').val().trim();

    var v_MalzemeAdi = $('#txtmalzemeadi').val().trim();

    if (v_MalzemeKodu.length < 3 && v_MalzemeAdi.length < 3) {
        UyariMesajiVer('Lütfen en az bir alanı doldurunuz');

        return false;

    }

    $.ajax({
        type: "POST",
        url: "api/KimliklMalzemeListesi",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmalzemekodu: v_MalzemeKodu,
                zmalzemeadi: v_MalzemeAdi
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        befored: function () {

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

                var vYanitDizi = msg.zDizi;

                var _TabloYazisi = '';

                for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {
                    _MalzemeAdi = encodeURI(vYanitDizi[iSayac].zmaktx);

                    _TabloYazisi += '<tr>';
                    _TabloYazisi += "<td>" + vYanitDizi[iSayac].zmatnr + "</td>";
                    _TabloYazisi += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";
                    _TabloYazisi += "<td style='text-align:center'><a class=\"m-link\" href=\"#\" onclick = jsSecimSec('" + vYanitDizi[iSayac].zmatnr + "','" + _MalzemeAdi + "');>Seç</a></td>";
                    _TabloYazisi += "</tr>";
                }

                $('#m_table_1 tbody').html(_TabloYazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            var settings = {};
            settings.pageLength = 10;
            settings.paging = true;
            settings.dom = 'Bfrtip';
            settings.searching = false;
            settings.ordering = false;

            settings.lengthMenu = [5, 10, 15];

            settings.language = {
                "url": "https://cdn.datatables.net/plug-ins/1.10.25/i18n/Turkish.json"
            };

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