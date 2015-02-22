(function(angular) {
    'use strict';

    angular
        .module('VirtualClarityApp')
        .directive('vcHeaderDropdown', vcHeaderDropdown);

    vcHeaderDropdown.$inject = ['$document'];

    function vcHeaderDropdown($document) {
        var directive = {
            restrict: 'A',
            link: linkFunc
        };

        return directive;

        function linkFunc(scope, element) {
            var parent = element.parent()[0];

            element.on('click', function () {
                if (isClosed(parent)) {
                    parent.className += ' open';
                } else {
                    parent.className = parent.className.replace('open', '').trim();
                }
            });

            $document.on('click', function (event) {
                if (event.target.className.indexOf('dropdown') < 0) {
                    parent.className = parent.className.replace('open', '').trim();
                }
            });
        }

        function isClosed(parent) {
            return parent.className.indexOf('open') < 0;
        }
    }


}(angular));