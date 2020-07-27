(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('deleteClientController', deleteClientController);

    deleteClientController.$inject = ['$scope', '$http', '$location', '$rootScope', 'toaster', 'GlobalService', 'ClientService', '$timeout'];

    function deleteClientController($scope, $http, $location, $rootScope, toaster, GlobalService, ClientService, $timeout) {

       
        $scope.show = false;
        $scope.clientSearched = {};
        $scope.showData = false;
        $scope.showAll = false;
       

        $scope.SearchClient = function () {
            $scope.showAll = true;
            var response = ClientService.GetAllClients()
              .success(function (data) {

                  if (data.length > 0) {

                      alert($scope.clientIdentification);

                      var encontre = false;
                      for (var i = 0; i < data.length && !encontre; i++) {
                          alert("DEL FOR"+data[i]['identification']);
                          if (data[i]['identification'] == $scope.clientIdentification) {
                              encontre = true;
                              $scope.showData = true;
                              $scope.clientSearched = {
                                  clientId: data[i]['clientId'],
                                  name: data[i]['name'],
                                  lastName: data[i]['lastName'],
                                  identification: data[i]['identification'],
                                  address: data[i]['address'],
                                  phoneNumber: data[i]['phoneNumber'],
                                  totalPoints:data[i]['totalPoints']
                                 
                              }

                          }

                      }
                      if (!encontre) {
                          $scope.errorMessage = "No existe el cliente buscado";
                          toaster.pop({
                              type: 'error',
                              title: 'Error',
                              body: "No existe el cliente buscado",
                              timeout: 3000
                          });

                      }


                  }
                  else {
                      $scope.errorMessage = "No hay clientes en el sistema";
                      toaster.pop({
                          type: 'error',
                          title: 'Error',
                          body: "No hay clientes en el sistema",
                          timeout: 3000
                      });
                  }
              });


        }



        $scope.DeleteClient = function () {

            var response = ClientService.DeleteClient($scope.clientSearched['clientId'])
        .success(function (data) {

            $scope.errorMessage = "Cliente eliminado";
            toaster.pop({
                type: 'success',
                title: 'Ok',
                body: "Cliente eliminado",
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
            $scope.clientSearched = client;
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
