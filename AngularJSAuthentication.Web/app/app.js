﻿
var app = angular.module('VirtualClarityApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap', 'ui.select', 'ngSanitize']);

app.config(function ($routeProvider) {

    $routeProvider.when("/login", {
        controller: "loginController as vm",
        templateUrl: "app/views/index2.html"
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

var serviceBase = 'http://localhost:8080/';
app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


