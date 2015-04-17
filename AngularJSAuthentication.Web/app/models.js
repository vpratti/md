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

(function () {
    'use strict';

    Array.prototype.indexOfByProperty = indexOfByProperty;

    function indexOfByProperty(key, value) {
        for (var i = 0; i < this.length; i++) {
            if (this[i][key] == value) {
                return i;
            }
        }
        return -1;
    }
}());