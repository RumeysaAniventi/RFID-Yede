function deneme() {
    $.ajax({
        type: "GET",
        url: "Deneme.asmx/HelloWorld",
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



        },

        complete: function () {


        }
    });
}