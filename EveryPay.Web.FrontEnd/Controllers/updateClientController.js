(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('updateClientController', updateClientController);

    updateClientController.$inject = ['$scope', '$http', '$location', '$rootScope', 'toaster', 'GlobalService', 'ClientService', '$timeout'];

    function updateClientController($scope, $http, $location, $rootScope, toaster, GlobalService, ClientService, $timeout) {


        $scope.show = false;
        $scope.userSearched = {};
        $scope.showData = false;
        $scope.showAll = false;
        $scope.showData = false;

     

        var allClientsInputsArray = [];


        $scope.UpdateClient = function (clientId) {

            $('#' + clientId + ' .client-input').each(function () {
                allClientsInputsArray.push($(this).val());
            });


            var client = {
                name: allClientsInputsArray[0],
                lastName: allClientsInputsArray[1],
                identification: allClientsInputsArray[2],
                address: allClientsInputsArray[3],
                phoneNumber: allClientsInputsArray[4]     
            }

            var clientAsJson = JSON.stringify(client);
            var response = ClientService.UpdateClient(clientId, clientAsJson)
        .success(function (data) {

            $scope.errorMessage = "Cliente modificado";

            toaster.pop({
                type: 'success',
                title: 'Ok',
                body: "Cliente modificado",
                timeout: 3000
            });

            $timeout(function () {

                $location.path('/adminClients');
            }, 3000);

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

        };



        $scope.SearchAllClients = function () {

            $scope.show = true;
            var response = ClientService.GetAllClients()
            .success(function (data) {
                if (data.length > 0) {
                    $scope.Clients = data;
                }
                else {
                    $scope.errorMessage = "No existen clientes en el sistema";

                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: "No existen clientes en el sistema",
                        timeout: 3000
                    });
                }
            })


        };

        $scope.ShowData = function (client) {
            HideInputs($scope.client);
            $scope.address = client['address'];
            $scope.phone = client['phoneNumber'];
            var divClientId = document.getElementById(client['clientId']);
            divClientId.style.display = 'block';

            $scope.showData = true;
        };



        function HideInputs(clientId) {

            var inputs = document.getElementsByClassName('clientDiv');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'block' && inputs[i].id != clientId) {

                    inputs[i].style.display = 'none';

                }

            }
        }









    };




})();

