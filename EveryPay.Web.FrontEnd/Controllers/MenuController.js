(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('MenuController', function ($scope, $http, $location, GlobalService, $timeout) {

            $scope.logged = false;
            $scope.isAdmin = false;
            $scope.userName = "";
            
            

            $scope.$on('lgdUpdated', function () {

                $scope.logged = GlobalService.GetLogged();

                $scope.isAdmin = $scope.logged ? GlobalService.GetIsAdministrator() : false;

                $scope.userName = $scope.logged ? GlobalService.GetUser() : "";
            });

            $scope.logOut = function () {
                $location.path('/');
                GlobalService.SetLogged(false);
                GlobalService.ResetResources();
            }


        });




})();

