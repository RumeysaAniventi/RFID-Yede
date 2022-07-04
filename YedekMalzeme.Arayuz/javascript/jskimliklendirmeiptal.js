
$(document).ready(function () {

    fn_KimlikIptalEpcGetir();
    
});




function fn_KimlikIptalMalzemeGetir() {

    var v_gelenepc = $('#txtepcdegeri').val();

    $.ajax({
        type: "POST",
        url: "api/KimlikIptalMalzemeGetir",
        data: JSON.stringify
            ({
                zdeger: '1',
                zepc: v_gelenepc
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
                $('#txtsernr').val(msg.zsernr);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {
            

        }
    });
}

function js_Iptal() {
    var v_zepc = $('#txtepcdegeri').val();
    var v_zmatnr = $('#txtkod').val();
    var v_zmatkx = $('#txtad').val();
    var v_zsernr = $('#txtsernr').val();

    swal({
        buttons: {
            cancel: "HAYIR",
            confirm: "EVET"
        },
        title: "UYARI",
        html: true,
        text: "Kimliklendirme İptal Edilsin Mi?",
        icon: "warning",
        dangerMode: true
    })
        .then((willDelete) => {



            if (willDelete) {

               

                $.ajax({
                    type: "POST",
                    url: "api/KimlikMalzemeIptal",
                    data: JSON.stringify
                        ({
                            zdeger: "1",
                            zepc: v_zepc,
                            zmatnr: v_zmatnr,
                            zmaktx: v_zmatkx,
                            zsernr: v_zsernr
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
                                text: "Kimliklendirme İptal Başarı ile Gerçekleştirildi..",
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

function fn_KimlikIptalEpcGetir() {

    

    $.ajax({
        type: "POST",
        url: "api/KimlikIptalEpcGetir",
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
            setTimeout("fn_KimlikIptalEpcGetir()", 5000);
            

            
            fn_KimlikIptalMalzemeGetir();   

        }
    });
}

