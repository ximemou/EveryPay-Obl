(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('ProfitsService', ProfitsService);

    function ProfitsService($rootScope, $http, GlobalService) {
        var service = {};

        service.resources = {


        };

        service.GetTotalProfits = function (dates) {

            return  $http({
                url: GlobalService.GetBaseUrl() + '/api/profits-supplier/',
                method: 'POST',
                data: dates,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };
 


        return service;
    }

})();