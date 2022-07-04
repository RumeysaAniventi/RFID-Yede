
$(document).on({
    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    fn_BelgeDegerleriListele();

});

function jsmalzemebelgelistesiGuncelle()
{
    var v_zbutdatlow = $('#txtBUDAT_LOW').val();
    var v_zbudathigh = $('#txtBUDAT_HIGH').val();
    var v_ziwerk = $('#txtIWERK').val();
    var v_zaufnr = $('#txtAUFNR').val();

    swal({
        buttons: {
            cancel: "İPTAL",
            confirm: "EVET"
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
                    url: "api/MalzemeBelgeListesiParametreKayit",
                    data: JSON.stringify
                        ({
                            z_BodatLow: v_zbutdatlow,
                            z_BudatHigh: v_zbudathigh,
                            z_Iwerk: v_ziwerk,
                            z_Aufnr: v_zaufnr
                        }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    beforeSend: function () {

                    },
                    error: function (request, status, error) {

                        UyariMesajiVer('Sistemsel bir hata oluştu1');
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
                            UyariMesajiVer('Sistemsel bir hata oluştu1');
                        }

                    },
                    complete: function () {
                        fn_BelgeDegerleriListele();
                    }
                });

            }

        });


}

function jsBelgeListele() {

    $.ajax({
        type: "POST",
        url: "api/MalzemeBelgeListesiParametreDegeri",
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
            UyariMesajiVer('Sistemsel bir hata oluştu2');
        },
        success: function (msg) {
            //debugger;

            if (msg.zSonuc == "1") {

                $('#txtBUDAT_LOW').val(msg.z_BudatLow);
                $('#txtBUDAT_HIGH').val(msg.z_BudatHigh);
                $('#txtIWERK').val(msg.z_Iwerk);
                $('#txtAUFNR').val(msg.z_Aufnr);
                
            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz2');
            }
        },
        complete: function () {
            $('#m_modal_1').modal('show');
        }
    });

}

function fn_BelgeDegerleriListele()
{
    $.ajax({
        type: "POST",
        url: "api/MalzemeBelgeListesiParametreListesi",
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
            UyariMesajiVer('Sistemsel bir hata oluştu3');
        },
        success: function (msg) {
            //debugger;

            if (msg.zSonuc == "1") {

                $('#tabloVeriler > tbody').empty();
                $("#tabloVeriler tbody").append(msg.zTabloYazisi);

            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz3');
            }
        },

        complete: function () {

        }
    });
  
}