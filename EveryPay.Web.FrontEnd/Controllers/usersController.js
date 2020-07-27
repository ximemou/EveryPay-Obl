(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('usersController',usersController);

    usersController.$inject = ['$scope','$http', '$location', '$rootScope', 'toaster', 'GlobalService','UserService','$timeout'];
      
    function usersController($scope, $http, $location, $rootScope, toaster, GlobalService, UserService, $timeout) {

        $scope.userName = "";
        $scope.name = "";
        $scope.lastName = "";
      
        
        $scope.password = "";
        $scope.confirmPassword = "";
        $scope.role = "Cashier";


        $scope.errorMessage = "";

        $scope.GetUsers = function () {

            var response = UserService.GetAllUsers()

           .success(function (data) {

                if (data.length > 0) {
                    $scope.Users = data;
                }
                else {
                    $scope.errorMessage = "No existen usuarios en el sistema";
                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: "No existen usuarios en el sistema",
                        timeout: 3000
                    });
                }

            });
        }

        $scope.CreateUser = function () {

            var user = {
                name: $scope.name,
                lastName:$scope.lastName,
                userName: $scope.userName,
                password: $scope.password,
                confirmPassword:$scope.confirmPassword,
                role: $scope.role
            }



      
            var response = UserService.CreateUser(user)
            .success(function (data) {

                $scope.errorMessage = "Creado correctamente";
                toaster.pop({
                    type: 'success',
                    title: 'Ok',
                    body:"Usuario creado",
                    timeout: 2000
                });

                $timeout(function () {

                    $location.path('/adminUsers');
                }, 2000);

            })
            .error(function(data,status){
                
                $scope.errorMessage = data["message"];
                toaster.pop({
                    type: 'error',
                    title: 'Error',
                    body: data["message"],
                    timeout: 3000
                });
                
            });

        }
    };




})();

