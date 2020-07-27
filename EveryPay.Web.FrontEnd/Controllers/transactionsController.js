(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('transactionsController', function ($scope, $http, $location, TransactionService, GlobalService, toaster, $timeout) {

            $scope.errorMessage = "";

            $scope.TransactionDate;
            $scope.Client = "";

            $scope.Bills = [];
            $scope.Bill;

            $scope.AddBill = function () {
                $scope.AddTransaction();
                $location.path('/addBill');
            }

            $scope.AddTransaction = function () {
                  
                var date = $('#transactionDate').val();             

                var response = TransactionService.CreateTransaction(date, $scope.Client)

                .success(function (data) {

                    $scope.insertedTransactionId = data["message"];

                    toaster.pop({
                        type: 'success',
                        title: 'Ok',
                        body: data["message"],
                        timeout: 3000
                    });


                    TransactionService.SetTransactionId($scope.insertedTransactionId);
                    TransactionService.SetClientId($scope.Client);

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

        });

})();

