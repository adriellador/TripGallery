using Microsoft.Owin;
using Owin;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using TripGallery.MVCClient.Helpers;


[assembly: OwinStartup(typeof(TripGallery.MVCClient.Startup))]
namespace TripGallery.MVCClient
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();


            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationOptions
            {
                ClientId = "tripgalleryhybrid",
                Authority = Constants.TripGallerySTS,
                RedirectUri = Constants.TripGalleryMVC,
                SignInAsAuthenticationType = "Cookies",
                ResponseType = "code id_token",
                Scope = "openid profile",
                Notifications = new Microsoft.Owin.Security.OpenIdConnect.OpenIdConnectAuthenticationNotifications()
                {
                    SecurityTokenValidated = async n =>
                    {
                        TokenHelper.DecodeAndWrite(n.ProtocolMessage.IdToken);

                        var subClaim = n.AuthenticationTicket.Identity.FindFirst(IdentityModel.JwtClaimTypes.Subject);

                        var nameClaim = new Claim(IdentityModel.JwtClaimTypes.Name, Constants.TripGalleryIssuerUri + subClaim.Value);

                        var givenNameClaim = n.AuthenticationTicket.Identity.FindFirst(IdentityModel.JwtClaimTypes.GivenName);
                        var familyNameClaim = n.AuthenticationTicket.Identity.FindFirst(IdentityModel.JwtClaimTypes.FamilyName);

                        var newClaimsIdentity = new ClaimsIdentity(
                            n.AuthenticationTicket.Identity.AuthenticationType,
                            IdentityModel.JwtClaimTypes.Name,
                            IdentityModel.JwtClaimTypes.Role);

                        if(nameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(nameClaim);
                        }

                        if(givenNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(givenNameClaim);
                        }

                        if(familyNameClaim != null)
                        {
                            newClaimsIdentity.AddClaim(familyNameClaim);
                        }

                        n.AuthenticationTicket = new Microsoft.Owin.Security.AuthenticationTicket(newClaimsIdentity, n.AuthenticationTicket.Properties);

                    }
                }
            });
        }
    }
}
