using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace Ecommerce.Application.Features.Customers.Commands.AddCustomer
{
    public class AddCustomerCommandHandler : ResponseHandler, IRequestHandler<AddCustomerCommand, Response<int?>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;
        private readonly IOTPRepository _OTPRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddCustomerCommandHandler(UserManager<User> userManager, IUserStore<User> userStore, IOTPRepository oTPRepository, ICustomerRepository customerRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userStore = userStore;
            _OTPRepository = oTPRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<int?>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.IsEmailExists(request.Email))
            {
                return BadRequest<int?>("Email is already in use. Please use a different email.");
            }

            string? ipAddress = _httpContextAccessor.GetUserIpAddress();
            if (!_OTPRepository.IsVerified(request.Email, ipAddress))
            {
                return BadRequest<int?>("Email not verified. Please verify your email before proceeding.");
            }

            if (!_userManager.SupportsUserEmail)
            {
                return Fail<int?>(null, "The user store does not support email functionality.");
            }

            // Create the user
            var userId = await CreateUserAsync(request);
            if (userId is null)
            {
                return BadRequest<int?>("An error occurred while creating the customer. Please try again.");
            }
            
            await _OTPRepository.RemoveVerificationAsync(request.Email, ipAddress);

            var customer = new Customer
            {
                Id = userId.Value, // Use the user ID
            };

            await _customerRepository.AddCustomerToExistsUser(customer.Id);

            return Success<int?>(customer.Id, "Customer created successfully.");
        }

        private async Task<int?> CreateUserAsync(AddCustomerCommand request)
        {
            var emailStore = (IUserEmailStore<User>)_userStore;
            var email = request.Email;

            var user = new User();
            string username = UsernameGenerator.GenerateUniqueUsername(email);

            await _userStore.SetUserNameAsync(user, username, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.Gender = request.Gender;
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var result = await _userManager.CreateAsync(user, request.Password);
                    if (!result.Succeeded)
                    {
                        return null; // User creation failed
                    }

                    var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
                    if (!roleResult.Succeeded)
                    {
                        // Delete the user if adding to the role failed
                        await _userManager.DeleteAsync(user);
                        return null; // Role assignment failed
                    }

                    transaction.Complete();
                    return user.Id;
                }
                catch
                {
                    // Rollback user creation if any exception occurs
                    await _userManager.DeleteAsync(user);
                    throw; // Re-throw the exception to handle it further up the stack
                }
            }
        }

    }
}
