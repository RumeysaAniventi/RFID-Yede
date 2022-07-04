<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="kullaniciYetkilendirme1.aspx.cs" Inherits="YedekMalzeme.Arayuz.kullaniciYetkilendirme1" %>

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
                                <h3 class="m-portlet__head-text">Kullanıcı Bilgileri
                                </h3>
                            </div>
                        </div>
                    </div>

                    <!--begin::Form-->
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <div class="m-portlet__body">

                            <div class="form-group m-form__group row">
                                <div class="col-lg-12">



                                    <div class="form-group m-form__group row">
                                        <div class="col-lg-8">
                                            <h6>AD</h6>
                                            <input type="text" autocomplete="off" class="form-control m-input" name="txtAdi" id="txtAdi" />
                                        </div>
                                    </div>

                                    <div class="form-group m-form__group row">
                                        <div class="col-lg-8">
                                            <h6>SOYADI</h6>
                                            <input type="text" autocomplete="off" class="form-control m-input" name="txtSoyAdi" id="txtSoyAdi" />
                                        </div>
                                    </div>

                                    <div class="form-group m-form__group row">
                                        <div class="col-lg-8">
                                            <h6>KULLANICI ADI</h6>
                                            <input type="text" autocomplete="off" class="form-control m-input" name="txtkullaniciAdi" id="txtkullaniciAdi" />
                                        </div>
                                    </div>

                                    <div class="form-group m-form__group row">
                                        <div class="col-lg-8">
                                            <h6>ŞİFRE</h6>
                                            <input type="text" autocomplete="off" class="form-control m-input" name="txtSifre" id="txtSifre" />
                                        </div>
                                    </div>

                                    <div class="form-group m-form__group row">
                                        <div class="col-lg-8">
                                            <h6>YETKİ</h6>
                                            <select type="select" autocomplete="off" class="form-control m-input" name="selectYetki" id="selectYetki">
                                                <option value="0" selected>YETKİ SEÇİNİZ</option>
                                                <option value="1">ADMİN</option>
                                                <option value="2">KULLANICI</option>
                                            </select>

                                        </div>
                                    </div>

                                    <div class="m-form__group form-group row">
                                        <div class="col-lg-8">
                                            <h6>&nbsp;</h6>
                                            <button type="button" class="btn btn-success pull-right" onclick="jsKisiKaydet()">KULLANICI  EKLE</button>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-lg-12">
                                    <h6>&nbsp;</h6>

                                </div>

                                <div class="col-lg-12">
                                    <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center; display: none;">ID</th>
                                                <th style="text-align: center">AD</th>
                                                <th style="text-align: center">SOYAD</th>
                                                <th style="text-align: center">KULLANICI ADI</th>
                                                <th style="text-align: center">YETKİ</th>
                                                <%--  <th style="text-align: center">GÜNCELLE</th>
                                                <th style="text-align: center">  SİL </th>
                                                --%>
                                                <th></th>

                                            </tr>
                                        </thead>
                                        <tbody>
                                        </tbody>
                                        <tfoot>
                                        </tfoot>

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
        <!--end::Guncelle MODAL-->
        <div class="modal fade" data-backdrop="static" id="m_modal_2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true" style="display: none;">
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
                               <input type="text" autocomplete="off" class="form-control m-input" readonly="readonly" name="txtid" id="txtid" style ="display:none"/>
                            </div>
                        </div>
                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>AD</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtGuncelAd" id="txtGuncelAd" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>SOYAD</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtGuncelSoyAd" id="txtGuncelSoyAd" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>KULLANICI ADI</h6>
                                <input type="text" autocomplete="off" class="form-control m-input" name="txtGuncelKullaniciAdi" id="txtGuncelKullaniciAdi" />
                            </div>
                        </div>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <h6>Yetki</h6>
                               <select type="select" autocomplete="off" class="form-control m-input" name="selectGuncelYetki" id="selectGuncelYetki">
                                                <option value="0" selected>YETKİ SEÇİNİZ</option>
                                                <option value="1">ADMİN</option>
                                                <option value="2">KULLANICI</option>
                                            </select>
                            </div>
                        </div>


                    </div>
                    <div class="modal-footer">
                        <div class="col-lg-2">
                            <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>
                        </div>
                        <div class="col-lg-10">
                            <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>--%>
                            <button type="button" class="btn btn-success pull-right" id="GuncelleButton" runat="server" onclick="JsGuncelle();">GÜNCELLE</button>



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
    <script type="text/javascript" src="javascript/jskullaniciYetkilendirme1.js"></script>

</asp:Content>
