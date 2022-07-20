
$(document).on({

    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    fn_StokSay();
});


function fn_DepoListele() {

    $.ajax({
        type: "POST",
        url: "api/DepoListele",
        data: JSON.stringify
            ({
                zdeger: '1'
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            swal({
                buttons: {
                    confirm: "TAMAM"
                },
                title: "UYARI",
                html: true,
                text: "Bu İşlem Uzun Sürebilir",
                icon: "info",
                dangerMode: false
            })
                //.then((willDelete) => {
                //    window.location.href = 'kapirapor2.aspx';
                //});
        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {

            if (msg.zSonuc == "1") {

                var vYanitDizi = msg.zdizi;
                var content = '';

                if (vYanitDizi.length) {

                    for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {


                        content += "<tr>";
                        content += "<td>" + vYanitDizi[iSayac].zsernr + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zmatnr + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";
                        content += "</tr>";
                    }

                    $('#m_table_depo tbody').html(content);


                }

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

function fn_StokSay() {

    $.ajax({
        type: "POST",
        url: "api/StokSay",
        data: JSON.stringify
            ({
                zdeger: '1'
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            swal({
                buttons: {
                    confirm: "TAMAM"
                },
                title: "UYARI",
                html: true,
                text: "Bu İşlem Uzun Sürebilir",
                icon: "info",
                dangerMode: false
            })
            //.then((willDelete) => {
            //    window.location.href = 'kapirapor2.aspx';
            //});
        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {

            if (msg.zSonuc == "1") {


                $('#depoCount').val(msg.zdepo);
                $('#koltukdepoCount').val(msg.zkoltukdepo);
                $('#tüketimCount').val(msg.ztuketim);

            }
            else {
                UyariMesajiVer('Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

        }
    });
}