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
        'userManagement'
    ];

    function usermanagementCtrl(userManagementService, rolesService, userModel, authService, utility,
        userManagement) {
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

    signupCtrl.$inject = ['utility', 'authService'];

    function signupCtrl(utility, authService) {
        var vm = this;
        vm.init = init;
        vm.createUser = createUser;

        function init() {
            vm.newUser = {};
        }

        function createUser() {
            utility.confirm("Are you sure you want to create this user?").result.then(function () {
                authService.saveAnonymousRegistration(vm.newUser).then(function () {
                    console.log("success");
                });
            });
        }

        init();
    }
}(angular));