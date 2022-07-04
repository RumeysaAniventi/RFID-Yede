$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});


$(document).ready(function () {

    fn_DegerleriListele();

});


function jsMalzemeGuncelle() {

    var v_zmtart = $('#txtMTART').val();
    var v_ziwerk = $('#txtIWERK').val();
    //debugger

    swal({
        buttons: {
            cancel: "İPTAL",
            confirm: "TAMAM"
        },
        title: "UYARI",
        html: true,
        text: "Parametre bilgileri güncellenecek. Devam etmek istiyor musunuz?",
        icon: "warning",
        dangerMode: true
    })
        .then((willDelete) => {



            if (willDelete) {

                // debugger

                $.ajax({
                    type: "POST",
                    url: "api/MalzemeListesiParamKayit",
                    data: JSON.stringify
                        ({

                            zmtart: v_zmtart,
                            ziwerk: v_ziwerk,
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
                                text: "Parametre bilgileri başarı ile kayıt güncellendi",
                                icon: "success",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    $('#m_modal_1').modal('hide');
                                });
                        }
                        else {
                            UyariMesajiVer('Sistemsel bir hata oluştu');
                        }

                    },
                    complete: function () {
                        fn_DegerleriListele();
                    }
                });

            }

        });
}

function jsMalzemeListele() {

    $.ajax({
        type: "POST",
        url: "api/MalzemeListesiParamDeger",
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

                $('#txtMTART').val(msg.zMtart);
                $('#txtIWERK').val(msg.zIwerk);
                UyariMesajiVer(' tekrar deneyiniz');
            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },
        complete: function () {
            $('#m_modal_1').modal('show');
        }
    });

}

function fn_DegerleriListele() {
    $.ajax({
        type: "POST",
        url: "api/MalzemeListesiParamListesi",
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

                $('#tabloVeriler > tbody').empty();
                $("#tabloVeriler tbody").append(msg.zTabloYazisi);

            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

        }
    });
}