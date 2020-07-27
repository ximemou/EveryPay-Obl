(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('BillService', BillService);

    function BillService($rootScope, $http, GlobalService) {
        var service = {};

        service.resources = {

        };

        service.SaveBillValues = function (billId, SupplierFieldsJson) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/bills/' + billId + '/values',
                method: 'POST',
                data: SupplierFieldsJson,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }

            })
        };

        service.DeleteBill = function (billId) {

            return  $http({
                url: GlobalService.GetBaseUrl() + '/api/bills/' + billId,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };


        return service;
    }

})();