(function () {
    "use strict";
    angular
        .module("tripGallery")
        .controller("tripIndexController",
                     ["tripResource", "$filter", "OidcManager",
                         TripIndexController]);

    function TripIndexController(tripResource, $filter) {
        var vm = this;

        vm.mgr = OidcManager.OidcManager();

        vm.getUserIdentifier = function () {
            if (!vm.mgr.expired) {
                return "https://tripcompanysts/identity" + vm.mgr.profile.sub;
            }
            else {
                return "";
            }
        }

        vm.switchPrivacyLevel = function (tripId, OidcManager)
        {         
            // create a JsonPatchDocument to change the privacy level,
            // and send it to the API through the tripResource.

            // get the trip to patch, using injected $filter dependency, and the built-in filter
            // named 'filter' (https://docs.angularjs.org/api/ng/filter) 
            
            var tripToPatch = $filter('filter')(vm.trips, { id: tripId }, true)[0];
         
            // only *really* switch the value after
            // the patch was succesful => success callback.
                        
            // patch the switched value
            tripToPatch.$patch(
                 {
                     tripId: tripId
                 },
                 // success
                    function (patchedTrip) {
                        debugger;
                        tripToPatch = patchedTrip;
                    },
                 // failure
                 null); 
 
        }

        vm.loadTrips = function()
        {
            tripResource.query(
                   // no query params
                   null,
                   // success
                   function (data) {
                       vm.trips = data;
                   },
                   // failure
                   null);
        };
   
        vm.loadTrips();       

    }
}());
