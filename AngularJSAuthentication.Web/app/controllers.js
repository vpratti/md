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

(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('usermanagementCtrl', usermanagementCtrl);

    usermanagementCtrl.$inject = ['userManagementService', 'rolesService', 'UserModel', 'authService', 'utility'];

    function usermanagementCtrl(userManagementService, rolesService, UserModel, authService, utility) {
        var vm = this;
        vm.name = 'usermanagementCtrl';

        vm.availableRoles = [];
        vm.newUser = new UserModel();
        vm.users = [];

        vm.editUser = function(id) {
            //todo
        }

        vm.createUser = function () {
            authService.saveRegistration(vm.newUser).then(function () {
                //todo consider sending back in payload so we dont have to do another call to refresh the screen
                vm.getAllUsers();
                vm.newUser = new UserModel();
            });
        }

        vm.deleteUser = function (id) {
            utility.confirm("Are you sure you want to delete this user?").result.then(function() {
                userManagementService.deleteUser(id).then(function () {
                    _.remove(vm.users, { 'id': id });
                });
            });
        }

        vm.init = function() {
            vm.getAllUsers();

            rolesService.getAllRoles().then(function (result) {
                vm.availableRoles = result.data;
            });
        }

        vm.getAllUsers = function() {
            userManagementService.getAllUsers().then(function (result) {
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
        .controller('ConfirmCtrl', ConfirmCtrl);

    ConfirmCtrl.$inject = ['$modalInstance', 'message'];

    function ConfirmCtrl($modalInstance, message) {
        var vm = this;
        vm.message = message;
        vm.$modalInstance = $modalInstance;
    }
}(angular));