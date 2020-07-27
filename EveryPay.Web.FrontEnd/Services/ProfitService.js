(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('ProfitService', ProfitService);

    function ProfitService($rootScope, $http, GlobalService) {
        var service = {};


        service.GetProfitsSupplier = function (dates) {
            
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/profits-supplier/',
                method: 'POST',
                data: dates,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        };

        service.GetTotalProfits = function (dates) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/profits/',
                method: 'POST',
                data: dates,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        }

        return service;
    }



})();