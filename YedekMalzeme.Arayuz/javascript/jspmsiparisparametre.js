$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});


$(document).ready(function () {

    fn_DegerleriListele();

});

function jsBilgiGuncelle()
{

    var v_zerdatlow = $('#txtERDAT_LOW').val();
    var v_zerdathigh = $('#txtERDAT_HIGH').val();
    var v_ziwerk = $('#txtIWERK').val();
    var v_zaufnr = $('#txtAUFNR').val();



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
                    url: "api/PMParametreKayit",
                    data: JSON.stringify
                        ({
                            zerdatlow: v_zerdatlow,
                            zerdathigh: v_zerdathigh,
                            ziwerk: v_ziwerk,
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


function jsListele()
{

    $.ajax({
        type: "POST",
        url: "api/PMParametreDeger",
        data: JSON.stringify
            ({
                zdeger: '1'
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

                $('#txtERDAT_LOW').val(msg.zerdatlow);
                $('#txtERDAT_HIGH').val(msg.zerdathigh);
                $('#txtIWERK').val(msg.ziwerk);
                $('#txtAUFNR').val(msg.zaufnr);

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

function fn_DegerleriListele()
{
    $.ajax({
        type: "POST",
        url: "api/PMParametreListesi",
        data: JSON.stringify
            ({
                zdeger:'1'
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