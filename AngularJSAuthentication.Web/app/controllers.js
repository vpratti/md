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
        .controller('usermanagementCtrl', dashboardCtrl);

    function dashboardCtrl() {
        var vm = this;
        vm.name = 'usermanagementCtrl';

        vm.availableRoles = ["admin", 'normal', 'other'];
        vm.roles = [];
    }
}(angular));