using Ecommerce.Doman.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace Ecommerce.API.Base
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole<int>>
    {
        public CustomClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);

            var nameClaim = identity.FindFirst(ClaimTypes.Name);
            if (nameClaim != null)
            {
                identity.RemoveClaim(nameClaim);
            }

            var SecurityStampClaim = identity.FindFirst("AspNet.Identity.SecurityStamp");

            //identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName));
            return identity;
        }
    }
}
