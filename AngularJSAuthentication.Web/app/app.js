
var app = angular.module('VirtualClarityApp', ['ui.router', 'LocalStorageModule', 'angular-loading-bar', 'ui.bootstrap',
    'ui.select', 'ngSanitize', 'angularUtils.directives.uiBreadcrumbs', 'angularBootstrapNavTree']);

app.config(function($stateProvider, $urlRouterProvider) {

    $stateProvider
        .state('login', {
            url: "/",
            templateUrl: "app/views/login.html",
            controller: "loginCtrl as vm",
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
                displayName: 'Users'
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
                displayName: 'Roles'
            },
            onEnter: [
                '$window', 'authService', function($window, authService) {
                    if (!authService.authentication.isAdmin) {
                        $window.location.href = "#/dashboard";
                    }
                }
            ]
        })
        .state('categoryManagement', {
            url: "/categoryManagement",
            templateUrl: "app/views/categoryManagement.html",
            controller: "categoryManagementCtrl as vm",
            data: {
                displayName: 'Lookups'
            },
            onEnter: [
                '$window', 'authService', function($window, authService) {
                    if (!authService.authentication.isAdmin) {
                        $window.location.href = "#/dashboard";
                    }
                }
            ]
        })
        .state('timeline', {
            url: "/timeline",
            templateUrl: "app/views/timeline.html",
            controller: "timelineCtrl as vm",
            data: {
                displayName: 'Timeline'
            },
            resolve: {
                parentTimeframes: ['timelineService', '$window', function (timelineService, $window) {
                    return timelineService.getTimeframes().error(function (result, event) {
                        if (event == 401) {
                            $window.location.href = "/";
                        }
                    });
                }]
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


