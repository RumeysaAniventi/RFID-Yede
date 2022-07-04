<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="kimliklendirmeiptal.aspx.cs" Inherits="YedekMalzeme.Arayuz.kimliklendirmeiptal" %>
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
                                <h3 class="m-portlet__head-text">Kimliklendirme İptal
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <div class="m-portlet__body">

                            <div class="form-group m-form__group row">
                                <div class="col-lg-5">
                                  <h6>Etiket Değeri</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtepcdegeri" id="txtepcdegeri" />
                                </div>

                            </div>


                            <div class="form-group m-form__group row">
                                <div class="col-lg-2">
                                    <h6>Malzeme Kodu</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtkod" id="txtkod" />
                                </div>

                                <div class="col-lg-3">
                                    <h6>Malzeme Adı</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtad" id="txtad" />
                                </div>

                                
                            </div>

                            <div class="form-group m-form__group row">
                                <div class="col-lg-5">
                                    <h6>Seri Numarası</h6>
                                    <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtsernr" id="txtsernr" />
                                </div>

                                <div class="col-lg-1">
                                    <h6>&nbsp;</h6>
                                    <%--<button type="button" class="btn btn-success btn-block" onclick="jsKimliklendirme()">Kaydet</button>--%>


                                    <a href="#" onclick="js_Iptal()" class="btn btn-outline-success m-btn m-btn--icon">
                                        <span>
                                            <i class="la la-calendar-check-o"></i>
                                            <span>İptal</span>
                                        </span>
                                    </a>

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
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title" id="exampleModalLabel">Malzeme Arama </h5>
                        <img src="resimler/close.png" data-dismiss="modal" class="close" />
                    </div>
                    <div class="modal-body">

                        <div class="form-group m-form__group row">
                            <div class="col-lg-5">
                                <h6>Malzeme Kodu</h6>
                                <input type="text" maxlength="7" autocomplete="off" class="form-control m-input" name="txtmalzemekodu" id="txtmalzemekodu" />
                            </div>

                            <div class="col-lg-5">
                                <h6>Malzeme Adı</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtmalzemeadi" id="txtmalzemeadi" />
                            </div>

                            <div class="col-lg-2">
                                <h6>&nbsp;</h6>
                                <button type="button" class="btn btn-success pull-right" onclick="jsMalzemeListesiAra()">Listele</button>
                            </div>
                        </div>


                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                    <thead>
                                        <tr>
                                            <th style="text-align: center">Malzeme Kodu</th>
                                            <th style="text-align: center">Malzeme Adı</th>
                                            <th>İşlem</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                        </div>


                    </div>
                    <div class="modal-footer">
                        <div class="col-lg-2">
                            <%-- <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>--%>
                        </div>
                        <div class="col-lg-10">
                            <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>--%>
                            <button type="button" class="btn btn-success pull-right" data-dismiss="modal">Kapat</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>







    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="javascript/jskimliklendirmeiptal.js"></script>
</asp:Content>
