using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommandHandler(ICustomerRepository customerRepository, IAuthRespository authRespository, IRefreshTokenRepository refreshTokenRepository) : ResponseHandler, IRequestHandler<LoginCommand, Response<AuthDTO>>
    {
        public async Task<Response<AuthDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var customer = await customerRepository.Get(request.Email, request.PasswordHash);

            if (customer == null)
            {
                return BadRequest<AuthDTO>("Email or password is incorrect!");
            }

            var tokenResult = authRespository.GenerateAccessToken(customer);
            AuthDTO AuthInfo = new AuthDTO
            {
                AccessToken = tokenResult.AccessToken,
                CustomerId = customer.Id,
                ExpiresOn = tokenResult.ExpiresOn,
            };


            var ActiveRefreshToken = await refreshTokenRepository.GetActiveRefreshToken(customer.Id);

            if (ActiveRefreshToken != null)
            {
                AuthInfo.RefreshToken = ActiveRefreshToken.Token;
                AuthInfo.RefreshTokenExpiration = ActiveRefreshToken.ExpiresOn;
            }
            else
            {
                var refreshToken = refreshTokenRepository.GenerateRefreshToken(customer.Id);

                await refreshTokenRepository.AddAsync(refreshToken);

                AuthInfo.RefreshToken = refreshToken.Token;
                AuthInfo.RefreshTokenExpiration = refreshToken.ExpiresOn;
            }

            return Success(AuthInfo);
        }
    }
}
