(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('ClientService', ClientService);

    function ClientService($rootScope, $http, GlobalService) {
        var service = {};


        service.GetAllClients = function () {
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/clients',
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        };

        service.GetClient = function (clientId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/clients/' + clientId,
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        }


        service.DeleteClient = function (clientId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/clients/' + clientId,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });

        }

        service.CreateClient = function (client) {
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/clients',
                method: 'POST',
                data: {
                    "Name": client.name,
                    "LastName": client.lastName,
                    "identification": client.identification,
                    "Address": client.address,
                    "phoneNumber": client.phoneNumber
                },
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        }


        service.UpdateClient = function (clientId,client) {
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/clients/'+clientId,
                method: 'PUT',
                data: client,  
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        }


        return service;
    }





})();