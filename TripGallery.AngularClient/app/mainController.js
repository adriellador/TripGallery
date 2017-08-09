(function () {
    "use strict";
    angular
        .module("tripGallery")
        .controller("mainController",
        ["OidcManager", MainController]);

    function MainController(OidcManager) {
        debugger;
        var vm = this;

        vm.mgr = OidcManager.OidcTokenManager();

        vm.logout = function () {
            vm.mgr.removeToken();
            window.location = "index.html";
        };

        // no id token or expired => redirect to get one
        if (vm.mgr.expired) {
            vm.mgr.redirectForToken();
        }
    }

}());