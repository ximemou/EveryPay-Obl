(function () {
    'use strict';

    angular
        .module('EveryPay')
        .factory('GlobalService', GlobalService);

    function GlobalService($rootScope) {
        var service = {};

        service.resources = {
            actualUser:null,
            userName: null,
            logged: false,
            token: null,
            isAdministrator: false,
            baseUrl: setUrl()
        };

        function setUrl() {

            var xhttp = new XMLHttpRequest();
            xhttp.onreadystatechange = function () {
                if (this.readyState == 4 && this.status == 200) {
                    readXml(this);
                }
            };
            xhttp.open("GET", "configUrl.xml", true);
            xhttp.send();

            function readXml(xml) {
                var xmlDoc = xml.responseXML;
                service.resources.baseUrl = xmlDoc.getElementsByTagName("baseUrl")[0].childNodes[0].nodeValue;
            }

        }

        service.GetActualUser = function () {
            return service.resources.actualUser;
        };

        service.SetActualUser = function (user) {
            service.resources.actualUser = user;
        }

        service.GetBaseUrl = function () {
            return service.resources.baseUrl;
        };

        service.GetUser = function () {
            return service.resources.userName;
        };

        service.SetUser = function (name) {
            service.resources.userName = name;
        };

        service.GetLogged = function () {
            return service.resources.logged;
        };

        service.SetLogged = function (lgd) {
            service.resources.logged = lgd;
            $rootScope.$broadcast("lgdUpdated");
        };

        service.GetToken = function () {
            return service.resources.token;
        };

        service.SetToken = function (userToken) {

            service.resources.token = userToken;
        };    

        service.GetIsAdministrator = function () {
            return service.resources.isAdministrator;
        };


        service.SetIsAdministrator = function (role) {
            if (role == "Administrator") {
                service.resources.isAdministrator = true;
            }
            else {
                service.resources.isAdministrator = false;
            }
        };

        service.ResetResources = function () {
            service.resources = {
                userName: null,
                logged: false,
                token: null,
                isAdministrator: false,
                baseUrl: setUrl()
            };
        };


        return service;
    }

})();