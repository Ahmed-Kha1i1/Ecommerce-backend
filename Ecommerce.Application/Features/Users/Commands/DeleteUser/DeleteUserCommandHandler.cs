using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : ResponseHandler, IRequestHandler<DeleteUserCommand, Response<bool>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DeleteUserCommandHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();
            if (userId == null)
            {
                return Unauthorized<bool>();
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound<bool>("User not found");
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, request.password);
            if (!passwordCheck)
            {
                return Unauthorized<bool>("");
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                return Success(true, "User deleted successfully");
            }
            else
            {
                return Fail(false, "Failed to delete user");
            }
        }
    }
}