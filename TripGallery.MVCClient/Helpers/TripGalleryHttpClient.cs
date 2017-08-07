using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TripGallery.MVCClient.Helpers
{
    public static class TripGalleryHttpClient
    {

        public static HttpClient GetClient()
        { 
            HttpClient client = new HttpClient();

            // client credentials flow
            // var accessToken = RequestAccessTokenClientCredentials();

            //Authorization code flow
            var accessToken = RequestAccessTokenAuthorizationCode();
            if(accessToken != null)
            {
                client.SetBearerToken(accessToken);
            }
          
           
            client.BaseAddress = new Uri(Constants.TripGalleryAPI); 

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public static string RequestAccessTokenAuthorizationCode()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get("TripGalleryCookie");
            if(cookie != null && cookie["access_token"] != null)
            {
                return cookie["access_token"];
            }

            var authorizeRequest = new AuthorizeRequest(Constants.TripGallerySTSAuthorizationEndpoint);

            var state = HttpContext.Current.Request.Url.OriginalString;
            var url = authorizeRequest.CreateAuthorizeUrl("tripgalleryauthcode", "code", "gallerymanagement", Constants.TripGalleryMVCSTSCallback, state);

            HttpContext.Current.Response.Redirect(url);
            return null;
        }

        public static string RequestAccessTokenClientCredentials()
        {
            var cookie = HttpContext.Current.Request.Cookies.Get("TripGalleryCookie");

            if(cookie != null && cookie["access_token"] != null)
            {
                return cookie["access_token"];
            }

            //create a Token Client 
            var tokenClient = new TokenClient(Constants.TripGallerySTSTokenEndpoint, "tripgalleryclientcredentials", Constants.TripGalleryClientSecret);
            var tokenResponse = tokenClient.RequestClientCredentialsAsync("gallerymanagement").Result;

            TokenHelper.DecodeAndWrite(tokenResponse.AccessToken);

            HttpContext.Current.Response.Cookies["TripGalleryCookie"]["access_token"] = tokenResponse.AccessToken;

            return tokenResponse.AccessToken;
        }
 
    }
}