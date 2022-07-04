<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="readerkimliklendirmeparam.aspx.cs" Inherits="YedekMalzeme.Arayuz.readerkimliklendirmeparam" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <style id="jsbin-css">
        @media (min-width: 768px) {
            .modal-xl {
                width: 90%;
                max-width: 1200px;
            }
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
                                <h3 class="m-portlet__head-text">Reader Parametre Ayaları
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <div class="m-portlet__body">

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                   <table name="tabloVeriler" id="tabloVeriler" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">Reader IP</th>
                                                <th style="text-align: center">Reader Okuma Gücü</th>
                                                <th style="text-align: center">RFID ID Değeri </th>
                                                <th></th>                                             
                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
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




        <div class="modal fade" data-backdrop="static" id="m_modal_1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Bilgileri Güncelle</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">×</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Reader IP</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtReaderIP" id="txtReaderIP" />
                            </div>
                        </div>

                         <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Reader Okuma Gücü</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtReaderPower" id="txtReaderPower" />
                            </div>
                        </div>

                         <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>RFID ID Değeri</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtRfıdID" id="txtRfıdID" />
                            </div>
                        </div>

                        


                    </div>
                    <div class="modal-footer">
                        <div class="col-lg-2">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>
                        </div>
                        <div class="col-lg-10">
                            <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>--%>
                            <button type="button" class="btn btn-success pull-right" onclick="jsBilgiGuncelle()">GÜNCELLE</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>



        

    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="javascript/jsreaderkimliklendirmeparam.js"></script>
</asp:Content>
