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
        url: "api/FiltreliKapiListesi",
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
                                <a class="dropdown-item" href="#" onclick="jsAlarmGoruntule('`+ full[0] + `','` + full[3] + `','` + full[4] + `');"><i class="la la-bell-o"></i>Alarm Durumu</a>
                             
                        
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