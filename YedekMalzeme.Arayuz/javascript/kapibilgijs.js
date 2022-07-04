$(document).on({

    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    fn_DegerleriListele();
});

function jsSipariseBilesenEkle() {

    var veps = $('#txtepc').val();
    var vaufnr = $('#listePmSiparis').val();

    $.ajax({
        type: "POST",
        url: "api/SipariseBilesenEkle",
        data: JSON.stringify
            ({
                zdeger: '1',
                zepc: veps,
                zaufnr: vaufnr
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

                BasariliIslem("Sipariş bileşeni eklendi");
            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {
            $('#m_modal_1').hide();
            $('#m_modal_2').hide();
        }
    });
}
function fn_SipariseEkle(v_eps) {

    var v_gelenlistesiparis = $('#listePmSiparis')

    $.ajax({
        type: "POST",
        url: "api/SipariseEkle",
        data: JSON.stringify
            ({
                zdeger: '1',
                zepc: v_eps
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

                v_gelenlistesiparis.html(msg.zaufnrlistesi);
                $('#txtepc').val(msg.zepc);
                $('#txtmatnr').val(msg.zmatnr);
                $('#txtmaktx').val(msg.zmaktx);
                $('#txtsernr').val(msg.zsern);

                $('#m_modal_2').modal({
                    show: true,

                });


            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

           

        }
    });

}

function js_Alarm_Goruntule(v_eps) {
    $.ajax({
        type: "POST",
        url: "api/AlarmGoruntulemek",
        data: JSON.stringify
            ({
                zdeger: '1',
                zeps: v_eps
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
                var vYanitTablo = msg.zTablo;

                var v_BodyYazisi = "";
                var v_HeadYazisi = "";

                for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {

                    v_BodyYazisi += "<tr>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zalarmdurumu + "</td>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zeps + "</td>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zmatnr + "</td>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zsernr + "</td>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zalarmkapatan + "</td>";
                    v_BodyYazisi += "<td>" + vYanitDizi[iSayac].zalrmkapatmatarih + "</td>";
                    v_BodyYazisi += "</tr>";

                }

                for (var kSayac = 0; kSayac < vYanitTablo.length; kSayac++) {

                    v_HeadYazisi += "<tr>";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zalarmdurumu + "</th";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zeps + "</th>";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zmatnr + "</th>";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zmaktx + "</th>";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zsernr + "</th>";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zalarmkapatan + "</th>";
                    v_HeadYazisi += "<th>" + vYanitTablo[kSayac].zalrmkapatmatarih + "</th>";
                    v_HeadYazisi += "</tr>";

                }

                $('#m_table_alarm_kapat tbody').html(v_BodyYazisi);
                $('#m_table_alarm_kapat thead').html(v_HeadYazisi);

                if ($.fn.dataTable.isDataTable('#m_table_alarm_kapat')) {
                    table = $('#m_table_alarm_kapat').DataTable();

                    table.clear();
                    table.destroy();

                }
            }
            else {
                UyariMesajiVer('Hata ' + msg.zAciklama);
            }
        },

        complete: function () {

            $('#m_modal_4').modal('show');
        }
    });
}


function fn_AlarmKapat(v_eps) {

    var v_zaciklama = $('#aciklama').val();

    $.ajax({
        type: "POST",
        url: "api/AlarmKapat",       
        data: JSON.stringify
            ({
                zdeger: '1',
                zeps: v_eps,
                zaciklama: v_zaciklama
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {

          

        },
        error: function (request, status, error) {


        },
        success: function (msg) {



            if (msg.zSonuc == "1") {

                //$('#m_table_bilesen thead').html(msg.ztablobasligi);
                //$('#m_table_bilesen tbody').html(msg.ztabloyazisi);

                BasariliIslem('Alarm Kapatıldı'); 
                $('#m_modal_5').modal({
                    show: true,
                    keyboard: false,
                    backdrop: 'static'
                });
            }
            else {
                UyariMesajiVer('Alarm Kapatılamadı.  ' + msg.zAciklama );
            }
        },

        complete: function () {
          

            $('#m_modal_5').modal({
                show: true,
                keyboard: false,
                backdrop: 'static'
            });

            fn_DegerleriListele();

            
        }
    });
}

//function fn_AlarmKapatAciklama() {

//    var v_zaciklama = $('#txtAlarmAciklama').val();

//    var v_eps = $('#epc').val();


//    $.ajax({
//        type: "POST",
//        url: "api/AlarmKapat",
//        data: JSON.stringify
//            ({
//                zdeger: '1',
//                zeps: v_eps,
//                zaciklama: v_zaciklama
//            }),

//        contentType: "application/json; charset=utf-8",

//        dataType: "json",

//        beforeSend: function () {

//            $('#m_modal_5').modal({
//                show: true,
//                keyboard: false,
//                backdrop: 'static'
//            });

//        },
//        error: function (request, status, error) {


//        },
//        success: function (msg) {



//            if (msg.zSonuc == "1") {

//                //$('#m_table_bilesen thead').html(msg.ztablobasligi);
//                //$('#m_table_bilesen tbody').html(msg.ztabloyazisi);


//            }
//            else {
//                UyariMesajiVer('Alarm Kapatılamadı. Lütfen daha sonra tekrar deneyiniz');
//            }
//        },

//        complete: function () {
//            $('#m_modal_1').modal({
//                show: false,
//                keyboard: false,
//                backdrop: 'static'
//            });

//            fn_DegerleriListele();


//        }
//    });
//}

function jsAlarmGoruntule(v_eps,v_aufnr,v_matnr) {

    $.ajax({
        type: "POST",
        url: "api/AlarmGoruntule",
        data: JSON.stringify
            ({
                zdeger: '1',
                zeps: v_eps,
                zaufnr: v_aufnr,
                zmatnr:v_matnr
               
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

                $('#m_table_bilesen thead').html(msg.ztablobasligi);
                $('#m_table_bilesen tbody').html(msg.ztabloyazisi);

            }

            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {
            $('#m_modal_1').modal({
                show: true,
               
            });

        }
    });
}

function fn_DegerleriListele() {

    $.ajax({
        type: "POST",
        url: "api/KapiReaderListesi",
        data: JSON.stringify
            ({
                zdeger: '1',
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {
            //tabloVeriler
            
          
            if ($.fn.dataTable.isDataTable('#m_table_1')) {
                table = $('#m_table_1').DataTable();
                debugger;
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
                            <a href="#" style='cursor:pointer !important;' class="btn m-btn m-btn--hover-success m-btn--icon m-btn--icon-flaticon-alarm m-btn--pill" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="#" onclick="jsAlarmGoruntule('`+ full[0] + `','` + full[3] + `','`+full[4]+`');"><i class="la la-bell-o"></i>Alarm Durumu</a>
                             
                        
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

function jsTarihFiltre() {

    function toTimestamp(inputDateTime) {
        debugger;
        if (inputDateTime == null || typeof inputDateTime == 'undefined') {
            inputDateTime = new Date();
        }
        return Math.round(new Date(inputDateTime).getTime() / 1000);
    }

    $.fn.dataTable.ext.search.push(

        function (settings, data, dataIndex) {
            debugger;
            var from_date = toTimestamp($('#from_date').val());
            var to_date = toTimestamp($('#to_date').val());
            var start_date = toTimestamp(data[1]);

            var record_found = false;


            if (record_found &&
                ((isNaN(from_date) && isNaN(to_date)) ||
                    (isNaN(from_date) && from_date <= to_date) ||
                    (from_date <= start_date && isNaN(to_date)) ||
                    (from_date <= start_date && start_date <= to_date))) {
                record_found = true;
            }
            else {
                record_found = false;
            }
            return record_found;
        }
    );

    debugger;
    $('#from_date, #to_date').keyup(function () {
        $('#m_table_1').DataTable(settings).draw();
    });
       
}

