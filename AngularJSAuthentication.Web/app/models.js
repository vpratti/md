(function (angular) {
    'use strict';

    angular
       .module('VirtualClarityApp')
       .factory('UserModel', function () { return UserModel; });

    function UserModel() {
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