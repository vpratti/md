
var app = angular.module('VirtualClarityApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap']);

app.config(function ($routeProvider) {

    $routeProvider.when("/login", {
        controller: "loginController as vm",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/dashboard", {
        controller: "dashboardCtrl as vm",
        templateUrl: "/app/views/dashboard.html"
    });

    $routeProvider.when("/usermanagement", {
        controller: "usermanagementCtrl as vm",
        templateUrl: "/app/views/usermanagement.html"
    });

    $routeProvider.otherwise({ redirectTo: "/login" });

});

//var serviceBase = 'http://localhost:26264/';
var serviceBase = 'http://ngauthenticationapi.azurewebsites.net/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


