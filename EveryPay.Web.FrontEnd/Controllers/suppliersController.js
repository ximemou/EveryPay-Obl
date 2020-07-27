(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('suppliersController', function ($scope, $http, $rootScope, $location, $compile, GlobalService, SupplierService, toaster, $timeout) {

            $scope.errorMessage = "";
            $scope.allSupplierFieldsInArray = [];
            $scope.SupplierFieldsJson = [];

            function inputsSupplierFields()
            {
                $('#supplierFieldsInput .form-control').each(function () {
                    $scope.allSupplierFieldsInArray.push($(this).val());
                });
            }

            function createSupplierFieldsJson()
            {
                for (var i = 0; i < $scope.allSupplierFieldsInArray.length - 1; i = i + 2) {

                    var supplierFieldInPositionIAsJson = {
                        "fieldName": $scope.allSupplierFieldsInArray[i],
                        "typeOfField": $scope.allSupplierFieldsInArray[i + 1]
                    };

                    $scope.SupplierFieldsJson.push(supplierFieldInPositionIAsJson);
                }
            }

            $scope.AddSupplier = function () {
                
                $scope.errorMessage = "";
                inputsSupplierFields();
                createSupplierFieldsJson();
                $scope.SupplierFieldsJson.splice($scope.SupplierFieldsJson.length - 1, 1);

                var supplierFieldsAsJson = JSON.stringify($scope.SupplierFieldsJson);

                var response = SupplierService.ValidateSupplier(supplierFieldsAsJson)
                .success(function (data) {

                    $scope.allSupplierFieldsInArray = [];
                    $scope.SupplierFieldsJson = [];

                    var responseCreateSupplier = SupplierService.CreateSupplier($scope.Name, $scope.Commission)
                    .success(function (data) {

                        $scope.insertedSupplierId = parseInt(data["message"]);
                        InsertFieldsToSupplier($scope.insertedSupplierId);

                    }).error(function (data, status) {

                        $scope.errorMessage = data["message"];
                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: data["message"],
                            timeout: 3000
                        });


                        $timeout(function () {
                            $location.path('/adminUsers');
                        }, 2000);



                        $scope.allSupplierFieldsInArray = [];
                        $scope.SupplierFieldsJson = [];

                    })

                }).error(function (data, status) {


                    $scope.errorMessage = data["message"];
                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: data["message"],
                        timeout: 3000
                    });

                    $scope.allSupplierFieldsInArray = [];
                    $scope.SupplierFieldsJson = [];
                });
               
            }

            function  InsertFieldsToSupplier (supplierId)
            {

                inputsSupplierFields();
                createSupplierFieldsJson();
                $scope.SupplierFieldsJson.splice($scope.SupplierFieldsJson.length - 1, 1);

                var supplierFieldsAsJson = JSON.stringify($scope.SupplierFieldsJson);

                var response = SupplierService.InsertFieldsToSupplier(supplierId, supplierFieldsAsJson)
                .success(function (data) {

                    toaster.pop({
                        type: 'success',
                        title: 'Ok',
                        body: data["message"],
                        timeout: 3000
                    });
                    $scope.allSupplierFieldsInArray = [];
                    $scope.SupplierFieldsJson = [];
                   
                });
            }

            $scope.handButtonEvent = function () {

                var $template = $('#FieldsTemplate'),
                $clone = $template
                            .clone()
                            .removeClass('hide')
                            .insertBefore($template);     

                $compile($clone)($scope);   
            }
       

            $scope.GetAllSuppliers = function()
            {
                var responseGetSuppliers = SupplierService.GetAllSuppliers()
                .success(function (data) {

                    if (data.length > 0) {
                        $scope.AllSuppliers = data;
                    }
                    else {
                        $scope.errorMessage = "No existen usuarios en el sistema";

                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body:"No existen usuarios en el sistema",
                            timeout: 3000
                        });
                    }

                });
            }

            $scope.deleteSupplier = function (supplierId)
            {
                
                var responseDeleteSupplier = SupplierService.DeleteSupplier(supplierId)
                .success(function (data) {

                    toaster.pop({
                        type: 'success',
                        title: 'Ok',
                        body: data["message"],
                        timeout: 3000
                    });


                    $location.path('/adminSuppliers');

                });
            }


        });

})();

