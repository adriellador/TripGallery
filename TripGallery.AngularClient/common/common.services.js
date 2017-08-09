(function () {
    "use strict";

    angular.module("common.services",
                    ["ngResource"])
      	.constant("appSettings",
        {
            tripGalleryAPI: "https://localhost:44315" 
        });

    angular.module("common.services")
    .factory("OidcManager", function () {
        debugger;
        var config = {
            client_id: "tripgalleryimplicit",
            redirect_uri: "https://localhost:44316/callback.html",
            response_type: "id_token",
            scope: "openid profile",
            authority: "https://localhost:44317/identity"
        }

        var mgr = new OidcTokenManager(config);

        return {
            OidcTokenManager: function () {
                return mgr;
            } 
        }
    });
}());
