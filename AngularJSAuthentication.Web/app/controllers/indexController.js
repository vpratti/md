'use strict';
app.controller('indexController', [
    '$scope', '$location', 'authService', 'ngAuthSettings', function($scope, $location, authService, ngAuthSettings) {

        console.log($location);

        $scope.logOut = function() {
            authService.logOut();
            $location.path('/');
        }

        $scope.authentication = authService.authentication;

    }
]);