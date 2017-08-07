(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("tokenContainer",
                  [tokenContainer])

    function tokenContainer() {
   
        var container = {
            token: ""
        };
         
        var setToken = function (token) {
            container.token = token;
        };

        var getToken = function () {
            debugger;
            if (container.token === "") {
                if (localStorage.getItem("access_token") === null) {

                    // get the token through an in app, non redirection based
                    // flow: resource owner pasword credentials.
                    // => we show the login screen

                    window.location.href = "#/login";


                    // get token using implicit flow
                    //var url = "https://localhost:44317/identity/connect/authorize?" +
                    //    "client_id=tripgalleryimplicit&" +
                    //    "redirect_uri=" + encodeURI("https://localhost:44316/callback.html") + "&" +
                    //    "response_type=token&" +
                    //    "scope=gallerymanagement";

                    //window.location = url;
                }
                else {
                    // set the token in localstorage
                    setToken(localStorage["access_token"]);
                }
            }
            return container;
        };

        // return value to call when calling the API 
        return {
            getToken: getToken
        };
        

    };

})();