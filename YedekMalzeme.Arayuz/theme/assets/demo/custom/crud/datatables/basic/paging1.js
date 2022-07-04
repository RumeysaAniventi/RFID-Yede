var DatatablesBasicPaginations = function() {

    var initTable1 = function() {
        var table = $('#tabloVeriler');

        // begin first table
        table.DataTable({
            paging: true
        });
    };

    return {

        //main function to initiate the module
        init: function() {
            initTable1();
        },

    };

}();

jQuery(document).ready(function() {
   // DatatablesBasicPaginations.init();
});