(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('clientsController', clientsController);

    clientsController.$inject = ['$scope', '$http', '$location', '$rootScope', 'toaster', 'GlobalService', 'ClientService','$timeout'];

    function clientsController($scope, $http, $location, $rootScope, toaster, GlobalService, ClientService, $timeout) {

        $scope.identification = "";
        $scope.name = "";
        $scope.lastName = "";
        $scope.address = "";
        $scope.phoneNumber = "";


        $scope.errorMessage = "";

        $scope.GetClients = function () {

            var response = ClientService.GetAllClients()

           .success(function (data) {

               if (data.length > 0) {
                   $scope.AllClients = data;
               }
               else {
                   $scope.errorMessage = "No existen clientes registrados en el sistema";

                   toaster.pop({
                       type: 'error',
                       title: 'Error',
                       body:"No exiten clientes registrados en el sistema",
                       timeout: 3000
                   });
               }

           });
        }

        $scope.CreateClient = function () {

            var client = {
                name: $scope.name,
                lastName: $scope.lastName,
                identification: $scope.identification,
                address: $scope.address,
                phoneNumber: $scope.phoneNumber
            }


            var response = ClientService.CreateClient(client)
            .success(function (data) {

                $scope.errorMessage = "Creado correctamente";

                toaster.pop({
                    type: 'success',
                    title: 'Ok',
                    body: "Creado correctamente",
                    timeout: 2000
                });


                $timeout(function () {

                    $location.path('/adminClients');
                }, 2000);

            })
            .error(function (data, status) {

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

