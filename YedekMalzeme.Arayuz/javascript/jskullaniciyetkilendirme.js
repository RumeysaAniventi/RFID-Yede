function js_Tamamla() {
    var v_zepc = $('#txtepcdegeri').val();
    var v_zmatnr = $('#txtkod').val();
    var v_zmaktx = $('#txtad').val();
    var v_zsernr = $('#listeserino').val();

    if (v_zepc.length != 24) {
        swal({
            buttons: {
                confirm: "TAMAM"
            },
            title: "UYARI",
            html: true,
            text: "Etiket değeri bulunamadı",
            icon: "warning",
            dangerMode: true
        })
            .then((willDelete) => {
                return false;
            });

        return false;
    }

    else {
        if (v_zmatnr.length < 5) {
            swal({
                buttons: {
                    confirm: "TAMAM"
                },
                title: "UYARI",
                html: true,
                text: "Malzeme kodu bulunamadı. Lütfen malzeme seçimi yapınız",
                icon: "warning",
                dangerMode: true
            })
                .then((willDelete) => {
                    return false;
                });

            return false;
        }
        else {
            if (v_zmaktx.length < 5) {
                swal({
                    buttons: {
                        confirm: "TAMAM"
                    },
                    title: "UYARI",
                    html: true,
                    text: "Malzeme adı bulunamadı. Lütfen malzeme seçimi yapınız",
                    icon: "warning",
                    dangerMode: true
                })
                    .then((willDelete) => {
                        return false;
                    });

                return false;
            }
            else {
                if (v_zsernr == '-1') {
                    swal({
                        buttons: {
                            confirm: "TAMAM"
                        },
                        title: "UYARI",
                        html: true,
                        text: "Lütfen seri numarası seçiniz",
                        icon: "warning",
                        dangerMode: true
                    })
                        .then((willDelete) => {
                            $('#listeserino').focus();
                            return false;
                        });

                    return false;
                }
            }
        }
    }



    swal({
        buttons: {
            cancel: "Vazgeç",
            confirm: "TAMAM"
        },
        title: "UYARI",
        html: true,
        text: "Kimliklendirme işlemine devam etmek istiyor musunuz",
        icon: "info",
        dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {

                $.ajax({
                    type: "POST",
                    url: "api/KimlikKimliklendir",
                    data: JSON.stringify
                        ({
                            zdeger: '1',
                            zepc: v_zepc,
                            zmatnr: v_zmatnr,
                            zmaktx: v_zmaktx,
                            zsernr: v_zsernr,
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
                                text: "Kimliklendirme işlemi başarıyla tamamlandı",
                                icon: "success",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    window.location.href = 'kimliklendirmev2.aspx';
                                });
                        }
                        else {

                            swal({
                                buttons: {
                                    confirm: "TAMAM"
                                },
                                title: "Hata",
                                text: "İşlem sırasında bir hata oluştu :" + msg.zAciklama,
                                icon: "error",
                                dangerMode: false
                            })
                                .then((willDelete) => {
                                    window.location.href = 'kimliklendirmev2.aspx';
                                });

                            //UyariMesajiVer('Sistemsel bir hata oluştu');
                        }

                    },
                    complete: function () {


                    }
                });


                //

            }

        });
}