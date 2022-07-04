function jsListele() {
    var v_zMatnr = $('#txtmalzemekodu').val();

    var v_Liste = $('#listeMalzeme');

    $.ajax({
        type: "POST",
        url: "api/MalzemeAraListele2",
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