(function (angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .controller('dashboardCtrl', dashboardCtrl);

    dashboardCtrl.$inject = ['ordersService'];

    function dashboardCtrl(ordersService) {
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

    }
}(angular));