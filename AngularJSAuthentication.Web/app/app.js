
var app = angular.module('VirtualClarityApp', ['ui.router', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap',
    'ui.select', 'ngSanitize']);
var serviceBase = 'http://localhost:8080/';

app.config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('login', {
            url: "/",
            templateUrl: "app/views/login.html",
            controller: "loginController as vm",
            onEnter: function ($state, $stateParams, authService) {
                if (authService.authentication.isAuth) {
                    $state.transitionTo(
                        'dashboard',
                        $stateParams,
                        {
                            reload: true,
                            inherit: false,
                            notify: true,
                            location: "replace"
                        });
                }
            }
        })
        .state('dashboard', {
            url: "/dashboard",
            templateUrl: "app/views/dashboard.html",
            controller: "dashboardCtrl as vm"
        })
        .state('usermanagement', {
            url: "/usermanagement",
            templateUrl: "app/views/usermanagement.html",
            controller: "usermanagementCtrl as vm"
        })
        .state('rolemanagement', {
            url: "/rolemanagement",
            templateUrl: "app/views/rolemanagement.html",
            controller: "rolemanagementCtrl as vm"
        });

    $urlRouterProvider.otherwise('/');
});


app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


