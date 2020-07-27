(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('updateUserController', updateUserController);

    updateUserController.$inject = ['$scope', '$http', '$location', '$rootScope', 'toaster', 'GlobalService', 'UserService', '$timeout'];

    function updateUserController($scope, $http, $location, $rootScope, toaster, GlobalService, UserService, $timeout) {


        $scope.show = false;
        $scope.userSearched = {};
        $scope.showData = false;
        $scope.showAll = false;
        $scope.showData = false;

        var allUserInputsArray = [];


        $scope.UpdateUser = function (userId) {

            $('#' + userId + ' .user-input').each(function () {
                allUserInputsArray.push($(this).val());
            });

             
            var user = {
                name: allUserInputsArray[0],
                lastName: allUserInputsArray[1],
                userName: allUserInputsArray[2],
                password: allUserInputsArray[3],
                role: allUserInputsArray[4]
            }

            var userAsJson = JSON.stringify(user);


            var response = UserService.UpdateUser(userId, userAsJson)
        .success(function (data) {

            $scope.errorMessage = "Usuario modificado";
            toaster.pop({
                type: 'success',
                title: 'Ok',
                body: "Usuario modificado",
                timeout: 3000
            });


            $timeout(function () {

                $location.path('/adminUsers');
            }, 1000);

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
            $scope.name = user['name'];
            $scope.lastName = user['lastName'];
            $scope.userName = user['userName'];
            $scope.password = user['password'];
            $scope.role = user['role'];


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

