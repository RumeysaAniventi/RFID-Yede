$(document).ready(function () {
    fn_KisiGetir();
});
function togglePassword() {
    var element = document.getElementById('txtSifre');
    element.type = (element.type == 'password' ? 'text' : 'password');
}
function jsKullaniciGuncelle() {
    var v_Gad = $('#txtGuncelAd').val();
    var v_Gsoyad = $('#txtGuncelSoyAd').val();
    var v_Gkullaniciadi = $('#txtGuncelKullaniciAdi').val();
    var v_Gsifre = $('#txtGuncelSifre').val();
    $.ajax({
        type: "POST",
        url: "api/KullaniciGuncelle",
        data: JSON.stringify({
            zdeger: '1',
            zad: v_Gad,
            zsoyad: v_Gsoyad,
            zkullanici: v_Gkullaniciadi,
            zsifre: v_Gsifre
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
                $('#m_modal_1').modal('hide');
                BasariliIslem('Kayıt Güncellendi');
            }
            else {
                UyariMesajiVer(msg.zAciklama);
            }
        },
        complete: function () {
            fn_KisiGetir();
        }
    });
}
function jsKisiJontrolEt() {
    var v_ad = $('#txtAdi').val();
    var v_soyad = $('#txtSoyAdi').val();
    var v_kullaniciadi = $('#txtkullaniciAdi').val();
    var v_sifre = $('#txtSifre').val();
    $.ajax({
        type: "POST",
        url: "api/KisiKontrolEt",
        data: JSON.stringify({
            zdeger: '1',
            zad: v_ad,
            zsoyad: v_soyad,
            zkullanici: v_kullaniciadi,
            zsifre: v_sifre
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
                $('#m_modal_1').modal({
                    show: true,
                    keyboard: false,
                    backdrop: 'static'
                });
            }
            else {
                UyariMesajiVer(msg.zAciklama);
            }
        },
        complete: function () {
        }
    });
}
function fn_KisiGetir() {
    $.ajax({
        type: "POST",
        url: "api/KisiGetir",
        data: JSON.stringify({
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
                $('#txtAdi').val(msg.zadi);
                $('#txtSoyAdi').val(msg.zsoyadi);
            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },
        complete: function () {
        }
    });
}
//# sourceMappingURL=jsKisiGuncelle.js.map