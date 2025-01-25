using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommand : IRequest<Response<bool>>
    {
        public string? Token { get; set; }

        public RevokeTokenCommand(string? token)
        {
            Token = token;
        }
    }
}
