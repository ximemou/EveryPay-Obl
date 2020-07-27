(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('UserService', UserService);

    function UserService($rootScope, $http, GlobalService) {
        var service = {};


        service.GetAllUsers = function () {
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/users',
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        };

        service.GetUser = function (userId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/users/' + userId,
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        }


        service.DeleteUser = function (userId) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/users/' + userId,
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });

        }

        service.CreateUser = function (user) {
            return $http({
                url: GlobalService.GetBaseUrl() + '/api/users',
                method: 'POST',
                data: {
                    "Name": user.name,
                    "LastName": user.lastName,
                    "UserName": user.userName,
                    "Password": user.password,
                    "PonfirmPassword": user.confirmPassword,
                    "Role": user.role
                },
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });
        }

        service.UpdateUser = function (userId,user) {

            return $http({
                url: GlobalService.GetBaseUrl() + '/api/users/'+userId,
                method: 'PUT',
                data: user,
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': GlobalService.GetToken()
                }
            });

        }



        return service;
    }





})();