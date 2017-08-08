using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer3.Core.Models;
using TripGallery;

namespace TripCompany.IdentityServer.Config
{
    public static class Clients
    {
        // AllowedScopes By default a client has no access to any scopes - either specify the scopes explicitly here(recommended) - or set AllowAccessToAllScopes to true.
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "tripgalleryclientcredentials",
                    ClientName = "Trip Gallery (Client Credentials)",
                    Flow = Flows.ClientCredentials,
                    ClientSecrets = new List<Secret>()
                    {
                        new Secret(Constants.TripGalleryClientSecret.Sha256())
                    },
                    AllowAccessToAllScopes = true,
                    AllowedScopes = new List<string>
                    {
                        "gallerymanagement"
                    }
                },
                 new Client
                {
                    ClientId = "tripgalleryimplicit",
                    ClientName = "Trip Gallery (Implicit)",
                    Flow = Flows.Implicit,
                    AllowAccessToAllScopes = true,
                    AllowedScopes = new List<string>
                    {
                        "gallerymanagement"
                    },
                    RedirectUris = new List<string>
                    {
                        Constants.TripGalleryAngular + "callback.html"
                    }
                },
                 new Client
                 {
                     ClientId = "tripgalleryauthcode",
                     ClientName = "Trip Gallery (Authirization Code)",
                     Flow = Flows.AuthorizationCode,
                     AllowAccessToAllScopes = true,
                     AllowedScopes = new List<string>
                     {
                        "gallerymanagement"
                     },
                     RedirectUris = new List<string>
                     {
                        Constants.TripGalleryMVCSTSCallback
                     },
                     ClientSecrets = new List<Secret>()
                     {
                         new Secret(Constants.TripGalleryClientSecret.Sha256())

                     }
                 },
                 new Client
                 {
                     ClientId = "tripgalleryropc",
                     ClientName = "Trip Gallery (REsource Owner Password Credentials)",
                     Flow = Flows.ResourceOwner,
                     AllowedScopes = new List<string>
                     {
                         "gallerymanagement"
                     },
                     AllowAccessToAllScopes = true,
                     ClientSecrets = new List<Secret>()
                     {
                         new Secret(Constants.TripGalleryClientSecret.Sha256())
                     }

                 },
                 new Client
                 {
                     ClientId = "tripgalleryhybrid",
                     ClientName = "Trip Gallery (Hybrid)",
                     Flow = Flows.Hybrid,
                     AllowAccessToAllScopes =true,
                     RedirectUris = new List<string>
                     {
                         Constants.TripGalleryMVC
                     }
                 }
            };
        }
    }
}
