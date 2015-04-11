
var app = angular.module('VirtualClarityApp', ['ui.router', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap',
    'ui.select', 'ngSanitize', 'angularUtils.directives.uiBreadcrumbs']);

app.config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('login', {
            url: "/",
            templateUrl: "app/views/login.html",
            controller: "loginController as vm",
            onEnter: [
                '$window', 'authService', function($window, authService) {
                    if (authService.authentication.isAuth) {
                        $window.location.href = "#/dashboard";
                    }
                }
            ]
        })
        .state('signup', {
            url: "/signup",
            templateUrl: "app/views/signup.html",
            controller: "signupCtrl as vm"
        })
        .state('dashboard', {
            url: "/dashboard",
            templateUrl: "app/views/dashboard.html",
            controller: "dashboardCtrl as vm",
            data: {
                displayName: 'Home'
            }
        })
        .state('usermanagement', {
            url: "/usermanagement",
            templateUrl: "app/views/usermanagement.html",
            controller: "usermanagementCtrl as vm",
            data: {
                displayName: 'Manage Users'
            },
            onEnter: [
                '$window', 'authService', function($window, authService) {
                    if (!authService.authentication.isAdmin) {
                        $window.location.href = "#/dashboard";
                    }
                }
            ]
        })
        .state('rolemanagement', {
            url: "/rolemanagement",
            templateUrl: "app/views/rolemanagement.html",
            controller: "rolemanagementCtrl as vm",
            data: {
                displayName: 'Manage Roles'
            }
        })
        .state('categoryLookupManagement', {
            url: "/categoryLookupManagement",
            templateUrl: "app/views/categoryLookupManagement.html",
            controller: "categoryLookupManagementCtrl as vm",
            data: {
                displayName: 'Manage Category Lookups'
            }
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


