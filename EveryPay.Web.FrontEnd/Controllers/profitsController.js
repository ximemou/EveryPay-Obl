(function () {
    'use strict';

    angular
        .module('EveryPay')
        .controller('profitsController', function ($scope, $rootScope, $http, $location, $compile, GlobalService, ProfitService, toaster, $timeout) {

            $scope.errorMessage = "";
            $scope.Dates = [];
            $scope.TotalProfits = 0;
            $scope.ProfitsBySupplier = [];

            $scope.DataProperties = [];
            $scope.DataPropertiesValues = [];

            $scope.GetProfitsBySupplier = function()
            {
                getDatesAsArray();
                var dates = JSON.stringify($scope.Dates);

                var responseProfitsSupplier = ProfitService.GetProfitsSupplier(dates)
             
                .success(function (data) {

                    $scope.ProfitsBySupplier = data;  
                    fillProfitsPerSupplierList();
                    loadChart();
                    GetTotalProfits();

                }).error(function (data, status) {

                    $scope.errorMessage = data["message"];

                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: data["message"],
                        timeout: 3000
                    });

                });

            }

            function GetTotalProfits() {

                getDatesAsArray();
                var dates = JSON.stringify($scope.Dates);
               
                var responseProfits = ProfitService.GetTotalProfits(dates)
                .success(function (data) {

                    $scope.TotalProfits = data;

                }).error(function (data, status) {

                    $scope.errorMessage = data["message"];

                    toaster.pop({
                        type: 'error',
                        title: 'Error',
                        body: data["message"],
                        timeout: 3000
                    });
                });
            }

            function getDatesAsArray()
            {
                var initDate = $('#initialDate').val();
                var finishDate = $('#finalDate').val();

                $scope.Dates.push(initDate);
                $scope.Dates.push(finishDate);
            }
                        

            function loadChart()
            {
                var ctx = $("#profitsBySupplier");

                var data = {
                    labels: $scope.DataProperties,
                    datasets: [
                        {
                            label: "Ganancias",
                            backgroundColor: [
                                'rgba(255, 99, 132, 0.2)',
                                'rgba(54, 162, 235, 0.2)',
                                'rgba(255, 206, 86, 0.2)',
                            ],
                            borderColor: [
                                'rgba(255,99,132,1)',
                                'rgba(54, 162, 235, 1)',
                                'rgba(255, 206, 86, 1)',
                            ],
                            borderWidth: 1,
                            data: $scope.DataPropertiesValues,
                        }
                    ]
                };

                var myBarChart = new Chart(ctx, {
                    type: 'bar',
                    data: data,
                    options: {
                        scales: {
                            xAxes: [{
                                stacked: true
                            }],
                            yAxes: [{
                                stacked: true
                            }]
                        }
                    }
                });
            }

            function fillProfitsPerSupplierList() {

                var profits_supplierArray = $scope.ProfitsBySupplier;
                for (var key in profits_supplierArray) {

                    if (profits_supplierArray.hasOwnProperty(key)) {

                        $scope.DataProperties.push(key);                    
                        $scope.DataPropertiesValues.push(profits_supplierArray[key]);
                    }
                }
            }

        })
})();
