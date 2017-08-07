using IdentityModel.Client;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace TripGallery.MVCClient.Controllers
{
    public class STSCallbackController : Controller
    {
        // GET: STSCallback
        public async Task<ActionResult> Index()
        {
            var authCode = Request.QueryString["code"];

            var client = new TokenClient(
                Constants.TripGallerySTSTokenEndpoint,
                "tripgalleryauthcode",
                Constants.TripGalleryClientSecret);

            var tokenResponse = await client.RequestAuthorizationCodeAsync(authCode, Constants.TripGalleryMVCSTSCallback);

            Response.Cookies["TripGalleryCookie"]["access_token"] = tokenResponse.AccessToken;

            return Redirect(Request.QueryString["state"]);
        }
    }
}