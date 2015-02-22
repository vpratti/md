(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('userManagementService', userManagementService);

    userManagementService.$inject = ['$http', 'ngAuthSettings'];

    function userManagementService($http, ngAuthSettings) {
        var factory = {
            getAllUsers: getAllUsers
        };

        return factory;

        function getAllUsers() {
           return $http.get(ngAuthSettings.apiServiceBaseUri + "api/Account/GetUsers");
        }
    }
}(angular));