
//GÜNCELLE BUTONU
function jsSiparisGuncelle()
{
    var v_zAufnr = $('#txtsiparisno').val();
    var v_zVornr = $('#txtVornr').val();
    var v_zMatnr = $('#txtMatnr').val();
    var v_zWerks = $('#txtWerks').val();
    var v_zLgort = $('#txtLgort').val();
    var v_zPostp = $('#txtPostp').val();
    var v_zBdmng = $('#txtBdmng').val();
    var v_zMeins = $('#txtMeins').val();
    var v_zWempf = $('#txtWempf').val();
    var v_zAblad = $('#txtAblad').val();
    var v_zRgekz = $('#txtRgekz').val();

    $.ajax({
        type: "POST",
        url: "api/SiparisGuncelle",
        data: JSON.stringify
            ({

                zAufnr: v_zAufnr,
                zVornr: v_zVornr,
                zMatnr: v_zMatnr,
                zWerks: v_zWerks,
                zLgort: v_zLgort,
                zPostp: v_zPostp,
                zBdmng: v_zBdmng,
                zMeins: v_zMeins,
                zWempf: v_zWempf,
                zAblad: v_zAblad,
                zRgekz:v_zRgekz,
                

            }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        beforeSend: function () {
            //   debugger;
        },
        error: function (request, status, error) {
            //debugger;
            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {
            //debugger;
            if (msg.zSonuc == "1") {
                swal({
                    buttons: {
                        confirm: "TAMAM"
                    },
                    title: "İşlem Tamamlandı",
                    //text: msg.zMessage,
                    icon: "success",
                    dangerMode: false
                })
                   
            }
            else {
                UyariMesajiVer('Güncelleme işlemi yapılmadı');
            }
        },
        complete: function () {
            $('#m_modal_1').modal('show');
        }
    });
}

function jsAcıkPmSiparisListele() {

    var v_zAufnr = $('#txtmalzemekodu').val();

    var v_Liste = $('#listeMalzeme');

    $.ajax({
        type: "POST",
        url: "api/SiparisAraListele",
        data: JSON.stringify
            ({
                zAufnr: v_zAufnr
                
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
                v_Liste.html(msg.ListeYazisi);
            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu');
            }

        },
        complete: function () {
        }
    });
}

//LİSTELE BUTONU
function jsSiparistekiMelzemeleriListele() {

    
    var v_zAufnr = $('#txtsiparisno').val();
    var v_zIwerk = $('#txturetimyeri').val();

    var v_Matnr = $('#txtMatnr');
    //var v_Message = $('#listeMalzeme');

    $.ajax({
        type: "POST",
        url: "api/SiparistekiMalzemeleriGetir",
        data: JSON.stringify
            ({
                
                zAufnr: v_zAufnr,
                zIwerk: v_zIwerk
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
                v_Matnr.html(msg.zListeYazisi);
            //    BasariliIslem(zMessage);
            }
            else {

                UyariMesajiVer(zMessage);
            }

        },
        complete: function () {
        }
    });
}