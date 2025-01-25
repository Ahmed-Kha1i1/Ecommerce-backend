using Ecommerce.Doman.Entities;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Common.Validators
{
    public class PasswordValidator<T> : AsyncPropertyValidator<T, string> where T : class
    {
        private readonly UserManager<User> _userManager;


        public PasswordValidator(UserManager<User> userManager)
        {
            _userManager = userManager;

        }

        public override string Name => "PasswordValidator";


        public override async Task<bool> IsValidAsync(ValidationContext<T> context, string value, CancellationToken cancellation)
        {
            var validationResult = await _userManager.PasswordValidators[0].ValidateAsync(_userManager, null, value);

            if (!validationResult.Succeeded)
            {
                context.AddFailure(validationResult.Errors.First().Description);
                return false;
            }


            return true;
        }
    }
}
