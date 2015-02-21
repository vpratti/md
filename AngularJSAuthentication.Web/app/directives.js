(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .directive('vcHeaderDropdown', vcHeaderDropdown);

    function vcHeaderDropdown() {
        var directive = {
            restrict: 'A',
            link: linkFunc
        };

        return directive;

        function linkFunc(scope, element) {

            element.on('click', function () {
                var parent = element.parent()[0];
                if (parent.className == "dropdown") {
                    parent.className += " open";
                } else {
                    parent.className = "dropdown";
                }
            });
        }
    }


}(angular));