(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('billController', function ($scope,$rootScope, $http,$location,$compile,TransactionService, GlobalService, BillService, SupplierService,toaster,$timeout) {
            $scope.errorMessage = "";
            $scope.selectedSupplier;
            $scope.allSupplierFieldsInArray = [];
            $scope.SupplierFieldsJson = [];
            $scope.Amount = 0;
            $scope.BillId = 0;

            $scope.GetSuppliers = function () {
            
                var response = SupplierService.GetAllSuppliers()
                .success(function(data){

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

                $scope.ShowSupplierFields = function (supplierId) {
                    HideInputs(supplierId);
                    $("#"+supplierId).empty();

                    var divSpecificFieldSuppleir = document.getElementById(supplierId);

                    if (divSpecificFieldSuppleir.style.display != 'block') {

                        divSpecificFieldSuppleir.style.display = 'block';

                        var responseSupplierFields = SupplierService.GetSupplierFields(supplierId)
                        .success(function (data) {

                            for (var i = 0; i < data.length; i++) {

                                var supplierField = data[i];
                                var supplierFieldId = supplierField["supplierFieldId"];
                                var supplierFieldName = supplierField["fieldName"];
                                var typeOfField = supplierField["typeOfField"];
                                TypeOfFieldInput(supplierFieldName, typeOfField, supplierId,supplierFieldId);
                            }

                            var amountInput = '<div class="input-group"> <span class="input-group-addon">$</span>'
                                            + '<input type="number" value="1000" min="0"'
                                            + 'step="0.01" data-number-to-fixed="2" data-number-stepfactor="100"'
                                            + 'class="form-control currency amount-input" id="c2" /> <br>';
                            var input = '<button type="submit" ng-click="SaveValues(' + supplierId + ')">Guardar factura</button>';

                            $("#" + supplierId).append(amountInput);
                            $("#" + supplierId).append(input);
                            $compile("#" + supplierId)($scope);
                           

                        }).error(function (data, status) {

                            $scope.errorMessage = data["message"];

                            toaster.pop({
                                type: 'error',
                                title: 'Error',
                                body: data["message"],
                                timeout: 3000
                            });
                        })
                    }
                    else {

                        divSpecificFieldSuppleir.style.display = 'none';
                    }

                   

                }


                $scope.SaveValues = function (supplierId) {
                   
                    inputsSupplierFields(supplierId);
                    createSupplierFieldsJson();
                    retrieveAmount(supplierId);

                    var transactionId = TransactionService.GetTransactionId();

                    saveBill(transactionId, supplierId);
                };

                function saveBill(transactionId, supplierId)
                {
                    
                    var responseTransactionBills = TransactionService.AddBills(transactionId, supplierId, $scope.Amount)
                    .success(function (data) {

                        var responseBillsId = TransactionService.GetBills(transactionId)
                        .then(function (response) {
                            
                            $scope.BillId = response.data[response.data.length -1]["billId"];
                            saveBillValues($scope.BillId, JSON.stringify($scope.SupplierFieldsJson));

                        });               

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

                function saveBillValues(billId, SupplierFieldsJson) {
                    
                    var responseBillValues = BillService.SaveBillValues(billId, SupplierFieldsJson)
                    .success(function (data) {
                        $scope.errorMessage = "Factura agregada correctamente";

                        toaster.pop({
                            type: 'success',
                            title: 'Ok',
                            body: "Factura agregada correctamente",
                            timeout: 3000
                        });
                      
                        $scope.allSupplierFieldsInArray = [];
                        $scope.SupplierFieldsJson = [];

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


                        var responseDelete = BillService.DeleteBill(billId)
                        .error(function (data, status) {

                            $scope.errorMessage = data["message"];

                            toaster.pop({
                                type: 'error',
                                title: 'Error',
                                body: data["message"],
                                timeout: 3000
                            });

                        });


                    });
                }

                
                function TypeOfFieldInput(supplierFieldName,typeOfField,supplierId,supplierFieldId) {
                    
                    var inputs = supplierFieldName;

                    switch (typeOfField) {

                        case 'Text':

                            
                            inputs += '<br><input type="text" id="'+supplierFieldId+'" class="input-class form-control"><br>';
                            $("#"+supplierId).append(inputs);
                            break;

                        case 'Date':
                            inputs += '<br><input type="date" id="' + supplierFieldId + '" class="form-control input-class"><br>';
                            $("#"+supplierId).append(inputs);
                            break;
                        
                        case 'Numeric':
                            inputs += '<br><input class="form-control input-class" id="' + supplierFieldId + '"type="number" step=0.01><br>';
                            $("#"+supplierId).append(inputs);
                            break;                                    
                    }

                   
                    $compile("#" + supplierId)($scope);
                }


                function HideInputs(supplierId) {
                   
                    var inputs =document.getElementsByClassName('supplierFieldDiv');
                    for (var i = 0; i < inputs.length; i++) {
                      
                        if (inputs[i].style.display == 'block' && inputs[i].id!=supplierId) {

                            inputs[i].style.display = 'none';

                        }

                    }
                }

                function retrieveAmount(supplierId) {

                    $('#' + supplierId + ' .amount-input').each(function () {
                        $scope.Amount =$(this).val();
                    });
                }

                function inputsSupplierFields(supplierId) {
                    $('#'+supplierId+' .input-class').each(function () {
                        $scope.allSupplierFieldsInArray.push($(this).attr('id'));
                        $scope.allSupplierFieldsInArray.push($(this).val());
                    });
                }

                function createSupplierFieldsJson() {
                    for (var i = 0; i < $scope.allSupplierFieldsInArray.length - 1; i=i+2){
                       
                        var supplierFieldInPositionIAsJson = {
                            "value": $scope.allSupplierFieldsInArray[i+1],
                            "idSupplierField": $scope.allSupplierFieldsInArray[i]
                        };

                        $scope.SupplierFieldsJson.push(supplierFieldInPositionIAsJson);
                    }
                }

                $scope.loadPaymentPage = function(){

                    $location.path('/payTransaction');

                }
            
            })
})();
