<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="kimliklendirmeRapor.aspx.cs" Inherits="YedekMalzeme.Arayuz.kimliklendirmeRapor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style id="jsbin-css">
        @media (min-width: 768px) {
            .modal-xl {
                width: 90%;
                max-width: 1200px;
            }
        }

        .dataTables_filter {
            display: none;
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
                                <h3 class="m-portlet__head-text">Kimliklendirme Rapor
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <div class="m-portlet__body">


                            <div class="form-group m-form__group row">
                                <div class="col-lg-4">
                                    <h6> İlk Tarih</h6>
                             <%--       <input type="date" autocomplete="off" class="form-control m-input" name="listekimliktarih" id="listekimliktarih" />--%>
                                      <input type="date" autocomplete="off" class="form-control m-input" name="ilktarih" id="ilktarih" />
                                  
                          <%--  <select class="form-control m-input m-input--square" id="listekimliktarih"></select>--%>
                                </div>
                                 <div class="col-lg-4">
                                    <h6>Son Tarih</h6>
                                     <input type="date" autocomplete="off" class="form-control m-input" name="sontarih" id="sontarih" />
                                </div>

                                <div class="col-lg-4">
                                    <h6>Kimliklendiren</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtkimliklendiren" id="txtkimliklendiren" />

                                </div>


                            </div>

                            <div class="form-group m-form__group row">

                                <div class="col-lg-4">
                                    <h6>Malzeme Kodu</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtmatnr" id="txtmatnr" />

                                </div>

                                <div class="col-lg-4">
                                    <h6>Malzeme Adı</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtmaktx" id="txtmaktx" />

                                </div>

                                <div class="col-lg-2">
                                    <h6>&nbsp;</h6>
                                    <button type="button" class="btn btn-success pull-right" onclick="jsFiltreleme()">LİSTELE</button>
                                </div>

                                <div class="col-lg-2">
                                    <h6>&nbsp;</h6>
                                    <button type="button" class="btn btn-success pull-right" onclick="fn_DegerleriListele()">TÜMÜNÜ LİSTELE</button>
                                </div>

                            </div>


                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary" style="display: none;">
                                        <thead>
                                            <tr>

                                                <th style="text-align: center">Malzeme Kodu</th>
                                                <th style="text-align: center">Malzeme Adı</th>
                                                <th style="text-align: center">Kimlikli Malzeme Sayısı</th>
                                                <th style="text-align: center"></th>

                                            </tr>
                                        </thead>
                                        <tbody></tbody>
                                    </table>
                                </div>
                            </div>


                           


                        </div>
                    </form>

                    <!--end::Form-->
                </div>

                <!--end::Portlet-->
            </div>
        </div>


        <!--begin::Modal-->
        <div class="modal fade" data-backdrop="static" id="m_modal_1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Sipariş Bileşenleri</h5>
                        <img src="resimler/close.png" data-dismiss="modal" class="close" />

                    </div>
                    <div class="modal-body">


                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <table name="m_table" id="m_table" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">RFID</th>
                                            <th style="text-align: center">Kimliklendirme Tarihi</th>
                                            <th style="text-align: center">Kimliklendiren</th>
                                            <th style="text-align: center">Malzeme Kodu</th>
                                            <th style="text-align: center">Malzeme Adı</th>
                                            <th style="text-align: center">Seri No</th>

                                        </tr>
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
                            <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">KAPAT</button>
                            <%--<button type="button" class="btn btn-success pull-right" onclick="jsBilgiGuncelle()">GÜNCELLE</button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end::Modal-->


    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="theme/assets/vendors/custom/datatables/datatables.bundle.js"></script>

    <script src="theme/assets/demo/custom/crud/datatables/basic/paging1.js"></script>

    <script type="text/javascript" src="javascript/jskimliklendirmerapor.js"></script>


</asp:Content>
