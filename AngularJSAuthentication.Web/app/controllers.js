﻿(function (angular) {
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

    usermanagementCtrl.$inject = ['userManagementService'];

    function usermanagementCtrl(userManagementService) {
        var vm = this;
        vm.name = 'usermanagementCtrl';

        vm.availableRoles = ["admin", 'normal', 'other'];
        vm.roles = [];
        vm.users = [];

        var init = function() {
            userManagementService.getAllUsers().then(function(result) {
                vm.users = result.data;
                console.log(result.data);
            });
        };

        init();
    }
}(angular));