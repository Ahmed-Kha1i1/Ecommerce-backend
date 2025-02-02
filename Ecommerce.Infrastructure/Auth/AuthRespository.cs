using Ecommerce.Application.Contracts;
using Ecommerce.Application.Features.Auth;
using Ecommerce.Doman.Entities;
using Ecommerce.Infrastructure.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Persistence.Repositories
{
    public class AuthRespository : IAuthRespository
    {
        private readonly JwtOptions _jwtOptions;
        public AuthRespository(IOptionsSnapshot<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }
        public TokenResult GenerateAccessToken(Customer customer)
        {
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audiences.FirstOrDefault(),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new(ClaimTypes.Name, $"{customer.FirstName} {customer.LastName}"),
                    //new(ClaimTypes.Role, GetRoleName(customer.Role)),
                    new(ClaimTypes.Email, customer.Email)
                })

            };

            var SecurityToken = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(SecurityToken);

            var tokenResult = new TokenResult
            {
                AccessToken = accessToken,
                ExpiresOn = DateTime.UtcNow.AddMinutes(_jwtOptions.Lifetime),
            };

            return tokenResult;
        }

    }
}
