'use strict';
app.controller('loginController', [
    '$scope', '$location', 'authService', 'ngAuthSettings', 'utility', 'passwordRecovery',
    function ($scope, $location, authService, ngAuthSettings, utility, passwordRecovery) {

        $scope.loginData = {
            userName: "",
            password: "",
            useRefreshTokens: false
        };

        $scope.message = "";

        $scope.login = function() {

            authService.login($scope.loginData).then(function(response) {

                    $location.path('/dashboard');

                },
                function(err) {
                    $scope.message = err.error_description;
                });
        };

        $scope.authExternalProvider = function(provider) {

            var redirectUri = location.protocol + '//' + location.host + '/authcomplete.html';

            var externalProviderUrl = ngAuthSettings.apiServiceBaseUri + "api/Account/ExternalLogin?provider=" + provider
                + "&response_type=token&client_id=" + ngAuthSettings.clientId
                + "&redirect_uri=" + redirectUri;
            window.$windowScope = $scope;

            var oauthWindow = window.open(externalProviderUrl, "Authenticate Account", "location=0,status=0,width=600,height=750");
        };

        $scope.authCompletedCB = function(fragment) {

            $scope.$apply(function() {

                if (fragment.haslocalaccount == 'False') {

                    authService.logOut();

                    authService.externalAuthData = {
                        provider: fragment.provider,
                        userName: fragment.external_user_name,
                        externalAccessToken: fragment.external_access_token
                    };

                    $location.path('/associate');

                } else {
                    //Obtain access token and redirect to orders
                    var externalData = { provider: fragment.provider, externalAccessToken: fragment.external_access_token };
                    authService.obtainAccessToken(externalData).then(function(response) {

                            $location.path('/dashboard');

                        },
                        function(err) {
                            $scope.message = err.error_description;
                        });
                }

            });
        };

        $scope.forgotPassword = function() {
            utility.confirm("Did you want a new password to be sent to your registered e-mail?")
                .result.then(function() {
                    passwordRecovery.recoverPassword();
                });
        };
    }
]);
