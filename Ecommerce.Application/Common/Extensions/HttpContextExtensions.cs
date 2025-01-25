using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecommerce.Application.Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static int? GetUserId(this IHttpContextAccessor httpContextAccessor)
        {
            string? userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out int id))
            {
                return id;
            }
            return null;
        }

        public static string? GetUserIpAddress(this IHttpContextAccessor httpContextAccessor)
        {
            var ipAddress = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;

            return ipAddress?.ToString();
        }
    }
}
