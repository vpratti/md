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