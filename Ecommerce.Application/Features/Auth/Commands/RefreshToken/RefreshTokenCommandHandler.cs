using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository, ICustomerRepository customerRepository, IAuthRespository authRespository)
        : ResponseHandler, IRequestHandler<RefreshTokenCommand, Response<AuthDTO>>
    {
        public async Task<Response<AuthDTO>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest<AuthDTO>("Invalid Token");
            }
            var refreshToken = await refreshTokenRepository.GetWithCustomerAsync(request.Token);

            if (refreshToken is null || !refreshToken.IsActive)
            {
                return BadRequest<AuthDTO>("Invalid Token");
            }
            refreshToken.RevokedOn = DateTime.UtcNow;
            await refreshTokenRepository.SaveChangesAsync();

            var newRefreshToken = refreshTokenRepository.GenerateRefreshToken(refreshToken.CustomerId);
            await refreshTokenRepository.AddAsync(newRefreshToken);

            var tokenResult = authRespository.GenerateAccessToken(refreshToken.Customer);

            AuthDTO authDto = new AuthDTO
            {
                AccessToken = tokenResult.AccessToken,
                CustomerId = refreshToken.CustomerId,
                ExpiresOn = tokenResult.ExpiresOn,
                RefreshToken = newRefreshToken.Token,
                RefreshTokenExpiration = newRefreshToken.ExpiresOn
            };

            return Success(authDto);
        }
    }
}
