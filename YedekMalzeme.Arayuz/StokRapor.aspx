<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StokRapor.aspx.cs" Inherits="YedekMalzeme.Arayuz.StokRapor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    


    <div class="m-content">
        <!--begin::YeniUrunKayit-->
        <div class="row">
            <div class="col-md-12 p-0">

                <form>
                    <div class="m-portlet">
                        <div class="m-portlet__head">
                            <div class="m-portlet__head-caption">
                                <div class="m-portlet__head-title">
                                    <span class="m-portlet__head-icon">
                                        <i class="la la-leaf"></i>
                                    </span>
                                    <h3 class="m-portlet__head-text">Stoke Raporu
                                    </h3>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="m-portlet__body">
                                <div class="m-pricing-table-1">
                                    <div class="m-pricing-table-1__items row">

                                        <div class="m-pricing-table-1__item col-lg-3">
                                            <div class="m-pricing-table-1__visual">
                                                <div class="m-pricing-table-1__hexagon1"></div>
                                                <div class="m-pricing-table-1__hexagon2"></div>
                                                <span class="m-pricing-table-1__icon m--font-primary"><i class="fa flaticon-home-1"></i></span>
                                            </div>

                                            <span class="m-pricing-table-1__price" id="depoCount" style="margin-bottom: 0px; font-size: 3rem;"></span>
                                            <h2 class="m-pricing-table-1__subtitle">Depodaki Ürün</h2>

                                            <div class="m-pricing-table-1__btn">
                                                <button type="button" class="btn m-btn--pill  btn-primary m-btn--wide m-btn--uppercase m-btn--bolder m-btn--sm" onclick="fn_DepoListele()">Listele</button>
                                            </div>
                                        </div>

                                        <div class="m-pricing-table-1__item col-lg-3">
                                            <div class="m-pricing-table-1__visual">
                                                <div class="m-pricing-table-1__hexagon1"></div>
                                                <div class="m-pricing-table-1__hexagon2"></div>
                                                <span class="m-pricing-table-1__icon m--font-accent"><i class="fa flaticon-clock-2"></i></span>
                                            </div>

                                            <span class="m-pricing-table-1__price" id="koltukdepoCount" style="margin-top:290px;"></span>
                                            <h2 class="m-pricing-table-1__subtitle">Koltuk Depoda Ürün</h2>

                                            <div class="m-pricing-table-1__btn">
                                                <button type="button" class="btn m-btn--pill  btn-accent m-btn--wide m-btn--uppercase m-btn--bolder m-btn--sm" onclick="fn_KoltukDepoListele()">Listele</button>
                                            </div>
                                        </div>

                                        <div class="m-pricing-table-1__item col-lg-3">
                                            <div class="m-pricing-table-1__visual">
                                                <div class="m-pricing-table-1__hexagon1"></div>
                                                <div class="m-pricing-table-1__hexagon2"></div>
                                                <span class="m-pricing-table-1__icon m--font-focus"><i class="fa flaticon-rocket"></i></span>
                                            </div>
                                            <span class="m-pricing-table-1__price" id="tüketimCount" style="margin-top:290px";></span>
                                            <h2 class="m-pricing-table-1__subtitle">Tüketilen Ürün</h2>

                                            <div class="m-pricing-table-1__btn">
                                                <button type="button" class="btn m-btn--pill  btn-focus m-btn--wide m-btn--uppercase m-btn--bolder m-btn--sm" onclick="fn_TuketimListele()">Listele</button>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>

                </form>
                <!--end::Form-->
                <div class="modal fade" data-backdrop="static" id="m_modal_1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel">Depodaki Ürünler</h5>
                                <img src="resimler/close.png" data-dismiss="modal" class="close" />

                            </div>
                            <div class="modal-body">


                                <div class="form-group m-form__group row">
                                    <div class="col-lg-12">
                                        <table name="m_table_depo" id="m_table_depo" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center">Seri No</th>
                                                    <th style="text-align: center">Malzeme Kodu</th>
                                                    <th style="text-align: center">Malzeme Adı</th>

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
                                    <%--     <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">KAPAT</button>--%>
                                    <%--<button type="button" class="btn btn-success pull-right" onclick="jsBilgiGuncelle()">GÜNCELLE</button>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="modal fade" data-backdrop="static" id="m_modal_koltuk" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel1">Alarm  Durumu</h5>
                                <img src="resimler/close.png" data-dismiss="modal" class="close" />

                            </div>
                            <div class="modal-body">


                                <div class="form-group m-form__group row">
                                    <div class="col-lg-12">
                                        <table name="m_table_koltukdepo" id="m_table_koltukdepo" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center">Sipariş Numaraso</th>
                                                    <th style="text-align: center">Seri No</th>
                                                    <th style="text-align: center">Malzeme Kodu</th>
                                                    <th style="text-align: center">Malzeme Adı</th>

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
                                    <%--     <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">KAPAT</button>--%>
                                    <%--<button type="button" class="btn btn-success pull-right" onclick="jsBilgiGuncelle()">GÜNCELLE</button>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <div class="modal fade" data-backdrop="static" id="m_modal_tuketim" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
                    <div class="modal-dialog modal-lg" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h5 class="modal-title" id="exampleModalLabel2">Alarm  Durumu</h5>
                                <img src="resimler/close.png" data-dismiss="modal" class="close" />

                            </div>
                            <div class="modal-body">


                                <div class="form-group m-form__group row">
                                    <div class="col-lg-12">
                                        <table name="m_table_koltukdepo" id="m_table_tuketim" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                            <thead>
                                                <tr>
                                                    <th style="text-align: center">Sipariş Numaraso</th>
                                                    <th style="text-align: center">Seri No</th>
                                                    <th style="text-align: center">Malzeme Kodu</th>
                                                    <th style="text-align: center">Malzeme Adı</th>

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
                                    <%--     <button type="button" class="btn btn-danger pull-right" data-dismiss="modal">KAPAT</button>--%>
                                    <%--<button type="button" class="btn btn-success pull-right" onclick="jsBilgiGuncelle()">GÜNCELLE</button>--%>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>

                <!--end::Modal-->

            </div>

        </div>
    </div>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="javascript/jsStokRapor.js"></script>

</asp:Content>
