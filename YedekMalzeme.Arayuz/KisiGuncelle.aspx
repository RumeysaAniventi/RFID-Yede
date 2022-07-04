<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="KisiGuncelle.aspx.cs" Inherits="YedekMalzeme.Arayuz.KisiGuncelle" %>

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
                                            <div class="input-group m-input-group">
                                                <input type="text" autocomplete="off" class="form-control m-input" name="txtSifre" id="txtSifre" />
                                                <div class="input-group-prepend"><span class="input-group-text" "><i class="la 	la-eye" id="goz" onclick="togglePassword()"></i></span></div>

                                            </div>

                                        </div>


                                    </div>
                              
                                    <div class="m-form__group form-group row">
                                    <div class="col-lg-8">
                                        <h6>&nbsp;</h6>
                                        <button type="button" class="btn btn-success pull-right" onclick="jsKisiJontrolEt()">GÜNCELLE</button>
                                    </div>
                                </div>

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
                            <h6>Sifre</h6>
                            <input type="text" autocomplete="off" class="form-control m-input" name="txtGuncelSifre" id="txtGuncelSifre" />
                        </div>
                    </div>


                </div>
                <div class="modal-footer">
                    <div class="col-lg-2">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">KAPAT</button>
                    </div>
                    <div class="col-lg-10">
                        <%--<button type="button" class="btn btn-secondary" data-dismiss="modal">Kapat</button>--%>
                        <button href="login.aspx" type="button" class="btn btn-success pull-right" id="GuncelleButton" runat="server" onclick="jsKullaniciGuncelle(),CallAspxPage();">GÜNCELLE</button>



                    </div>
                </div>
            </div>
        </div>
    </div>

    </div>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript" src="javascript/jsKisiGuncelle.js"></script>
</asp:Content>
