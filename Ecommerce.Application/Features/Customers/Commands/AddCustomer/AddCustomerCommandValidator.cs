using Ecommerce.Application.Common.Validators;
using Ecommerce.Doman.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.Features.Customers.Commands.AddCustomer
{

    public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
    {

        public AddCustomerCommandValidator(UserManager<User> userManager)
        {


            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .SetValidator(new PhoneValidator());

            RuleFor(x => x.Password)
               .NotEmpty().WithMessage("Password is required.")
               .SetAsyncValidator(new Common.Validators.PasswordValidator<AddCustomerCommand>(userManager));

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender value.");
        }
    }
}
