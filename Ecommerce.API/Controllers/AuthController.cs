using Ecommerce.API.Base;
using Ecommerce.Application.Features.Auth.Commands.LoginCommand;
using Ecommerce.Application.Features.Auth.Commands.RefreshToken;
using Ecommerce.Application.Features.Auth.Commands.RevokeToken;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : AppControllerBase
    {
        private const string _refreshTokenCookie = "RefreshToken";

        [HttpGet("status")]
        public IActionResult GetAuthStatus()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return Ok(new { IsAuthenticated = true, UserId = userId });
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost(Name = "GetAccessTokon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccessTokon(LoginCommand loginCommand)
        {
            var result = await _mediator.Send(loginCommand);

            if (result.Data is not null && !string.IsNullOrEmpty(result.Data.RefreshToken))
                SetRefreshTokeninCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);

            return CreateResult(result);
        }

        [HttpPost("RefreshToken", Name = "RefreshToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken()
        {
            HttpContext.Request.Cookies.TryGetValue(_refreshTokenCookie, out var refreshToken);
            var result = await _mediator.Send(new RefreshTokenCommand(refreshToken));

            if (result.Data is not null && !string.IsNullOrEmpty(result.Data.RefreshToken))
                SetRefreshTokeninCookie(result.Data.RefreshToken, result.Data.RefreshTokenExpiration);

            return CreateResult(result);
        }

        [HttpPost("RevokeToken", Name = "RevokeToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken()
        {
            HttpContext.Request.Cookies.TryGetValue(_refreshTokenCookie, out var refreshToken);
            var result = await _mediator.Send(new RevokeTokenCommand(refreshToken));

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
                RemoveRefreshTokenCookie();

            return CreateResult(result);
        }

        private void SetRefreshTokeninCookie(string refreshToken, DateTime ExpiresOn)
        {

            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = ExpiresOn,
            };

            HttpContext.Response.Cookies.Append(_refreshTokenCookie, refreshToken, CookieOptions);
        }
        private void RemoveRefreshTokenCookie()
        {
            var CookieOptions = new CookieOptions
            {
                HttpOnly = true,
                IsEssential = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(-1),
            };
            HttpContext.Response.Cookies.Delete(_refreshTokenCookie, CookieOptions);
        }
    }
}
