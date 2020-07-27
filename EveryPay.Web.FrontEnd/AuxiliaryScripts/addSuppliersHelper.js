$(document).ready(function () {

   $('#SupplierFieldsForm')

         //Remove button click handler
        .on('click', '.removeButton', function () {
            var $row = $(this).parents('.form-group');
            $row.remove();
        });
});