(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('deleteUserController', deleteUserController);

    deleteUserController.$inject = ['$scope', '$http', '$location', '$rootScope', 'toaster', 'GlobalService', 'UserService', '$timeout'];

    function deleteUserController($scope, $http, $location, $rootScope, toaster, GlobalService, UserService, $timeout) {


        $scope.show = false;
        $scope.userSearched = {};
        $scope.showData = false;
        $scope.showAll = false;
        $scope.showData = false;

        $scope.SearchUser = function () {
            $scope.showAll = true;
            var response = UserService.GetAllUsers()
              .success(function (data) {

                  if (data.length > 0) {

                      var encontre = false;
                      for (var i = 0; i < data.length && !encontre; i++) {

                          if (data[i]['userName'] == $scope.userName) {
                              encontre = true;
                              $scope.showData = true;
                              $scope.userSearched = {
                                  userId: data[i]['userId'],
                                  name: data[i]['name'],
                                  lastName: data[i]['lastName'],
                                  userName: data[i]['userName'],
                                  password: data[i]['password'],
                                  role: data[i]['role']

                              }

                          }

                      }
                      if (!encontre) {
                          $scope.errorMessage = "No existe el usuario buscado";

                          toaster.pop({
                              type: 'error',
                              title: 'Error',
                              body: "No existe el usuario buscado",
                              timeout: 3000
                          });
                      }


                  }
                  else {
                      $scope.errorMessage = "No hay usuarios en el sistema";

                      toaster.pop({
                          type: 'error',
                          title: 'Error',
                          body: "No hay usuarios en el sistema",
                          timeout: 3000
                      });
                  }
              });


        }



        $scope.DeleteUser = function () {

            var response = UserService.DeleteUser($scope.userSearched['userId'])
        .success(function (data) {

            $scope.errorMessage = "Usuario eliminado";

            toaster.pop({
                type: 'success',
                title: 'Ok',
                body: "Usuario eliminado",
                timeout: 2000
            });

            $timeout(function () {

                $location.path('/adminUsers');
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



        $scope.SearchAllUsers = function () {

            $scope.show = true;
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
            })


        };

        $scope.ShowData = function (user) {
            HideInputs($scope.user);
            $scope.userSearched = user;
            var divUserId = document.getElementById(user['userId']);
            divUserId.style.display = 'block';

            $scope.showData = true;
        };



        function HideInputs(userId) {

            var inputs = document.getElementsByClassName('userDiv');
            for (var i = 0; i < inputs.length; i++) {

                if (inputs[i].style.display == 'block' && inputs[i].id != userId) {

                    inputs[i].style.display = 'none';

                }

            }
        }





    };




})();


