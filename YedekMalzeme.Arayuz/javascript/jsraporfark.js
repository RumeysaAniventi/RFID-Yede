$(document).on({

    ajaxStart: function () { $body.addClass("loading"); },
    ajaxStop: function () { $body.removeClass("loading"); }
});

$(document).ready(function () {

    fn_DegerleriListele();
});

function fn_DegerleriListele() {

   

    $.ajax({
        type: "POST",
        url: "api/RfidFarklistesi",
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
                        content += `<td style="text-align: center; display:none">" + vYanitDizi[iSayac].zid + "</td>`;                       
                        content += `<td style="text-align: center;">` + vYanitDizi[iSayac].zaufnr + `</td>`;
                        content += `<td style="text-align: center;">` + vYanitDizi[iSayac].zrfidcount + `</td>`;
                        content += `<td style="text-align: center;">` + vYanitDizi[iSayac].zsapcount + `</td>`;
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
            settings.pageLength = 50;
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
                              <div style="display: flex; justify-content: center;">
                                <button type="button" onclick="fn_RfidGoruntule('` + full[1] +`')" class="btn m-btn--pill" style="background-color:#282733; color:#bfc0c1; width:150px;">
                                       <span>
										<i class="fa fa-info-circle"></i>
											<span><b>İncele</b></span>
										</span>
                                    
                                </button>
                              </div>
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

function fn_RfidGoruntule(siparisno) {
    
    $.ajax({
        type: "POST",
        url: "api/BilesenMalzemeListesi",
        data: JSON.stringify
            ({
                zdeger: '1',
                zaufnr: siparisno
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

                var v_TabloYazisi = "";

                for (var iSayac = 0; iSayac < vYanitDizi.length; iSayac++) {
                    v_TabloYazisi += "<tr>";
                    v_TabloYazisi += "<td>" + vYanitDizi[iSayac].zkullanici+"</td>";
                    v_TabloYazisi += "<td>" + vYanitDizi[iSayac].zmatnr + "</td>";
                    v_TabloYazisi += "<td>" + vYanitDizi[iSayac].zmaktx + "</td>";
                    v_TabloYazisi += "<td>" + vYanitDizi[iSayac].zsernr + "</td>";
                    v_TabloYazisi += "</tr>";
                }

                $('#m_table_bilesen_rfid tbody').html(v_TabloYazisi);
            }
            else {
                UyariMesajiVer('Hata ' + msg.zAciklama);
            }
        },

        complete: function () {

          fn_Sap_Goruntule(siparisno);
        }
    });
}

function fn_Sap_Goruntule(siparisno) {
    $.ajax({
        type: "POST",
        url: "api/SiparisMalzemeListesi",
        data: JSON.stringify
            ({
                zdeger: '1',
                zaufnr: siparisno
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

                $('#m_table_bilesen_sap tbody').html(msg.ztabloyazisi);

            }
            else {
                UyariMesajiVer('HTN3 Sistemsel bir hata oluştu. Lütfen daha sonra tekrar deneyiniz');
            }
        },

        complete: function () {

            $('#m_modal_3').modal('show');
        }
    });
}

