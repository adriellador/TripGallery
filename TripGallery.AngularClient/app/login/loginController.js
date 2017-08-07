(function () {
    "use strict";
    angular
        .module("tripGallery")
        .controller("loginController",
                     ["$http", LoginController]);

    function LoginController($http) {
        var vm = this;

        vm.loginError = "";
        vm.credentials = {
            username: "",
            password: ""
        };

        vm.submit = function ()
        {
            vm.loginError = "";

            // get the token, using the resource owner password
            // credentials flow

            // the message body
            var dataForBody = "grant_type=password&" +
                "username=" + encodeURI(vm.credentials.username) + "&" +
                "scope=" + encodeURI(vm.credentials.password) + "&" +
                "scope=" + encodeURI("gallerymanagement");

            var encodedClientIdAndSecret = btoa("tripgalleryropc:myrandomclientsecret");

            var messageHeaders = {
                "Content-Type": "application/x-www-form-urlencoded",
                "Authorization": "Basic " + encodedClientIdAndSecret
            };

            return $http({
                method: "POST",
                url: "https://localhhost:44317/identity/connect/token",
                headers: messageHeaders,
                data: dataForBody
            }).success(function (data) {
                debugger;

                localStorage["access_token"] = data.access_token;

                vm.credentials.username = "";
                vm.credentials.password = "";

                window.location = window.location.protocol + "//" + window.location.host + "/";
            }).error(function (data) {
                vm.loginError = data.error;

                vm.credentials.username = "";
                vm.credentials.password = "";
            })
            
        }
    }
}());
