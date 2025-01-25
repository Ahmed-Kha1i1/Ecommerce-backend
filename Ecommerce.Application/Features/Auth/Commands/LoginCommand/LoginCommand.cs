using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommand : IRequest<Response<AuthDTO>>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
