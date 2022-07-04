$(document).ready(function () {

    fn_DegerleriListele();

});
function jsStokeGuncelle()
{
    var v_zmtart = $('#txtMTART').val();
    var v_ziwerk = $('#txtIWERK').val();
    var v_zlgort = $('#txtLGORT').val();
        


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
                    url: "api/StokParametreKayit",
                    data: JSON.stringify
                        ({
                            zmtart: v_zmtart,
                            ziwerk: v_ziwerk,
                            zlgort: v_zlgort
                        
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
                            UyariMesajiVer('HTN1 Sistemsel bir hata oluştu');
                        }

                    },
                    complete: function () {
                        fn_DegerleriListele();
                    }
                });

            }

        });


}

   

function jsStokeListele()
{

    $.ajax({
        type: "POST",
        url: "api/StokDeger",
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
              
                $('#txtMTART').val(msg.zmtart);
                $('#txtIWERK').val(msg.ziwerk);
                $('#txtLGORT').val(msg.zlgort);
                
              
            }
            else {
                UyariMesajiVer('HTN2 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
            //debugger;
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
        url: "api/StokeListesi",
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
                $("#tabloVeriler tbody").append(msg.ztabloyazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

        }
    });
}