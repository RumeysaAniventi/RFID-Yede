
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});



function jsSiparisBileseniEkle() {

    var v_zAufnr = $('#txtsiparisno').val();
    var v_zMatnr = $('#txtmalzemekodu').val();
    var v_zLgort = $('#txtdepoyeri').val();
    var v_zWerks = $('#txturetimyeri').val();
    var v_zBdmng = $('#txtihtiyacmiktari').val();
    var v_zVornr = $('#txtislemnumrasi').val();
    var v_zWempf = $('#txteslimalan').val();

//Ekrandaki değerleri alıp ekleme yapsın diye
    $.ajax({
        type: "POST",
        url: "api/SiparisBileseniEkle",//Controllerda ki uzantı
        data: JSON.stringify
            ({
                zAufnr: v_zAufnr,
                zVornr: v_zVornr,
                zMatnr: v_zMatnr,
                zWerks: v_zWerks,
                zBdmng: v_zBdmng,
                zLgort: v_zLgort,
                zWempf: v_zWempf,
                zdeger: '1'
            }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {//çağırılmadan önce çağırılan 

        },
        error: function (request, status, error) {//hata olduğunda 

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {//başarılı olduğunda

            if (msg.zSonuc == "1") {
                swal({
                    buttons: {
                        confirm: "TAMAM"
                    },
                    title: "DİKKAT",
                    text: msg.zMessage,
                    dangerMode: false
                })
                    .then((willDelete) => {
                        $('#m_modal_1').modal('hide');
                    });
              
            }
            else {
                UyariMesajiVer(msg.zMessage);
                
            }

        },
        complete: function () {//Fonksiyon sonlandığında çalışan
        }
    });

    //Hataya düşerse message ekrana yazsın diye

   

}

function jsAcıkPmSiparisListele() {

    var v_zMatnr = $('#txtmalzemekodu').val();

    var v_Liste = $('#listeSiparis');

    $.ajax({
        type: "POST",
        url: "api/AcıkPmSiparisListele",
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
