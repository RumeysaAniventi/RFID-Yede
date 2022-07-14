<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="raporfark.aspx.cs" Inherits="YedekMalzeme.Arayuz.raporfark" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


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
                                <h3 class="m-portlet__head-text">Rfid ve Sap Fark Raporu
                                </h3>
                            </div>
                        </div>
                    </div>
                    <form class="m-form m-form--fit m-form--label-align-right">
                        <%-- filtre kısmı --%>
                        <%--<div class="m-portlet__body">

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
                                    <input type="date" autocomplete="off" class="form-control m-input" name="ilktarih" id="ilktarih" />                                   
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
                        </div>--%>
                        <%-- filtre bitişi --%>

                        <div class="form-group m-form__group row">
                            <div class="col-lg-12">
                                <table name="m_table_1" id="m_table_1" class="table table-bordered m-table m-table--border-brand m-table--head-separator-primary">
                                    <thead>

                                        <tr>
                                            <th style="text-align: center;">Id</th>                                            
                                            <th style="text-align: center">Kimlikli Sipariş Sayısı</th>
                                            <th style="text-align: center">Toplam Sipariş Sayısı</th>                                            
                                            <th style="text-align: center"></th>
                                        </tr>

                                    </thead>
                                    <tbody>
                                    </tbody>

                                </table>
                            </div>
                        </div>



                    </form>

                </div>

            </div>
        </div>
    </div>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script src="theme/assets/vendors/custom/datatables/datatables.bundle.js"></script>

    <script src="theme/assets/demo/custom/crud/datatables/basic/paging1.js"></script>

    <script src="javascript/jsraporfark.js"></script>

</asp:Content>
