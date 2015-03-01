(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('userManagementService', userManagementService);

    userManagementService.$inject = ['$http', 'ngAuthSettings'];

    function userManagementService($http, ngAuthSettings) {
        var factory = {
            getAllUsers: getAllUsers,
            deleteUser: deleteUser
        };

        return factory;

        function getAllUsers() {
           return $http.get(ngAuthSettings.apiServiceBaseUri + "api/Account/GetUsers");
        }

        function deleteUser(id) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + "api/Account/DeleteUser?userId=" + id);
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
            getAllRoles: getAllRoles,
            createRole: createRole
        };

        return factory;

        function getAllRoles() {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Roles/GetRoles');
        }

        function createRole(name) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Roles/CreateRole?roleName=' + name);
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('utility', utility);

    utility.$inject = ['$modal'];

    function utility($modal) {
        var factory = {
            confirm: confirm
        }

        return factory;

        function confirm(message) {
            return $modal.open({
                templateUrl: 'app/views/confirm.html',
                controller: 'confirmCtrl as vm',
                size: 'sm',
                resolve: {
                    message: function () {
                        return message;
                    }
                }
            });
        }
    }
}(angular));