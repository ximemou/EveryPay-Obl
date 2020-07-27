(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('LogInService', LogInService);

    function LogInService($rootScope,$http,GlobalService) {
        var service = {};


        service.LogIn = function (userName, password) {
           return  $http({
                url: GlobalService.GetBaseUrl() + '/api/login',
                method: 'POST',
                data: {
                    "UserName": userName,
                    "Password": password

                },
                headers: { 'Content-Type': 'application/json' }

            })
        };
     
        return service;
    }

})();