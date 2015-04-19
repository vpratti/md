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

    passwordRecoveryService.$inject = ['$http', 'ngAuthSettings'];

    function passwordRecoveryService($http, ngAuthSettings) {
        var factory = {
            resetPassword: resetPassword
        };

        return factory;

        function resetPassword(email) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/PasswordRecovery/ResetPassword?email=' + email);
        }
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

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('messaging', messaging);

    messaging.$inject = ['$rootScope'];

    function messaging($rootScope) {
        var factory = {
            publish: publish,
            subscribe: subscribe
        };

        return factory;

        function publish(name, params) {
            $rootScope.$emit(name, params);
        }

        function subscribe(name, listener) {
            $rootScope.$on(name, listener);
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('accountLocksService', accountLocksService);

    accountLocksService.$inject = ['$http', 'ngAuthSettings'];

    function accountLocksService($http, ngAuthSettings) {
        var factory = {
            unlock: unlock,
            lock: lock
        }

        return factory;

        function unlock(userId) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/AccountLocks/Unlock?userId=' + userId);
        }

        function lock(userId) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/AccountLocks/Lock?userId=' + userId);
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('lookupsService', lookupsService);

    lookupsService.$inject = ['$http', 'ngAuthSettings'];

    function lookupsService($http, ngAuthSettings) {
        var factory = {
            getCategories: getCategories,
            createCategory: createCategory,
            editCategory: editCategory,
            deleteCategory: deleteCategory,
            addLookupAlias: addLookupAlias,
            getLookupValues: getLookupValues,
            addLookupValue: addLookupValue,
            deleteAlias: deleteAlias,
            editAlias: editAlias,
            deleteLookupValue: deleteLookupValue,
            editLookupValue: editLookupValue
        };

        return factory;

        function editAlias(alias) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/EditAlias', alias);
        }

        function editLookupValue(lookupValue) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/EditLookupValue', lookupValue);
        }

        function deleteLookupValue(id) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/DeleteLookupValue?id=' + id);
        }

        function deleteAlias(id) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/DeleteAlias?id=' + id);
        }

        function addLookupValue(lookupValue) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/CreateLookupValue', lookupValue);
        }    

        function getLookupValues() {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/GetLookupValues');
        }

        function getCategories(id) {
            return $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/GetCategories');
        }

        function createCategory(newCategory) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/CreateCategory', newCategory);
        }

        function editCategory(category) {
            return $http.put(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/EditCategory', category);
        }

        function addLookupAlias(lookupAlias) {
            return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/CreateLookupAlias', lookupAlias);
        }

        function deleteCategory(id) {
            return $http.delete(ngAuthSettings.apiServiceBaseUri + 'api/Lookup/DeleteCategory?id=' + id);
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('lookups', lookups);

    lookups.$inject = ['$modal'];

    function lookups($modal) {
        var factory = {
            editCategoryLookup: editCategoryLookup
        };

        return factory;

        function editCategoryLookup(category, categoryTypes) {
            return $modal.open({
                templateUrl: 'app/views/editCategoryLookup.html',
                controller: 'editCategoryLookupCtrl as vm',
                resolve: {
                    category: function() {
                        return category;
                    },
                    categoryTypes: function() {
                        return categoryTypes;
                    }
                }
            });
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('categoryTypesFactory', categoryTypesFactory);

    categoryTypesFactory.$inject = ['$modal'];

    function categoryTypesFactory($modal) {
        var factory = {
            createCategoryType: createCategoryType
        };

        return factory;

        function createCategoryType() {
            return $modal.open({
                templateUrl: 'app/views/createCategoryType.html',
                controller: 'createCategoryTypeCtrl as vm',
                size: 'sm'
            });
        }
    }
}(angular));

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .factory('categoryFactory', categoryFactory);

    categoryFactory.$inject = ['$modal'];

    function categoryFactory($modal) {
        var factory = {
            addCategory: addCategory
        };

        return factory;

        function addCategory() {
            return $modal.open({
                templateUrl: 'app/views/addCategory.html',
                controller: 'addCategoryCtrl as vm',
            });
        }
    }
}(angular));