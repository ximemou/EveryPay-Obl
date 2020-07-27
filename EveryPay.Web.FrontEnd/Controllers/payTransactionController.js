(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('payTransactionController', function ($scope, $rootScope, $http, $location, $compile, TransactionService, GlobalService, BillService, toaster, $timeout) {
            $scope.errorMessage = "";
            $scope.AmountGiven = 0;
            $scope.PaymentType = "";
        
            $scope.GetBillsOfTransaction = function () {

                var transactionId = TransactionService.GetTransactionId();

                var responseBills = TransactionService.GetBills(transactionId)
                .success(function (data) {

                     $scope.Bills = data;
                    
                });
            }
            

            $scope.DeleteBill = function (billId) {

                $("#" + billId).empty();
                var responseDelete = BillService.DeleteBill(billId);
            }
            
            $scope.payTransaction = function () {

                $scope.PaymentType = $("#paymentType").val();

                var responsePay = TransactionService.PayTransaction(TransactionService.GetTransactionId(), $scope.PaymentType, $scope.AmountGiven)
                 
                .success(function (data) {

                    toaster.pop({
                        type: 'success',
                        title: 'Ok',
                        body: data["message"],
                        timeout: 2000
                    });

                    $timeout(function () {

                        $location.path('/main');
                    }, 2000);

                }).error(function (data, status) {

                    $scope.errorMessage = data["message"];

                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: data["message"],
                        timeout: 3000
                    });
                });

            }
   
        })
})();
