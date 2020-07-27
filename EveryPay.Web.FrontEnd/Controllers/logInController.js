(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('logInController',logInController);

    logInController.$inject = ['$scope','$http', '$location', '$rootScope', 'toaster', 'GlobalService','LogInService','$timeout'];
      
    function logInController($scope, $http, $location, $rootScope, toaster, GlobalService, LogInService, $timeout) {

            $scope.errorMessage = "";

            $scope.userName = "";
            $scope.password = "";


            $scope.LogIn = function () {

                var response= LogInService.LogIn($scope.userName,$scope.password)
                
                .success(function (data) {

                    
                    $scope.token = data["authToken"];
                    $rootScope.token = $scope.token;

                    var userId = data["userId"];

                    var user = data["user"];
                    var role = user["role"];

                    var name = user["userName"];
                   
                    GlobalService.SetActualUser(user);
                    GlobalService.SetUser(name);
                    GlobalService.SetIsAdministrator(role);

                    GlobalService.SetToken($scope.token);

                    GlobalService.SetLogged(true);

                    $location.path('/main');

                   
                }).error(function (data, status) {

                    $scope.errorMessage = data["message"];

                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: data["message"],
                        timeout: 3000
                    });
                })
            }

        };




})();

