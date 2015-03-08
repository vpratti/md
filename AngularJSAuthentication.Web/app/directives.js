(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .directive('vcRolesUnique', vcRolesUnique);

    function vcRolesUnique() {
        var directive = {
            restrict: 'A',
            require: '^ngModel',
            link: linkFunc
        };

        return directive;

        function linkFunc(scope, elem, attrs, ngModel) {

            ngModel.$parsers.push(function(value) {
                var unique = _.uniq(value, 'id');
                ngModel.$viewValue = unique;
                ngModel.$render();
                return unique;
            });
        }
    }
}(angular));

(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .directive('vcErrorNotification', vcErrorNotification);

    vcErrorNotification.$inject = ['messaging', 'vcAppConstants'];

    function vcErrorNotification(messaging, vcAppConstants) {
        var directive = {
            restrict: 'E',
            templateUrl: '/app/views/errorNotification.html',
            controller: controller
        };

        return directive;

        function controller($scope) {
            $scope.message = '';

            $scope.dismiss = function() {
                $scope.message = '';
            }

            messaging.subscribe(vcAppConstants.vcErrorNotificationEvt, function (event, data) {
                $scope.message = data.message;
            });
        }
    }
}(angular));