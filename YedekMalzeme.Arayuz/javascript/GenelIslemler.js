$body = $("body");



function getParameterByName(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, '\\$&');
    var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, ' '));
}

function BasariliIslem(varMesaj) {
    $.notify({
        title: 'BAŞARILI',
        message: '' + varMesaj
    }, {
            type: 'success',
            delay: 1500,
            z_index: 2000
        });
}


function UyariMesajiVer(varMesaj) {
    $.notify({
        title: 'HATA!!!',
        message: '' + varMesaj
    }, {
            type: 'danger',
            delay: 1500,
            z_index: 2000
        });
}

function RaporSayfasiAc(varSayfaAdi) {
    window.open(varSayfaAdi, 'width=800,height=600,toolbar=no,scrollbars=yes,resizable=yes,top=0,left=0');
}