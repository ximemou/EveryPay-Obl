(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('TransactionService', TransactionService);

    function TransactionService($rootScope, $http, GlobalService) {
        var service = {};

        service.resources = {
            transactionId: null,
            clientId:null

        };



        service.GetTransactionId = function () {
            return service.resources.transactionId;
        };

        service.SetTransactionId = function (idTransaction) {

            service.resources.transactionId = idTransaction;
        };

        service.GetClientId = function () {
            return service.resources.clientId;
        };

        service.SetClientId = function (idClient) {

            service.resources.clientId = idClient;
        };




        service.ResetResources = function () {
            
            service.resources = {
                transactionId: null,
                clientId: null
            };
        };

        service.CreateTransaction = function (date, client) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/transactions',
                method: 'POST',
                data: {
                    "transactionDate": date,
                    "clientIdentification":client

                },
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }

            })
        };

        service.AddBills = function (transactionId, supplierId, amount) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/transactions/' + transactionId + '/bills',
                method: 'POST',
                data:
                          [
                                {
                                    "supplierId": supplierId,
                                    "amount": amount
                                }
                          ],
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };

        service.GetBills = function (transactionId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/transactions/' + transactionId + '/bills',
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };

        service.PayTransaction = function (transactionId, paymentType, amount) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/transactions/' + transactionId + '/pay',
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                },
                data: {
                    'paymentMethodType': paymentType,
                    'amountGiven': amount
                }
            });
        }


        return service;
    }

})();