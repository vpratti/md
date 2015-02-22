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

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('rolesService', rolesService);

    rolesService.$inject = ['$http', 'ngAuthSettings'];

    function rolesService($http, ngAuthSettings) {
        var factory = {
            getAllRoles: getAllRoles
        };

        return factory;

        function getAllRoles() {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Roles/GetRoles');
        }
    }
}(angular));