(function (angular) {
    'use strict';

    angular
       .module('VirtualClarityApp')
       .factory('userModel', function () { return userModel; });

    function userModel() {
        var model = {
            userName: '',
            roles: [],
            password: '',
            confirmPassword: '',
            setData: setData
        };

        return model;
    }

    function setData(userModel) {
        _.extend(this, userModel);
    }

}(angular));