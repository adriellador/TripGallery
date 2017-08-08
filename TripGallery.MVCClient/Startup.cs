using Microsoft.Owin;
using Microsoft.Owin.Security;
using Owin;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using TripGallery.MVCClient.Helpers;


[assembly: OwinStartup(typeof(TripGallery.MVCClient.Startup))]
namespace TripGallery.MVCClient
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
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
                    }
                }
            });
        }
    }
}
