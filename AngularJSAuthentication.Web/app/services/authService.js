'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', 'messaging', 'vcAppConstants',
    function ($http, $q, localStorageService, ngAuthSettings, messaging, vcAppConstants) {

    var serviceBase = ngAuthSettings.apiServiceBaseUri;
    var authServiceFactory = {};

    var authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };

    var externalAuthData = {
        provider: "",
        userName: "",
        externalAccessToken: ""
    };

    var saveRegistration = function (registration) {
        return $http.post(ngAuthSettings.apiServiceBaseUri + 'api/account/register', registration).then(function (response) {
            return response;
        });
       
    };

    var login = function (loginData) {

        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        if (loginData.useRefreshTokens) {
            data = data + "&client_id=" + ngAuthSettings.clientId;
        }

        var deferred = $q.defer();

        $http.post(ngAuthSettings.apiServiceBaseUri + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            if (loginData.useRefreshTokens) {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: response.refresh_token, useRefreshTokens: true });
            }
            else {
                localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, refreshToken: "", useRefreshTokens: false });
            }

            //todo optimize logic for retreiving user roles
            $http.get(ngAuthSettings.apiServiceBaseUri + 'api/Roles/GetUserRoles?username=' + loginData.userName).success(function (result) {
                var authorizationData = localStorageService.get('authorizationData');
                authorizationData.userRoles = result;
                localStorageService.set('authorizationData', authorizationData);
                fillAuthData();
                deferred.resolve(response);
            }).error(function(err) {
                logOut();
                messaging.publish(vcAppConstants.vcErrorNotificationEvt, { message: 'There was a problem logging in' }); //todo add interceptor to catch all errors
                deferred.reject(err);
            });

        }).error(function (err) {
            logOut();
            messaging.publish(vcAppConstants.vcErrorNotificationEvt, { message: 'Your username or password is incorrect' }); //todo add interceptor to catch all errors
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var logOut = function () {

        localStorageService.remove('authorizationData');

        authentication.isAuth = false;
        authentication.userName = "";
        authentication.useRefreshTokens = false;
        authentication.userRoles = [];
        authentication.isAdmin = false;
    };

    var fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            authentication.isAuth = true;
            authentication.userName = authData.userName;
            authentication.useRefreshTokens = authData.useRefreshTokens;
            authentication.userRoles = authData.userRoles;
            authentication.isAdmin = _.findIndex(authData.userRoles, { name: 'admin' }) > -1;
        }

    };

    var refreshToken = function() {
        var deferred = $q.defer();

        var authData = localStorageService.get('authorizationData');

        if (authData && authData.useRefreshTokens) {

            var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + ngAuthSettings.clientId;

            localStorageService.remove('authorizationData');

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function(response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

                deferred.resolve(response);

            }).error(function(err) {
                logOut();
                deferred.reject(err);
            });
        }

        return deferred.promise;
    };

    var obtainAccessToken = function (externalData) {

        var deferred = $q.defer();

        $http.get(serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

            authentication.isAuth = true;
            authentication.userName = response.userName;
            authentication.useRefreshTokens = false;

            deferred.resolve(response);

        }).error(function (err) {
            logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    authServiceFactory.saveRegistration = saveRegistration;
    authServiceFactory.login = login;
    authServiceFactory.logOut = logOut;
    authServiceFactory.fillAuthData = fillAuthData;
    authServiceFactory.authentication = authentication;
    authServiceFactory.refreshToken = refreshToken;

    authServiceFactory.obtainAccessToken = obtainAccessToken;
    authServiceFactory.externalAuthData = externalAuthData;

    return authServiceFactory;
}]);