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

            
        }
    }
}());
