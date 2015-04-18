(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('loginCtrl', loginCtrl);

    loginCtrl.$inject = ['$scope', '$location', 'authService', 'ngAuthSettings', 'utility', 'passwordRecovery'];

    function loginCtrl($scope, $location, authService, ngAuthSettings, utility, passwordRecovery) {

        $scope.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };

        $scope.message = "";

        $scope.login = function () {
            $scope.inProgress = true;
            authService.login($scope.loginData).then(function() {
                    $location.path('/dashboard');
                },
                function(err) {
                    $scope.message = err.error_description;
                    $scope.inProgress = false;
                });
        };

        $scope.authExternalProvider = function(provider) {

            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                + "&response_type=token&client_id=" + ngAuthSettings.clientId
                + "&redirect_uri=" + redirectUri;
            window.$windowScope = $scope;

            window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };

        $scope.authCompletedCB = function(fragment) {
            $scope.$apply(function() {
                if (fragment.haslocalaccount == 'False') {
                    authService.logOut();
                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };
                    $location.path('/associate');

                } else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function() {
                            $location.path('/dashboard');
                        },
                        function(err) {
                            $scope.message = err.error_description;
                        });
                }

            });
        };

        $scope.forgotPassword = function() {
            utility.confirm("Do you want a new password to be sent to your registered e-mail?")
                .result.then(function() {
                    passwordRecovery.recoverPassword().result.then(function(data) {
                        utility.confirm(data);
                    });
                });
        };
    }
}(angular));

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('indexCtrl', indexCtrl);

    indexCtrl.$inject = ['$scope', '$location', 'authService'];

    function indexCtrl($scope, $location, authService) {
        $scope.logOut = function () {
            authService.logOut();
            $location.path('/');
        }

        $scope.authentication = authService.authentication;
    }
}(angular));

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('dashboardCtrl', dashboardCtrl);

    function dashboardCtrl() {
        var vm = this;
        vm.name = 'dashboardCtrl';

    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('usermanagementCtrl', usermanagementCtrl);

    usermanagementCtrl.$inject = [
        'userManagementService', 'rolesService', 'userModel', 'authService', 'utility',
        'userManagement', 'accountLocksService'
    ];

    function usermanagementCtrl(userManagementService, rolesService, userModel, authService, utility,
        userManagement, accountLocksService) {
        var vm = this;
        vm.name = 'usermanagementCtrl';

        vm.availableRoles = [];
        vm.newUser = new userModel();
        vm.users = [];

        vm.editUser = function(user) {
            userManagement.editUser(user).result.then(function() {
                vm.getAllUsers();
            });
        }

        vm.createUser = function () {
            utility.confirm("Are you sure you want to create this user?").result.then(function() {
                authService.saveRegistration(vm.newUser).then(function () {
                    //todo consider sending back in payload so we dont have to do another call to refresh the screen
                    vm.getAllUsers();
                    vm.newUser = new userModel();
                });
            });
        }

        vm.deleteUser = function(id) {
            utility.confirm("Are you sure you want to delete this user?").result.then(function() {
                userManagementService.deleteUser(id).then(function() {
                    _.remove(vm.users, { 'id': id });
                });
            });
        }

        vm.init = function() {
            vm.getAllUsers();

            rolesService.getAllRoles().then(function(result) {
                vm.availableRoles = result.data;
            });
        }

        vm.getAllUsers = function() {
            userManagementService.getAllUsers().then(function(result) {
                vm.users = result.data;
            });
        }

        vm.unlock = function (user) {
            utility.confirm("Are you sure you want to unlock this user?").result.then(function() {
                accountLocksService.unlock(user.id).then(function () {
                    user.isLocked = false;
                });
            });
        }

        vm.lock = function (user) {
            utility.confirm("Are you sure you want to lock this user?").result.then(function () {
                accountLocksService.lock(user.id).then(function () {
                    user.isLocked = true;
                });
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('rolemanagementCtrl', rolemanagementCtrl);

    rolemanagementCtrl.$inject = ['rolesService', 'utility'];

    function rolemanagementCtrl(rolesService, utility) {
        var vm = this;
        vm.roles = [];
        vm.newRole = "";

        vm.deleteRole = function (id) {
            utility.confirm("Are you sure you want to delete this role?").result.then(function() {
                rolesService.deleteRole(id).then(function () {
                    vm.getRoles();
                });
            });
        }

        vm.createRole = function () {
            utility.confirm("Are you sure you want to create this role?").result.then(function() {
                rolesService.createRole(vm.newRole).then(function (result) {
                    vm.newRole = "";
                    vm.roles = result.data;
                });
            });
        }

        vm.getRoles = function() {
            rolesService.getAllRoles().then(function(result) {
                vm.roles = result.data;
            });
        }

        vm.init = function() {
            vm.getRoles();
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('confirmCtrl', confirmCtrl);

    confirmCtrl.$inject = ['$modalInstance', 'message'];

    function confirmCtrl($modalInstance, message) {
        var vm = this;
        vm.message = message;
        vm.$modalInstance = $modalInstance;
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('editUserCtrl', editUserCtrl);

    editUserCtrl.$inject = ['$modalInstance', 'user', 'userManagementService', 'rolesService'];

    function editUserCtrl($modalInstance, user, userManagementService, rolesService) {
        var vm = this;
        vm.user = user;
        vm.availableRoles = [];
        vm.$modalInstance = $modalInstance;

        vm.updateUser = function() {
            userManagementService.updateUser(vm.user).then(function() {
                $modalInstance.close();
            });
        }

        vm.init = function() {
            rolesService.getAllRoles().then(function (result) {
                vm.availableRoles = result.data;
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('passwordRecoveryCtrl', passwordRecoveryCtrl);

    passwordRecoveryCtrl.$inject = ['$modalInstance', 'passwordRecoveryService', 'utility', 'messaging', 'vcAppConstants'];

    function passwordRecoveryCtrl($modalInstance, passwordRecoveryService, utility, messaging, vcAppConstants) {
        var vm = this;
        vm.$modalInstance = $modalInstance;
        vm.email = "";

        vm.resetPassword = function() {
            passwordRecoveryService.resetPassword(vm.email).then(function () {
                $modalInstance.close('Temporary password has been sent to ' + vm.email);
            }, function() {
                messaging.publish(vcAppConstants.vcErrorNotificationEvt, { message: 'Failed to reset password' }); //todo add interceptor to catch all errors
            });
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('signupCtrl', signupCtrl);

    signupCtrl.$inject = ['utility', 'authService', '$state'];

    function signupCtrl(utility, authService, $state) {
        var vm = this;
        vm.init = init;
        vm.createUser = createUser;

        function init() {
            vm.newUser = {};
        }

        function createUser() {
            vm.inProgress = true;

            utility.confirm("Are you sure you want to create this user?")
                .result.then(
                    function onConfirm() {
                        authService.saveAnonymousRegistration(vm.newUser)
                            .then(
                            function onSuccess() {
                                $state.transitionTo('login', {}, { reload: true, inherit: false, notify: true, location: "replace" });
                            },
                            function onError() {
                                vm.inProgress = false;
                            });
                    },
                    function onReject() {
                        vm.inProgress = false;
                    });
        }

        init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('categoryLookupManagementCtrl', categoryLookupManagementCtrl);

    categoryLookupManagementCtrl.$inject = ['lookups', 'lookupsService', 'categoryTypesFactory', 'categoryFactory'];

    function categoryLookupManagementCtrl(lookups, lookupsService, categoryTypesFactory, categoryFactory) {
        var vm = this;

        vm.init = init;
        vm.populateLookupValues = populateLookupValues;
        vm.createCategory = createCategory;
        vm.populateCategories = populateCategories;
        vm.deleteCategory = deleteCategory;
        vm.selectCategoryType = selectCategoryType;
        vm.addCategory = addCategory;
        vm.addLookupAlias = addLookupAlias;

        function init() {
            vm.populateLookupValues();
            vm.populateCategories();
        }

        function addCategory() {
            categoryFactory.addCategory().result.then(function() {
                vm.populateCategories();
            });
        }

        function deleteCategory(id) {
            lookupsService.deleteCategory(id).then(function() {
                vm.populateCategories();
            });
        }

        function populateLookupValues() {
            lookupsService.getLookupValues().then(function (result) {
                vm.categoryTypes = result.data; //todo rename
            });
        }

        function populateCategories() {
            lookupsService.getCategories().then(function(result) {
                vm.categories = result.data;
            });
        }

        function createCategory() {
            lookupsService.createCategory(vm.newCategory).then(function () {
                vm.newCategory = {};
                vm.populateCategories();
            });
        }

        function selectCategoryType(type) {
            if (angular.isUndefined(type.originalName)) {
                type.originalName = type.name;
            }

            if (!type.isEdit) {
                type.isEdit = true;
            }
        }

        function addLookupAlias() {
            var payload = {
                name: vm.newLookupAlias,
                active: vm.newLookupAliasIsActive,
                lookupValueId: vm.selectedLookupValue.id
            };

            lookupsService.addLookupAlias(payload).then(function () {
                console.log('success');
            });
        }

        vm.init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('createCategoryTypeCtrl', createCategoryTypeCtrl);

    createCategoryTypeCtrl.$inject = ['lookupsService', '$modalInstance'];

    function createCategoryTypeCtrl(lookupsService, $modalInstance) {
        var vm = this;
        vm.init = init;
        vm.createCategoryType = createCategoryType;
        vm.$modalInstance = $modalInstance;

        function init() {
            vm.lookupsService = lookupsService;
        }

        function createCategoryType() {
            vm.inProgress = true;
            vm.lookupsService.createCategoryType(vm.categoryTypeName)
                .then(
                    function onSuccess() {
                        $modalInstance.close();
                    },
                    function onError() {
                        vm.inProgress = false;
                    });
        }

        init();
    }
}(angular));

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('editCategoryLookupCtrl', editCategoryLookupCtrl);

    editCategoryLookupCtrl.$inject = ['lookupsService', 'category', 'categoryTypes', '$modalInstance'];

    function editCategoryLookupCtrl(lookupsService, category, categoryTypes, $modalInstance) {
        var vm = this;
        vm.init = init;
        vm.editCategory = editCategory;

        function init() {
            vm.category = category;
            vm.$modalInstance = $modalInstance;
            vm.lookupsService = lookupsService;
            vm.statuses = [true, false];
            vm.categoryTypes = categoryTypes;
        }

        function editCategory() {
            lookupsService.editCategory(vm.category).then(function() {
                $modalInstance.close();
            });
        }

        init();
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('addCategoryCtrl', addCategoryCtrl);

    addCategoryCtrl.$inject = ['$modalInstance','lookupsService'];

    function addCategoryCtrl($modalInstance, lookupsService) {
        var vm = this;
        vm.addLookupValue = addLookupValue;
        vm.removeLookupValue = removeLookupValue;
        vm.createCategory = createCategory;
        vm.init = init;

        function init() {
            vm.newCategory = {};
            vm.newCategory.values = [];
            vm.newLookupValueIsActive = true;
            vm.newLookupValue = '';
        }

        function removeLookupValue(lookupValue) {
            vm.newCategory.values = _.remove(vm.newLookupValues, function (n) {
                return n.name != lookupValue.name;
            });
        }

        function addLookupValue() {
            if (vm.newCategory.values.indexOfByProperty('name', vm.newLookupValue) < 0) {
                vm.newCategory.values.push({ name: vm.newLookupValue, active: vm.newLookupValueIsActive });
                vm.newLookupValue = '';
                vm.newLookupValueIsActive = true;
            }
        }

        function createCategory() {
            lookupsService.createCategory(vm.newCategory).then(function () {
                $modalInstance.close();
            });
        }

        vm.init();
    }
}(angular));