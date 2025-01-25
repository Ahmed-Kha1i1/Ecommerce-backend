using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Response<bool>>
    {
        public string password { get; set; }
    }
}
