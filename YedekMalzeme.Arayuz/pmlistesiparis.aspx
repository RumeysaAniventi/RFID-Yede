<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pmlistesiparis.aspx.cs" Inherits="YedekMalzeme.Arayuz.pmlistesiparis" %>

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
                                <h3 class="m-portlet__head-text">Açık PM Siparişleri Listesi
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <div class="m-portlet__body">

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">
                                    <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">Sipariş Numarası</th>
                                                <th style="text-align: center">Tarih</th>
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
                                <table name="m_table_bilesen" id="m_table_bilesen" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">Malzeme Kodu</th>
                                            <th style="text-align: center">Malzeme Adı</th>
                                            <th style="text-align: center">Adet</th>
                                            <th style="text-align: center">Birim</th>
                                            <th style="text-align: center">İşlem Türü</th>
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


        <!--begin::Modal Malzeme Ekle Çıkar-->
        <div class="modal fade" data-backdrop="static" id="m_modal_2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleMoxdalLabel">Malzeme Ekle / Çıkar </h5>
                        <img src="resimler/close.png" data-dismiss="modal" class="close" />
                    </div>
                    <div class="modal-body">

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Sipariş Numarası</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtsiparisno" id="txtsiparisno" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row" style="display: none;">
                            <div class="col-lg-12">
                                <h6>Etiket Değeri</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtepcdegeri" id="txtepcdegeri" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Malzeme Kodu</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtkod" id="txtkod" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Malzeme Adı</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtad" id="txtad" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Seri Numarası</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtseri" id="txtseri" />
                                <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txteklecikar" id="txteklecikar" style=" display:none" />
                            
                            </div>
                        </div>

                        <div class="form-group m-form__group row" >
                            <div class="col-lg-3">
                                <a href="#" id ="divCikar" onclick="fn_BilesenEkle('-1');" class="btn btn-outline-danger m-btn m-btn--icon">
                                    <span>
                                        <i class="fa flaticon-delete-1"></i>
                                        <span>Bileşen Çıkar</span>
                                    </span>
                                </a>
                            </div>

                            <div class="col-lg-6"></div>

                            <div class="col-lg-3" >
                                <a href="#" id ="divEkle" onclick="fn_BilesenEkle('1');" class="btn btn-outline-success m-btn m-btn--icon pull-right">
                                    <span>
                                        <i class="fa flaticon-add"></i>
                                        <span>Bileşen Ekle</span>
                                    </span>
                                </a>
                            </div>
                        </div>

                    </div>






                    <div class="modal-footer">
                        <div class="col-lg-2">
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>--%>
                        </div>
                        <div class="col-lg-10">
                            <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>--%>
                            <%-- <button type="button" class="btn btn-success pull-right" data-dismiss="modal">Kapat</button>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--end::Modal Malzeme Ekle Çıkar-->



    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="theme/assets/vendors/custom/datatables/datatables.bundle.js"></script>

    <script src="theme/assets/demo/custom/crud/datatables/basic/paging1.js"></script>

    <script type="text/javascript" src="javascript/jspmlistesiparis.js"></script>
</asp:Content>
