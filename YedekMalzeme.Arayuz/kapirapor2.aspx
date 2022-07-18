<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="kapirapor2.aspx.cs" Inherits="YedekMalzeme.Arayuz.kapirapor2" %>

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
            <div class="col-md-12 p-0">
                <!--begin::Portlet-->
                <div class="m-portlet m-portlet--tab">
                    <div class="m-portlet__head">
                        <div class="m-portlet__head-caption">
                            <div class="m-portlet__head-title">
                                <span class="m-portlet__head-icon m--hide">
                                    <i class="la la-gear"></i>
                                </span>
                                <h3 class="m-portlet__head-text">Kapı Geçiş Rapor
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <%-- filtre kısmı --%>
                        <div class="m-portlet__body">

                            <div class="form-group m-form__group row">

                                <div class="col-lg-4">
                                    <h6>Sipariş Numarası</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtaufnrfiltre" id="txtaufnrfiltre" />

                                </div>

                                <div class="col-lg-4">
                                    <h6>Malzeme Kodu</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtmatnrfiltre" id="txtmatnrfiltre" />

                                </div>

                                <div class="col-lg-4">
                                    <h6>Malzeme Adı</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" name="txtmaktxfiltre" id="txtmaktxfiltre" />

                                </div>
                            </div>
                            <div class="form-group m-form__group row">
                                <div class="col-lg-4">
                                    <h6>İlk Tarih</h6>
                                    <%--       <input type="date" autocomplete="off" class="form-control m-input" name="listekimliktarih" id="listekimliktarih" />--%>
                                    <input type="date" autocomplete="off" class="form-control m-input" name="ilktarih" id="ilktarih" />

                                    <%--  <select class="form-control m-input m-input--square" id="listekimliktarih"></select>--%>
                                </div>
                                <div class="col-lg-4">
                                    <h6>Son Tarih</h6>
                                    <input type="date" autocomplete="off" class="form-control m-input" name="sontarih" id="sontarih" />
                                </div>
                                <div class="col-lg-2">
                                    <h6>Geçiş İzni</h6>
                                    <select class="form-control m-input m-input--square" id="listegecis">

                                        <option value="2" selected="selected">Seçiniz</option>
                                        <option value="0">İzinsiz Geçiş</option>
                                        <option value="1">İzinli Geçiş</option>
                                    </select>

                                </div>

                                <div class="col-lg-2">
                                    <h6>Alarm Durumu</h6>
                                    <select class="form-control m-input m-input--square" id="istealarm">

                                        <option value="3" selected="selected">Seçiniz</option>
                                        <option value="0">Alarm YOK</option>
                                        <option value="1">Alarm VAR</option>
                                        <option value="2">Alarm KAPATILDI</option>
                                    </select>
                                </div>

                            </div>

                            <div class="form-group m-form__group row" style="padding-top: 0px;">

                                <div>
                                    <button type="button" class="btn btn-success pull-right" style="align-content: center; width: 120px; position: absolute; right: 30px;" onclick="fn_DegerleriListele()">LİSTELE</button>
                                </div>
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                    <thead>

                                        <tr>
                                            <th style="text-align: center; display: none">Id</th>
                                            <th style="text-align: center">Etiket No</th>
                                            <th style="text-align: center">Okuma Başlangıç</th>
                                            <th style="text-align: center">Okuma Bitiş</th>
                                            <th style="text-align: center">Sipariş No</th>
                                            <th style="text-align: center">Seri No</th>
                                            <th style="text-align: center">Malzeme Kodu</th>
                                            <th style="text-align: center">Malzeme Adı</th>
                                            <th style="text-align: center">Geçiş İzni</th>
                                            <th style="text-align: center">Alarm Durumu</th>
                                            <th style="text-align: center"></th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                    </tbody>

                                </table>
                            </div>
                        </div>



                    </form>
                    <!--end::Form-->


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
                                    <h5 class="modal-title" id="exampleModalLabe2">Sipariş Bilgileri Güncelle</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">×</span>
                                    </button>
                                </div>
                                <div class="modal-body">

                                    <div class="form-group m-form__group row">
                                        <div class="col-lg-12">
                                            <span id="lblID" style="display:none"></span>
                                        </div>
                                    </div>

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
                                        <button type="button" class="btn btn-success pull-right" onclick="jsSipariseBilesenEkle()">Bileşen Ekle</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!--end::Modal-->

                </div>

            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script src="theme/assets/vendors/custom/datatables/datatables.bundle.js"></script>

    <script src="theme/assets/demo/custom/crud/datatables/basic/paging1.js"></script>


    <script type="text/javascript" src="javascript/jskapirapor2.js"></script>
</asp:Content>
