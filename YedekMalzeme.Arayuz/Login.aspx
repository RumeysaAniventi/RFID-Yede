<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="YedekMalzeme.Arayuz.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<!-- begin::Head -->
<head runat="server">
    <meta charset="utf-8" />
    <title>Abdi İbrahim </title>
    <meta name="description" content="Portlet tools examples" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, shrink-to-fit=no" />

    <!--begin::Web font -->
    <script src="https://ajax.googleapis.com/ajax/libs/webfont/1.6.16/webfont.js"></script>
    <script>
        WebFont.load({
            google: { "families": ["Poppins:300,400,500,600,700", "Roboto:300,400,500,600,700"] },
            active: function () {
                sessionStorage.fonts = true;
            }
        });
    </script>

    <!--end::Web font -->

    <!--begin:: Global Mandatory Vendors -->
    <link href="theme/vendors/perfect-scrollbar/css/perfect-scrollbar.css" rel="stylesheet" type="text/css" />

    <!--end:: Global Mandatory Vendors -->

    <!--begin:: Global Optional Vendors -->
    <link href="theme/vendors/tether/dist/css/tether.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-datepicker/dist/css/bootstrap-datepicker3.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-datetime-picker/css/bootstrap-datetimepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-timepicker/css/bootstrap-timepicker.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-switch/dist/css/bootstrap3/bootstrap-switch.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-select/dist/css/bootstrap-select.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/select2/dist/css/select2.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/nouislider/distribute/nouislider.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/owl.carousel/dist/assets/owl.carousel.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/owl.carousel/dist/assets/owl.theme.default.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/ion-rangeslider/css/ion.rangeSlider.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/ion-rangeslider/css/ion.rangeSlider.skinFlat.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/dropzone/dist/dropzone.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/summernote/dist/summernote.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/bootstrap-markdown/css/bootstrap-markdown.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/animate.css/animate.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/toastr/build/toastr.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/jstree/dist/themes/default/style.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/morris.js/morris.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/chartist/dist/chartist.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/sweetalert2/dist/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/socicon/css/socicon.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/vendors/line-awesome/css/line-awesome.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/vendors/flaticon/css/flaticon.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/vendors/metronic/css/styles.css" rel="stylesheet" type="text/css" />
    <link href="theme/vendors/vendors/fontawesome5/css/all.min.css" rel="stylesheet" type="text/css" />

    <!--end:: Global Optional Vendors -->

    <!--begin::Global Theme Styles -->
    <link href="theme/assets/demo/base/style.bundle.css" rel="stylesheet" type="text/css" />

    <!--RTL version:<link href="../../assets/demo/base/style.bundle.rtl.css" rel="stylesheet" type="text/css" />-->

    <!--end::Global Theme Styles -->
    <link rel="shortcut icon" href="theme/assets/demo/media/img/logo/icon.ico" />
</head>

<!-- end::Head -->

<!-- begin::Body -->
<body class="m-page--fluid m--skin- m-content--skin-light2 m-header--fixed m-header--fixed-mobile m-aside-left--enabled m-aside-left--skin-dark m-aside-left--fixed m-aside-left--offcanvas m-footer--push m-aside--offcanvas-default">

    <form runat="server">
       
    <!-- begin:: Page -->
        <div class="m-grid m-grid--hor m-grid--root m-page">



            <!-- begin::Body -->
            <div class="m-grid__item m-grid__item--fluid m-grid m-grid--ver-desktop m-grid--desktop m-body">
                <div class="m-grid__item m-grid__item--fluid m-wrapper">
                    <div class="m-content">
                        <div class="row">
                            <div class="col-lg-4 offset-lg-3">
                                <!--begin::Portlet-->
                                <div class="m-portlet m-portlet--primary m-portlet--head-solid-bg m-portlet--head-sm" m-portlet="true" id="m_portlet_tools_2">
                                    <div class="m-portlet__head">
                                        <div class="m-portlet__head-caption">
                                            <div class="m-portlet__head-title">
                                                <span class="m-portlet__head-icon">
                                                    <i class="flaticon-lock"></i>
                                                </span>
                                                <h3 class="m-portlet__head-text">Giriş
                                                </h3>
                                            </div>
                                        </div>
                                        <div class="m-portlet__head-tools">
                                            <ul class="m-portlet__nav">
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="m-portlet__body">
                                        <div class="form-group m-form__group">
                                            <asp:TextBox ID="txtUserName"  autocomplete="off" class="form-control m-input" placeholder="Kullanıcı Adı" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group m-form__group">
                                            <asp:TextBox ID="txtPassword" TextMode="Password" autocomplete="off" class="form-control m-input" placeholder="Şifre Adı" runat="server"></asp:TextBox>
                                        </div>

                                        <div class="form-group m-form__group float-right">
                                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" class="btn btn-primary" Text="Giriş" />                                   
                                        </div>
                                        <div class="form-group m-form__group">
                                            &nbsp;
                                        </div>

                                         <div class="form-group m-form__group">
                                             <asp:Label ID="lblSonuc" runat="server" ForeColor="Red" Font-Bold="true" Text="&nbsp;"></asp:Label>
                                         </div>

                                    </div>
                                </div>
                                <!--end::Portlet-->
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <br />
            <br />
            <br />


            <!-- end:: Body -->


        </div>
        <!-- end:: Page -->


    </form>




    <!--begin:: Global Mandatory Vendors -->
    <script src="theme/vendors/jquery/dist/jquery.js" type="text/javascript"></script>
    <script src="theme/vendors/popper.js/dist/umd/popper.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap/dist/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="theme/vendors/js-cookie/src/js.cookie.js" type="text/javascript"></script>
    <script src="theme/vendors/moment/min/moment.min.js" type="text/javascript"></script>
    <script src="theme/vendors/tooltip.js/dist/umd/tooltip.min.js" type="text/javascript"></script>
    <script src="theme/vendors/perfect-scrollbar/dist/perfect-scrollbar.js" type="text/javascript"></script>
    <script src="theme/vendors/wnumb/wNumb.js" type="text/javascript"></script>

    <!--end:: Global Mandatory Vendors -->

    <!--begin:: Global Optional Vendors -->
    <script src="theme/vendors/jquery.repeater/src/lib.js" type="text/javascript"></script>
    <script src="theme/vendors/jquery.repeater/src/jquery.input.js" type="text/javascript"></script>
    <script src="theme/vendors/jquery.repeater/src/repeater.js" type="text/javascript"></script>
    <script src="theme/vendors/jquery-form/dist/jquery.form.min.js" type="text/javascript"></script>
    <script src="theme/vendors/block-ui/jquery.blockUI.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-datepicker/dist/js/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/forms/bootstrap-datepicker.init.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-datetime-picker/js/bootstrap-datetimepicker.min.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-timepicker/js/bootstrap-timepicker.min.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/forms/bootstrap-timepicker.init.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-daterangepicker/daterangepicker.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/forms/bootstrap-daterangepicker.init.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-touchspin/dist/jquery.bootstrap-touchspin.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-maxlength/src/bootstrap-maxlength.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-switch/dist/js/bootstrap-switch.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/forms/bootstrap-switch.init.js" type="text/javascript"></script>
    <script src="theme/vendors/vendors/bootstrap-multiselectsplitter/bootstrap-multiselectsplitter.min.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-select/dist/js/bootstrap-select.js" type="text/javascript"></script>
    <script src="theme/vendors/select2/dist/js/select2.full.js" type="text/javascript"></script>
    <script src="theme/vendors/typeahead.js/dist/typeahead.bundle.js" type="text/javascript"></script>
    <script src="theme/vendors/handlebars/dist/handlebars.js" type="text/javascript"></script>
    <script src="theme/vendors/inputmask/dist/jquery.inputmask.bundle.js" type="text/javascript"></script>
    <script src="theme/vendors/inputmask/dist/inputmask/inputmask.date.extensions.js" type="text/javascript"></script>
    <script src="theme/vendors/inputmask/dist/inputmask/inputmask.numeric.extensions.js" type="text/javascript"></script>
    <script src="theme/vendors/inputmask/dist/inputmask/inputmask.phone.extensions.js" type="text/javascript"></script>
    <script src="theme/vendors/nouislider/distribute/nouislider.js" type="text/javascript"></script>
    <script src="theme/vendors/owl.carousel/dist/owl.carousel.js" type="text/javascript"></script>
    <script src="theme/vendors/autosize/dist/autosize.js" type="text/javascript"></script>
    <script src="theme/vendors/clipboard/dist/clipboard.min.js" type="text/javascript"></script>
    <script src="theme/vendors/ion-rangeslider/js/ion.rangeSlider.js" type="text/javascript"></script>
    <script src="theme/vendors/dropzone/dist/dropzone.js" type="text/javascript"></script>
    <script src="theme/vendors/summernote/dist/summernote.js" type="text/javascript"></script>
    <script src="theme/vendors/markdown/lib/markdown.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-markdown/js/bootstrap-markdown.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/forms/bootstrap-markdown.init.js" type="text/javascript"></script>
    <script src="theme/vendors/jquery-validation/dist/jquery.validate.js" type="text/javascript"></script>
    <script src="theme/vendors/jquery-validation/dist/additional-methods.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/forms/jquery-validation.init.js" type="text/javascript"></script>
    <script src="theme/vendors/bootstrap-notify/bootstrap-notify.min.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/base/bootstrap-notify.init.js" type="text/javascript"></script>
    <script src="theme/vendors/toastr/build/toastr.min.js" type="text/javascript"></script>
    <script src="theme/vendors/jstree/dist/jstree.js" type="text/javascript"></script>
    <script src="theme/vendors/raphael/raphael.js" type="text/javascript"></script>
    <script src="theme/vendors/morris.js/morris.js" type="text/javascript"></script>
    <script src="theme/vendors/chartist/dist/chartist.js" type="text/javascript"></script>
    <script src="theme/vendors/chart.js/dist/Chart.bundle.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/charts/chart.init.js" type="text/javascript"></script>
    <script src="theme/vendors/vendors/bootstrap-session-timeout/dist/bootstrap-session-timeout.min.js" type="text/javascript"></script>
    <script src="theme/vendors/vendors/jquery-idletimer/idle-timer.min.js" type="text/javascript"></script>
    <script src="theme/vendors/waypoints/lib/jquery.waypoints.js" type="text/javascript"></script>
    <script src="theme/vendors/counterup/jquery.counterup.js" type="text/javascript"></script>
    <script src="theme/vendors/es6-promise-polyfill/promise.min.js" type="text/javascript"></script>
    <script src="theme/vendors/sweetalert2/dist/sweetalert2.min.js" type="text/javascript"></script>
    <script src="theme/vendors/js/framework/components/plugins/base/sweetalert2.init.js" type="text/javascript"></script>

    <!--end:: Global Optional Vendors -->

    <!--begin::Global Theme Bundle -->
    <script src="theme/assets/demo/base/scripts.bundle.js" type="text/javascript"></script>

    <!--end::Global Theme Bundle -->

    <!--begin::Page Scripts -->
    <script src="theme/assets/demo/custom/components/portlets/tools.js" type="text/javascript"></script>

    <script src="Javascript/sweetalert.min.js" type="text/javascript"></script>



    <script src="Javascript/GenelIslemler.js" type="text/javascript"></script>
    <!--end::Page Scripts -->
</body>

<!-- end::Body -->

</html>
