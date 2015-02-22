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

    usermanagementCtrl.$inject = ['userManagementService', 'rolesService', 'UserModel', 'authService'];

    function usermanagementCtrl(userManagementService, rolesService, UserModel, authService) {
        var vm = this;
        vm.name = 'usermanagementCtrl';

        vm.availableRoles = [];
        vm.newUser = new UserModel();
        vm.users = [];

        vm.createUser = function () {
            authService.saveRegistration(vm.newUser);
        };

        vm.init = function() {
            userManagementService.getAllUsers().then(function(result) {
                vm.users = result.data;
            });


            rolesService.getAllRoles().then(function (result) {
                vm.availableRoles = result.data;
            });
        };

        vm.init();
    }
}(angular));