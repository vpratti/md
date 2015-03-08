(function (angular) {
    'use strict';

    var vcAppConstants = {
        vcErrorNotificationEvt: 'vcErrorNotification::evt'
    };

    angular
        .module('VirtualClarityApp')
        .constant('vcAppConstants', vcAppConstants);

}(angular));