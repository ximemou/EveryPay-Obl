(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('updateSupplierController', updateSupplierController);

    updateSupplierController.$inject = ['$scope', '$http', '$location', '$rootScope', 'toaster', 'GlobalService', 'SupplierService','$timeout','$compile'];

    function updateSupplierController($scope, $http, $location, $rootScope, toaster, GlobalService, SupplierService, $timeout, $compile) {

        $scope.hideSaveButton = false;

        $scope.allSupplierFieldsInArray = [];
        $scope.SupplierFieldsJson = [];

        $scope.errorModifySupplier = "";

        var selectedSupplier = {};

        var allSupplierInputsInArray = [];

        $scope.show = false;

        $scope.errorMessage = "";

        $scope.SearchAllSuppliers = function () {

            var response = SupplierService.GetAllSuppliers()

           .success(function (data) {

               if (data.length > 0) {
                   $scope.Suppliers = data;


               }
               else {
                   $scope.errorMessage = "No existen proveedores en el sistema";

                   toaster.pop({
                       type: 'error',
                       title: 'Error',
                       body: "No existen proveedores en el sistema",
                       timeout: 3000
                   });
               }

           });
        }

        function ShowSupplierFields(supplierId) {

            var response = SupplierService.GetSupplierFields(supplierId)
                .success(function (data) {


                    $scope.SupplierFields = data;


                    if (data.length > 0) {

                        $scope.supplierFieldError = "";
                    }
                    else {
                        $scope.supplierFieldError = "El proveedor no posee campos particulares";

                        toaster.pop({
                            type: 'error',
                            title: 'Error',
                            body: "El proveedor no posee campos particulares",
                            timeout: 3000
                        });
                    }

                });

        }


        $scope.ShowData = function (supplier) {
            allSupplierInputsInArray = [];
            var divSupplier = document.getElementById(supplier['supplierId']);
            $scope.errorMessage = "";
            $scope.errorAddFieldsMessage = "";
            $scope.hideSaveButton = false;
            if (divSupplier.style.display != 'block') {

                $scope.name = supplier['name'];
                $scope.commission = supplier['commission'];

                ShowSupplierFields(supplier['supplierId']);
                CloseInput(supplier['supplierId']);
                HideInputs($scope.supplier);

                ShowButtons();
                $scope.show = false;
                $scope.supplierSearched = supplier;
                var divSupplier = document.getElementById(supplier['supplierId']);
                divSupplier.style.display = 'block';
                SetReadOnlyProperty();
                $scope.showData = true;



            }
            else {
                HideAllInputs();
                ShowButtons();
                CloseInput(supplier['supplierId']);
                SetReadOnlyProperty();
                document.getElementById("SupplierFieldsForm").reset();
                $scope.show = false;
                

            }
        };


        function ShowButtons() {

            var inputs = document.getElementsByClassName('suuplierNameButton');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'none') {

                    inputs[i].style.display = 'block';

                }

            }
        }


        $scope.EditSupplier=function(supplierId){
            allSupplierInputsInArray = [];
           
            $scope.hideSaveButton = true;

            RemoveReadOnlyProperty();
           
        }

        function RemoveReadOnlyProperty() {

            var inputs = document.getElementsByClassName('supplier-input');
            for (var i = 0; i < inputs.length; i++) {
                inputs[i].readOnly = false;
            }
        }


        function SetReadOnlyProperty() {

            var inputs = document.getElementsByClassName('supplier-input');
            for (var i = 0; i < inputs.length; i++) {
                inputs[i].readOnly = true;
            }

        }


        function CloseInput(supplierId) {
            var inputs = document.getElementsByClassName('supplierDiv');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'block' && inputs[i].id == supplierId){

                    inputs[i].style.display = 'none';

                }

            }

        }


        function HideAllInputs() {
            var inputs = document.getElementsByClassName('supplierDiv');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'block') {

                    inputs[i].style.display = 'none';

                }

            }


        }

     


        function HideInputs(supplierId) {
            $scope.errorMessage = "";

            var inputs = document.getElementsByClassName('supplierDiv');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'block' && inputs[i].id != supplierId) {

                    inputs[i].style.display = 'none';

                }

            }
        }


        function HideButtons(supplierName) {
           
            var inputs = document.getElementsByClassName('suuplierNameButton');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'block' && inputs[i].id != supplierName) {

                    inputs[i].style.display = 'none';

                }

            }
        }


        $scope.DeleteField = function (supplierId, supplierFieldId) {

            var respnse = SupplierService.DeleteSupplierField(supplierId, supplierFieldId)
            .success(function (data) {

                $scope.errorMessage = "Campo particular eliminado";

                toaster.pop({
                    type: 'success',
                    title: 'Ok',
                    body: "Campo particular eliminado",
                    timeout: 2000
                });

                $timeout(function () {

                   
                    HideAllInputs();
                    ShowButtons();
                    CloseInput(supplierId);
                    var divSupplier = document.getElementById(supplierId);
                    divSupplier.style.display = 'none';
                    $scope.errorMessage = "";



                }, 2000);

            })
              .error(function (data, status) {

                  $scope.errorMessage = data["message"];

                  toaster.pop({
                      type: 'error',
                      title: 'Error',
                      body: data["message"],
                      timeout: 3000
                  });
              });


        };

        $scope.AddField = function (supplier) {
            $scope.show = true;
            selectedSupplier = supplier;
            HideButtons(supplier['name']);
            
        };


        $scope.handButtonEvent = function () {

            var $template = $('#FieldsTemplate'),
            $clone = $template
                        .clone()
                        .removeClass('hide')
                        .insertBefore($template);

            $compile($clone)($scope);
        };


        function inputsSupplierFields() {
           
            $('#supplierFieldsInput .form-control').each(function () {
                $scope.allSupplierFieldsInArray.push($(this).val());
            });

          
        }



        $scope.ModifySupplier = function (supplierId) {
            $('#' + supplierId + ' .supplier-input').each(function () {
                allSupplierInputsInArray.push($(this).val());
            });

            var newSupplier = {
                "Name": allSupplierInputsInArray[0],
                "Commission": parseFloat(allSupplierInputsInArray[1])
            }

         
            var newSupplierJson = JSON.stringify(newSupplier);


            var response = SupplierService.UpdateSupplier(supplierId, newSupplierJson)
            .success(function (data) {

                $scope.errorModifySupplier = data["message"];

                toaster.pop({
                    type: 'success',
                    title: 'Ok',
                    body: data["message"],
                    timeout: 2000
                });


                $timeout(function () {
                    $scope.errorAddFieldsMessage = "";
                    document.getElementById("SupplierFieldsForm").reset();
                    HideAllInputs();
                    ShowButtons();
                    CloseInput(selectedSupplier['supplierId']);
                    $scope.show = false;
                    
                    $scope.SearchAllSuppliers();

                }, 2000);


            }).error(function (data, status) {
                $scope.errorModifySupplier = data["message"];
                toaster.pop({
                    type: 'error',
                    title: 'Error',
                    body: data["message"],
                    timeout: 3000
                });

                allSupplierInputsInArray = [];
            })



        }


        function createSupplierFieldsJson() {
            for (var i = 0; i < $scope.allSupplierFieldsInArray.length - 1; i = i + 2) {

                var supplierFieldInPositionIAsJson = {
                    "fieldName": $scope.allSupplierFieldsInArray[i],
                    "typeOfField": $scope.allSupplierFieldsInArray[i + 1]
                };

                $scope.SupplierFieldsJson.push(supplierFieldInPositionIAsJson);
            }
        }


        $scope.SaveSupplierFields = function () {
            $scope.errorAddFieldsMessage = "";
            inputsSupplierFields();
            createSupplierFieldsJson();
            $scope.SupplierFieldsJson.splice($scope.SupplierFieldsJson.length - 1, 1);

            var supplierFieldsAsJson = JSON.stringify($scope.SupplierFieldsJson);

            var response = SupplierService.ValidateSupplier(supplierFieldsAsJson)
            .success(function (data) {
                $scope.allSupplierFieldsInArray = [];
                $scope.SupplierFieldsJson = [];
                inputsSupplierFields();
                createSupplierFieldsJson();
                $scope.SupplierFieldsJson.splice($scope.SupplierFieldsJson.length - 1, 1);

                var supplierFieldsAsJson = JSON.stringify($scope.SupplierFieldsJson);

                var responseInsert = SupplierService.InsertFieldsToSupplier(selectedSupplier['supplierId'], supplierFieldsAsJson)
                .success(function (data) {

                    $scope.errorAddFieldsMessage = data["message"];
                    $scope.allSupplierFieldsInArray = [];
                    $scope.SupplierFieldsJson = [];

                    toaster.pop({
                        type: 'success',
                        title: 'Ok',
                        body: data["message"],
                        timeout: 2000
                    });


                    $timeout(function () {
                        $scope.errorAddFieldsMessage = "";


                        document.getElementById("SupplierFieldsForm").reset();
                      
                        HideAllInputs();
                        ShowButtons();
                        CloseInput(selectedSupplier['supplierId']);
                        var divSupplier = document.getElementById(selectedSupplier['supplierId']);
                        divSupplier.style.display = 'none';
                       
                        $scope.show = false;
                        
                     
                    }, 2000);

                })
                .error(function (data, status) {

                    $scope.errorAddFieldsMessage = data["message"];

                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: data["message"],
                        timeout: 3000
                    });
                })
            })
            .error(function (data, status) {
                $scope.errorAddFieldsMessage = data["message"];
                toaster.pop({
                    type: 'error',
                    title: 'Error',
                    body: data["message"],
                    timeout: 3000
                });
            })
        };

    }



})();


