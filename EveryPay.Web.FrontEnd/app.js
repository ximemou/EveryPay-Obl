(function () {
    'use strict';

    angular.module('EveryPay', ['ngRoute','datePicker','toaster'])
    .config(function ($routeProvider) {

        $routeProvider
         .when('/login', {
             controller: 'logInController',
             templateUrl: 'Views/login.html',
             controllerAs: 'ctrl'
         }
        )
          .when('/main', {
              templateUrl: 'Views/home.html'
          }
         )
            .when('/',{
        
                templateUrl:'Views/home.html'
        
    })


        .when('/addSuppliers', {
            controller: 'suppliersController',
            templateUrl: 'Views/addSuppliers.html',
            controllerAs: 'ctrl'
        })
            .when('/addTransaction', {
                controller: 'transactionsController',
                templateUrl: 'Views/addTransaction.html',
                controllerAs: 'ctrl'
            })
            .when('/addBill', {
                controller: 'billController',
                templateUrl: 'Views/addBill.html',
                controllerAs: 'ctrl'
            })
            .when('/payTransaction', {
                controller: 'payTransactionController',
                templateUrl: 'Views/payTransaction.html',
                controllerAs: 'ctrl'
            })
              .when('/profits', {
                  controller: 'profitsController',
                  templateUrl: 'Views/profits.html',
                  controllerAs: 'ctrl'
              })
            .when('/getUsers', {
                controller: 'usersController',
                templateUrl: 'Views/getAllUsers.html',
                controllerAs:'ctrl'
            })
              .when('/getClients', {
                  controller: 'clientsController',
                  templateUrl: 'Views/getAllClients.html',
                  controllerAs: 'ctrl'
              })

            .when('/adminUsers', {
                controller: 'usersController',
                templateUrl: 'Views/usersMenu.html',
                controllerAs: 'ctrl'
            })
            .when('/adminSuppliers',{
                controller: 'suppliersController',
                templateUrl: 'Views/suppliersMenu.html',
               controllerAs: 'ctrl'
          })
            .when('/adminClients', {

                controller: 'clientsController',
                templateUrl: 'Views/clientsMenu.html',
                controllerAs: 'ctrl'
            })
            .when('/addUser', {
                controller: 'usersController',
                templateUrl: 'Views/addUser.html',
                controllerAs: 'ctrl'
            })
             .when('/updateUser', {
                 controller: 'updateUserController',
                 templateUrl: 'Views/updateUser.html',
                 controllerAs: 'ctrl'
             })
             .when('/deleteSupplier', {
                 controller: 'suppliersController',
                 templateUrl: 'Views/deleteSupplier.html',
                 controllerAs: 'ctrl'
             })
            .when('/updateSupplier', {
                controller: 'suppliersController',
                templateUrl: 'Views/updateSupplier.html',
                controllerAs: 'ctrl'
            })
             .when('/addClient', {
                 controller: 'clientsController',
                 templateUrl: 'Views/addClient.html',
                 controllerAs: 'ctrl'
             })
            .when('/updateClient', {
                controller: 'updateClientController',
                templateUrl: 'Views/updateClient.html',
                controllerAs: 'ctrl'
            })
             .when('/deleteUser', {
                 controller: 'deleteUserController',
                 templateUrl: 'Views/deleteUser.html',
                 controllerAs: 'ctrl'
             })
            .when('/deleteClient', {
                controller: 'deleteClientController',
                templateUrl: 'Views/deleteClient.html',
                controllerAs: 'ctrl'
            })
            
            
        .otherwise({ redirectTo: '/login' });

    })



    .run(run)
    .service('BillService', function () {
        var service = this;
        service.Bill = "";
    });
   
    

    run.$inject = ['$rootScope', '$location', '$http'];
    function run($rootScope, $location, $http) { 
        $rootScope.token = "";
    }
   
 
})();