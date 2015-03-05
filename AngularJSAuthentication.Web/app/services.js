(function(angular) {
    'use strict';

    angular
    .module('VirtualClarityApp')
    .factory('passwordRecovery', passwordRecovery);

    passwordRecovery.$inject = ['$modal'];

    function passwordRecovery($modal) {
        var factory = {
            recoverPassword: recoverPassword
        };

        return factory;

        function recoverPassword() {
            return $modal.open({
                templateUrl: 'app/views/passwordRecovery.html',
                controller: 'passwordRecoveryCtrl as vm',
                size: 'sm'
            });
        }
    }
}(angular));

(function (angular) {
    'use strict';

    angular
    .module('VirtualClarityApp')
    .factory('passwordRecoveryService', passwordRecoveryService);

    passwordRecoveryService.$inject = ['$http'];

    function passwordRecoveryService($http) {
        var factory = {

        };

        return factory;
    }
}(angular));

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('userManagement', userManagement);

    userManagement.$inject = ['$modal'];

    function userManagement($modal) {
        var factory = {
            editUser: editUser
        };

        return factory;

        function editUser(user) {
            return $modal.open({
                templateUrl: 'app/views/editUser.html',
                controller: 'editUserCtrl as vm',
                size: 'lg',
                resolve: {
                    user: function () {
                        return user;
                    }
                }
            });
        }
    }
}(angular));

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('userManagementService', userManagementService);

    userManagementService.$inject = ['$http', 'ngAuthSettings'];

    function userManagementService($http, ngAuthSettings) {
        var factory = {
            getAllUsers: getAllUsers,
            deleteUser: deleteUser,
            updateUser: updateUser
        };

        return factory;

        function getAllUsers() {
           return $http.get(ngAuthSettings.apiServiceBaseUri + "api/Account/GetUsers");
        }

        function deleteUser(id) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + "api/Account/DeleteUser?userId=" + id);
        }

        function updateUser(user) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + "api/Account/UpdateUser", user);
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
            createRole: createRole,
            deleteRole: deleteRole
        };

        return factory;

        function getAllRoles() {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Roles/GetRoles');
        }

        function createRole(name) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Roles/CreateRole?roleName=' + name);
        }

        function deleteRole(id) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/Roles/DeleteRole?id=' + id);
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