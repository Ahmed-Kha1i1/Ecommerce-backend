using Ecommerce.API.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class OAuthController : AppControllerBase
    {
        [HttpGet("signin-facebook")]
        public IActionResult GitHubLogin()
        {
            var authenticationProperties = new AuthenticationProperties();

            return Challenge(authenticationProperties, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-google")]
        public IActionResult GoogleLogin()
        {
            var authenticationProperties = new AuthenticationProperties();

            return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
