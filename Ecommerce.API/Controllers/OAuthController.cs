using Ecommerce.API.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class OAuthController : AppControllerBase
    {

        [HttpGet("signin-google")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GoogleLogin()
        {
            var authenticationProperties = new AuthenticationProperties();

            return Challenge(authenticationProperties, GoogleDefaults.AuthenticationScheme);
        }
    }
}
