$(document).on({

    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    fn_DegerleriListele();
});


function fn_AlarmKapat(v_id) {

    var v_zaciklama = $('#aciklama').val();

    $.ajax({
        type: "POST",
        url: "api/AlarmKapat",
        data: JSON.stringify
            ({
                zdeger: '1',
                zid: v_id,
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

                //BasariliIslem('Alarm Kapatıldı');
                //$('#m_modal_5').modal({
                //    show: true,
                //    keyboard: false,
                //    backdrop: 'static'
                //});
                if (msg.zSonuc == "1") {
                    swal({
                        buttons: {
                            confirm: "TAMAM"
                        },
                        title: "İşlem Tamamlandı",
                        html: true,
                        text: "Alarm Kapatıldı",
                        icon: "success",
                        dangerMode: false
                    })
                        .then((willDelete) => {
                            window.location.href = 'kapirapor2.aspx';
                        });
                }
            }
            else {
                UyariMesajiVer('Alarm Kapatılamadı.  ' + msg.zAciklama);
            }
        },

        complete: function () {


            $('#m_modal_1').modal('hidden');

            //$('#m_modal_2').modal('hidden');

           

            window.location.href = 'kapirapor2.aspx';
            //fn_DegerleriListele();


        }
    });
}

function fn_AlarmGoruntule(v_id) {
    

    $.ajax({
        type: "POST",
        url: "api/AlarmDurumGoruntule",
        data: JSON.stringify
            ({
                zdeger: '1',
                zid: v_id,
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

                $('#m_table_bilesen thead').html(msg._TabloBasligi);
                $('#m_table_bilesen tbody').html(msg._TabloYazisi);

            }
            else {
                UyariMesajiVer('Hata ' + msg.zAciklama);
            }

            
        },

        complete: function () {

            $('#m_modal_1').modal('show');
        }
    });
}


function fn_DegerleriListele() {

    var v_gelenilktarih = $('#ilktarih').val();
    var v_gelensontarih = $('#sontarih').val();
    var v_gelensiparisno = $('#txtaufnrfiltre').val();
    var v_gelenmalzemekod = $('#txtmatnrfiltre').val();
    var v_gelenmalzemeadi = $('#txtmaktxfiltre').val();
    var v_gelengecisizni = $('#listegecis').val();
    var v_gelenalarm = $('#istealarm').val();

    $.ajax({
        type: "POST",
        url: "api/FiltreliKapiListesi",
        data: JSON.stringify
            ({
                zdeger: '1',
                zilktarih: v_gelenilktarih,
                zsontarih: v_gelensontarih,
                zaufnr: v_gelensiparisno,
                zmatnr: v_gelenmalzemekod,
                zmaktx: v_gelenmalzemeadi,
                zgecisizni: v_gelengecisizni,
                zalarm: v_gelenalarm
            }),

        contentType: "application/json; charset=utf-8",

        dataType: "json",

        beforeSend: function () {
            //tabloVeriler


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

                var vYanitDizi = msg.zdizi;
                var content = '';

                if (vYanitDizi.length) {

                    for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {


                        content += "<tr>";

                        content += "<td style='display: none'>" + vYanitDizi[iSayac].zid + "</td>";
                        content += "<td >" + vYanitDizi[iSayac].zepc + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zokumabaslangic + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zokumabitis + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zaufnr + "</td>";
                        content += "<td>" + vYanitDizi[iSayac].zsernr + "</td>";                        
                        content += "<td>" + vYanitDizi[iSayac].zmatnr + "</td>";            
                        content += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";

                       
                        if (vYanitDizi[iSayac].zgecisizni == 1)
                        {
                           // content += "<td>" + vYanitDizi[iSayac].zgecisizni + "</td>";
                            content += "<td style='text - align: right'> <img src='resimler/1.png'  title='Resim' > izinli </td> ";
                        }
                        else if (vYanitDizi[iSayac].zgecisizni == 0)
                        {
                            content +=  "<td style='text - align: right'> <img src='resimler/0.png'  title='Resim' > izinsiz </td> ";
                        }

                        if (vYanitDizi[iSayac].zalarm == 0) {
                            content +=  "<td> <span class='m-badge  m-badge--success m-badge--wide'>ALARM YOK</span></td>";
                        }
                        else if (vYanitDizi[iSayac].zalarm == 1) {

                            content +=  "<td> <span class='m-badge  m-badge--danger m-badge--wide'>ALARM VAR</span></td>";
                        }
                        else if (vYanitDizi[iSayac].zalarm == 2) {
                            content += "<td> <span class='m-badge  m-badge--metal  m-badge--wide'>ALARM KAPALI</span></td>";

                        }
                        content += "<td>...</td>";
                        content += "</tr>";
                    }

                    $('#m_table_1 tbody').html(content);

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
                            <a href="#" style='cursor:pointer !important;' class="btn m-btn m-btn--hover-success m-btn--icon m-btn--icon-flaticon-alarm m-btn--pill" data-toggle="dropdown" aria-expanded="true">
                              <i class="la la-ellipsis-h"></i>
                            </a>
                            <div class="dropdown-menu dropdown-menu-right">
                                <a class="dropdown-item" href="#" onclick="fn_AlarmGoruntule('`+full[0]+`');"><i class="la la-bell-o"></i>Alarm Durumu</a>
                             
                        
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

function jsSipariseBilesenEkle() {

    var vid = document.getElementById("lblID").innerText;
    var vaufnr = $('#listePmSiparis').val();

    $.ajax({
        type: "POST",
        url: "api/SipariseBilesenEkle",
        data: JSON.stringify
            ({
                zdeger: '1',
                zid: vid,
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
                swal({
                    buttons: {
                        confirm: "TAMAM"
                    },
                    title: "İşlem Tamamlandı",
                    html: true,
                    text: "Sipariş bileşeni olarak eklendi",
                    icon: "success",
                    dangerMode: false
                })
                    .then((willDelete) => {
                        window.location.href = 'kapirapor2.aspx';
                    });
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

function fn_SipariseEkle(v_id) {

    var v_gelenlistesiparis = $('#listePmSiparis')

    $.ajax({
        type: "POST",
        url: "api/SipariseEkle",
        data: JSON.stringify
            ({
                zdeger: '1',
                zid: v_id
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

               document.getElementById("lblID").innerHTML =msg.zid;
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