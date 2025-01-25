using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<Response<AuthDTO>>
    {
        public string? Token { get; set; }

        public RefreshTokenCommand(string? token)
        {
            Token = token;
        }
    }
}
