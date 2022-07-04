$(document).on({

    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
    
    
});

$(document).ready(function () {

    jsKimlikTarihListele(); 
    
});
    

function jsFiltreleme(){
   // var v_gelenlistekimliktarih = $('#listekimliktarih').val();
    var v_gelenilktarih = $('#ilktarih').val();
    var v_gelensontarih = $('#sontarih').val();
    var v_gelenkimliklendiren = $('#txtkimliklendiren').val();
    var v_gelenmalzemekod = $('#txtmatnr').val();
    var v_gelenmalzemead = $('#txtmaktx').val();
   

    // zkimliktarih: v_gelenlistekimliktarih,

    $('#m_table_1').css('display', 'inline-table');
    $.ajax({
        type: "POST",
        url: "api/KimlikRaporFiltreleme",
        data: JSON.stringify
            ({
                zdeger:'1',
                zilktarih: v_gelenilktarih,
                zsontarih: v_gelensontarih,
                zkimliklendiren: v_gelenkimliklendiren,
                zmatnr: v_gelenmalzemekod,
                zmaktx: v_gelenmalzemead

            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            if ($.fn.dataTable.isDataTable('#m_table_1')) {
                table = $('#m_table_1').DataTable();

                table.clear();
                
                table.destroy();
            }


        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {



            if (msg.zSonuc == "1") {

                var vYanitDizi = msg.zDizi;
                var content = '';

                if (vYanitDizi.length != 0) {


                    for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {


                        content += "<tr>";
                        content += "<td style='text-align:right'>" + vYanitDizi[iSayac].zmatnr + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zkimliklimalzemesayisi + "</td>";
                        content += "<td> </td>";
                        content += "</tr>";
                    }

                    $('#m_table_1 tbody').html(content);

                }

                else {
                swal({
                    buttons: {
                        confirm: "TAMAM"
                    },
                    title: "Filtreleme Yapılamadı",
                    text: "Girdiğiniz Kriterlere Uygun Kayıt Bulunamamıştır..",
                    icon: "error",
                    dangerMode: false
                })
                    .then((willDelete) => {
                        $('#m_modal_1').modal('hide');
                        });
                }

                


            }


            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            var v_Yazi = 1;

            var settings = {};
            settings.pageLength = 10;
            settings.paging = true;
            settings.dom = 'Bfrtip';
            settings.searching = true;
            settings.ordering = true;
            settings.lengthMenu = [5, 10, 15];
            
            
            
            
            

            settings.language = {

                "url": "https://cdn.datatables.net/plug-ins/1.10.25/i18n/Turkish.json"
    
            },

                settings.columnDefs = [
                    {
                        targets: -1,
                        data: 'name',
                        title: '',
                        orderable: false,
                        render: function (data, type, full, meta) {



                            return `
                        <span class="dropdown" style='cursor:pointer !important;'>
                            <a href="#" style='cursor:pointer !important;' class="btn m-btn m-btn--hover-success m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="#" onclick="jsBilesenGoruntule1('`+ full[0] + `');"><i class="la la-edit"></i>Ayrıntıları Görüntüle</a>
                                
                        
                            </div>
                        </span>
                        `;
                        }
                    }],


                settings.buttons = [
                    //{
                    //    extend: 'pdf',
                    //    customize: function (doc) {
                    //        doc.content[1].table.widths =
                    //            Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                    //    }
                    //},

                    //{
                    //    extend: 'excel',
                    //    exportOptions: {
                    //        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    //    }
                    //}
                ];

            $('#m_table_1').DataTable(settings);
        }
    });
}

function jsKimlikTarihListele() {

    var v_gelenlistekimliktarih = $('#listekimliktarih')

    $.ajax({
        type: "POST",
        url: "api/KimlikTarihListele",
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

                v_gelenlistekimliktarih.html(msg.ztabloyazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

          
        }
    });
}

function jsBilesenGoruntule(v_matnr) {
    $.ajax({
        type: "POST",
        url: "api/KimliklendirilmisMalzemeListele",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmatnr: v_matnr
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

                $('#m_table tbody').html(msg.ztabloyazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            $('#m_modal_1').modal('show');
        }
    });
}


function jsBilesenGoruntule1(v_matnr) {

   
    var v_gelenilktarih = $('#ilktarih').val();
    var v_gelensontarih = $('#sontarih').val();
    var v_gelenkimliklendiren = $('#txtkimliklendiren').val();

    $.ajax({
        type: "POST",
        url: "api/FiltrelenmisKimlikMalzemeListele",
        data: JSON.stringify
            ({
                zdeger: '1',
                zmatnr: v_matnr,
                zsontarih: v_gelensontarih,
                zilktarih: v_gelenilktarih,
                zkimliklendiren: v_gelenkimliklendiren
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

                var vYanitDizi = msg.zDizi;
                var content = '';

                for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {


                    content += "<tr>";
                    content += "<td style='text-align:right'>" + vYanitDizi[iSayac].zepc + "</td>";
                    content += "<td>" + vYanitDizi[iSayac].zkimliktarih + "</td>";
                    content += "<td>" + vYanitDizi[iSayac].zkimliklendiren + "</td>";
                    content += "<td>" + vYanitDizi[iSayac].zmatnr + "</td>";
                    content += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";
                    content += "<td>" + vYanitDizi[iSayac].zsern + "</td>";
                    content += "</tr>";
                }

                $('#m_table tbody').html(content);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            $('#m_modal_1').modal('show');
        }
    });

}



function fn_DegerleriListele() {
    $('#m_table_1').css('display', 'inline-table');
    $.ajax({
        type: "POST",
        url: "api/KimliklendirmeRaporListele",
        data: JSON.stringify
            ({
                zdeger: '1',
                zdepo: '0'
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

            if ($.fn.dataTable.isDataTable('#m_table_1')) {
                table = $('#m_table_1').DataTable();

                table.clear();
                table.destroy();
            }

        },
        error: function (request, status, error) {

            UyariMesajiVer('Sistemsel bir hata oluştu');
        },
        success: function (msg) {



            if (msg.zSonuc == "1") {

                $('#m_table_1 tbody').html(msg.zListeYazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            var v_Yazi = 1;

            var settings = {};
            settings.pageLength = 10;
            settings.paging = true;
            settings.dom = 'Bfrtip';
            settings.searching = true;
            settings.ordering = true;
            settings.lengthMenu = [5, 10, 15];

            settings.language = {
                "url": "https://cdn.datatables.net/plug-ins/1.10.25/i18n/Turkish.json"
            },

                settings.columnDefs = [
                    {
                        targets: -1,
                        data: 'name',
                        title: '',
                        orderable: false,
                        render: function (data, type, full, meta) {



                            return `
                        <span class="dropdown" style='cursor:pointer !important;'>
                            <a href="#" style='cursor:pointer !important;' class="btn m-btn m-btn--hover-success m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="#" onclick="jsBilesenGoruntule('`+ full[0] + `');"><i class="la la-edit"></i>Ayrıntıları Görüntüle</a>
                                
                        
                            </div>
                        </span>
                        `;
                        }
                    }],


                settings.buttons = [
                    //{
                    //    extend: 'pdf',
                    //    customize: function (doc) {
                    //        doc.content[1].table.widths =
                    //            Array(doc.content[1].table.body[0].length + 1).join('*').split('');
                    //    }
                    //},

                    //{
                    //    extend: 'excel',
                    //    exportOptions: {
                    //        columns: [0, 1, 2, 3, 4, 5, 6, 7]
                    //    }
                    //}
                ];

            $('#m_table_1').DataTable(settings);
        }
    });
}


//function getMalzemeListesi() {
//    let malzemeListesiArray = [];
//    $.get("Deneme.asmx/HelloWorld")
 
//    malzemeListesiArray = json.parse(sonuc);
 
//    malzemeListesiArray.filter()

//}