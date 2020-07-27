(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('SupplierService', SupplierService);

    function SupplierService($rootScope, $http, GlobalService) {
        var service = {};

        service.resources = {
            

        };

        service.GetAllSuppliers = function () {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers',
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };

        service.GetSupplierFields = function (supplierId) {
           
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers/' + supplierId + '/fields',
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };

        service.ValidateSupplier = function (SupplierFieldsJson) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliersValidation',
                method: 'POST',
                data: SupplierFieldsJson,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        }

        service.CreateSupplier = function (name, commission) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers',
                method: 'POST',
                data: {
                    "Name": name,
                    "Commission": parseFloat(commission)

                },
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }

            })
        }

        service.InsertFieldsToSupplier = function (supplierId, SupplierFieldsJson) {

            return  $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers/' + supplierId + '/fields',
                method: 'POST',
                data: SupplierFieldsJson,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }

            })

        };

        service.DeleteSupplier = function (supplierId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers/' + supplierId,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            })
        };


        service.DeleteSupplierField = function (supplierId, supplierFieldId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers/'+ supplierId+'/fields/'+supplierFieldId,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }

            })
        };


        service.UpdateSupplier = function (supplierId, supplier) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/suppliers/'+ supplierId,
                method: 'PUT',
                data: supplier,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });

        }


        return service;
    }

})();