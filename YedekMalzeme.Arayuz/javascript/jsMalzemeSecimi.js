$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});


function jsMalzemeSerialListele() {

    var v_zMatnr = $('#listeMalzeme').val();

    var v_Liste = $('#listeUrun');

    $.ajax({
        type: "POST",
        url: "api/MalzemeUrunListele",
        data: JSON.stringify
            ({
                zmatnr: v_zMatnr
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
                v_Liste.html(msg.zListeYazisi);
            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu');
            }

        },
        complete: function () {
        }
    });

}


function jsListele() {

    var v_zMatnr = $('#txtmalzemekodu').val();

    var v_Liste = $('#listeMalzeme');

    $.ajax({
        type: "POST",
        url: "api/MalzemeAraListele",
        data: JSON.stringify
            ({
                zMatnr: v_zMatnr
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
                v_Liste.html(msg.zListeYazisi);
            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu');
            }

        },
        complete: function () {
                    }
    });

}