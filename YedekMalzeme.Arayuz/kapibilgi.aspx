<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="kapibilgi.aspx.cs" Inherits="YedekMalzeme.Arayuz.kapibilgi" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style id="jsbin-css">
        @media (min-width: 768px) {
            .modal-xl {
                width: 90%;
                max-width: 1200px;
            }
        }

        .column_filter {
            border: white solid 2px;
            background-color: white;
            margin-bottom: 1px;
            font-size: medium;
            outline: none;
        }


        #m_table_1_filter {
            visibility: hidden;
        }

        #filtreBaslik {
            text-align: center;
        }
    </style>

    <div class="m-content">
        <!--begin::YeniUrunKayit-->
        <div class="row">

            <div class="col-md-12">
                <!--begin::Portlet-->
                <div class="m-portlet m-portlet--tab">
                    <div class="m-portlet__head">
                        <div class="m-portlet__head-caption">
                            <div class="m-portlet__head-title">
                                <span class="m-portlet__head-icon m--hide">
                                    <i class="la la-gear"></i>
                                </span>
                                <h3 class="m-portlet__head-text">Kapı Okuma Hareketleri
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">


                        <div class="col-lg-12">
                            <table class="table   table-hover  dataTable  dtr-inline ">
                                <tbody>
                                    <tr id="filter_col2" data-column="1">
                                        <td align="center">Okuma Başlangıç 
                                               
                                            <input align="center" type="text" class="column_filter" style="padding: 0px!important;" id="col1_filter"></td>
                                        <tr id="filter_col3" data-column="2">
                                            <td align="center">Okuma Bitiş
                                                   
                                                <input type="text" class="column_filter" id="col2_filter"></td>
                                            <td align="center"></td>
                                        </tr>
                                        <tr id="filter_col4" data-column="3">
                                            <td align="center">Sipariş Numarası 
                                                   
                                                <input type="text" class="column_filter" id="col3_filter"></td>

                                        </tr>
                                        <tr id="filter_col5" data-column="4">
                                            <td align="center">Malzeme Kodu 
                                                   
                                                <input type="text" class="column_filter" id="col4_filter"></td>

                                        </tr>

                                        <tr id="filter_col6" data-column="5">
                                            <td align="center">Malzeme Adı 
                                                   
                                                <input type="text" class="column_filter" id="col5_filter"></td>

                                        </tr>

                                        <tr id="filter_col7" data-column="6">
                                            <td align="center">Geçiş İzni 
                                                   
                                                <input type="text" class="column_filter" id="col6_filter"></td>

                                        </tr>

                                        <tr id="filter_col8" data-column="7">
                                            <td align="center">Alarm Durumu  
                                                   
                                                <input type="text" class="column_filter" id="col7_filter"></td>

                                        </tr>
                                </tbody>
                            </table>

                        </div>
                        <div class="col-lg-12">
                            <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                <thead>
                                    <tr>
                                        <%--<tr class="filters"><th rowspan="1" colspan="1" style="width: 148.175px;">
                                                      <input type="text" placeholder="Okuma Başlangı"></th><th rowspan="1" colspan="1" style="width: 114.488px;">
                                                          <input type="text" placeholder="Okuma Bitiş"></th><th rowspan="1" colspan="1" style="width: 56.8563px;">
                                                              <input type="text" placeholder="Malzeme Kodu" title="61"></th><th rowspan="1" colspan="1" style="width: 103.425px;">
                                                                  <input type="text" placeholder="Seri No"></th><th rowspan="1" colspan="1" style="width: 90.8187px;">
                                                                   <input type="text" placeholder="Geçiş İzni"></th><th rowspan="1" colspan="1" style="width: 90.8187px;">
                                                                  
                                                                      <input type="text" placeholder="Alarm Türü"></th></tr>

                                            <tr>--%>

                                        <th style="text-align: center">Etiket No</th>
                                        <th style="text-align: center">Okuma Başlangıç</th>
                                        <th style="text-align: center">Okuma Bitiş</th>
                                        <th style="text-align: center">Sipariş No</th>
                                        <th style="text-align: center">Malzeme Kodu</th>
                                        <th style="text-align: center">Malzeme Adı</th>
                                        <th style="text-align: center">Geçiş İzni</th>
                                        <th style="text-align: center">Alarm Durumu</th>
                                        <th style="text-align: center"></th>
                                    </tr>


                                </thead>
                                <tbody>
                                </tbody>

                                <tfoot>
                                </tfoot>
                            </table>
                        </div>

                        <%--                            </div>
                            --%>
                    </form>
                </div>




                <!--end::Form-->
            </div>

            <div class="modal fade" data-backdrop="static" id="m_modal_1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
                <div class="modal-dialog modal-lg" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Alarm  Durumu</h5>
                            <img src="resimler/close.png" data-dismiss="modal" class="close" />

                        </div>
                        <div class="modal-body">


                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <table name="m_table_bilesen" id="m_table_bilesen" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                        <thead>
                                            <%-- <tr>
                                            
                                            
                                            <th style="text-align: center">Alarm Durumu</th>
                                            <th style="text-align: center">Malzeme Kodu</th>
                                            <th style="text-align: center">Malzeme Adı</th>
                                            <th style="text-align: center">Seri No</th>
                                            <th style="text-align: center">Etiket Değeri</th>

                                        </tr>--%>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>


                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-2">
                                <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>--%>
                            </div>
                            <div class="col-lg-10">
                                <%--     <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">KAPAT</button>--%>
                                <%--<button type="button" class="btn btn-success pull-right" onclick="jsBilgiGuncelle()">GÜNCELLE</button>--%>
                            </div>
                        </div>

                    </div>
                </div>
            </div>


            <div class="modal fade" data-backdrop="static" id="m_modal_2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="exampleModalLabel">Sipariş Bilgileri Güncelle</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">×</span>
                            </button>
                        </div>
                        <div class="modal-body">


                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <h6>Pm Sipariş</h6>
                                    <select class="form-control m-input m-input--square" id="listePmSiparis"></select>
                                </div>
                            </div>

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <h6>Etiket No</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtepc" id="txtepc" />
                                </div>
                            </div>

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <h6>Malzeme Kodu</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtmatnr" id="txtmatnr" />
                                </div>
                            </div>

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <h6>Malzeme Adı</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtmaktx" id="txtmaktx" />
                                </div>
                            </div>

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <h6>Seri No</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtsernr" id="txtsernr" />
                                </div>
                            </div>



                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-2">
                                <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>
                            </div>
                            <div class="col-lg-10">
                                <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>--%>
                                <button type="button" class="btn btn-success pull-right" onclick="jsSipariseBilesenEkle()">Bileşen Ekle</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="theme/assets/vendors/custom/datatables/datatables.bundle.js"></script>

    <script src="theme/assets/demo/custom/crud/datatables/basic/paging1.js"></script>


    <script type="text/javascript" src="javascript/kapibilgijs.js"></script>


    <%-- <script>$(document).ready(function () {
            table = $('#m_table_1').DataTable({
                initComplete: function () {
                    this.api()
                        .columns()
                        .every(function () {
                            var column = this;
                            var select = $('<select><option value=""></option></select>')
                                .appendTo($(column.footer()).empty())
                                .on('change', function () {
                                    var val = $.fn.dataTable.util.escapeRegex($(this).val());

                                    column.search(val ? '^' + val + '$' : '', true, false).draw();
                                });

                            column
                                .data()
                                .unique()
                                .sort()
                                .each(function (d, j) {
                                    select.append('<option value="' + d + '">' + d + '</option>');
                                });
                        });

                },
            });
            table.destroy();
        });--%>

    <%--</script>--%>

    <%--  <script type="text/javascript" src="javascript/jskapibilgi.js"></script>
    --%>
    <%--<script src="https://code.jquery.com/jquery-3.5.1.js"> </script>

    <script src="https://cdn.datatables.net/1.12.1/js/jquery.dataTables.min.js"></script>
    --%>

    <script>
        
        function filterColumn(i) {

            $('#m_table_1')
                .DataTable()
                .column(i)
                .search(
                    $('#col' + i + '_filter').val()
                )
                .draw()
                ;   
        }

        $('input.column_filter').on('keyup click', function () {
            filterColumn($(this).parents('tr').attr('data-column'));

        });


    </script>

    <script>

       
        //$(document).ready(function () {
        //  //  var table = $('#m_table_1').DataTable();
        //     if ($.fn.dataTable.isDataTable('#m_table_1')) {
        //        table = $('#m_table_1').DataTable();

        //        table.clear();
        //        table.destroy();

        //    }
            
        //    // Event listener to the two date filtering inputs to redraw on input

           
        //});

    </script>


</asp:Content>
