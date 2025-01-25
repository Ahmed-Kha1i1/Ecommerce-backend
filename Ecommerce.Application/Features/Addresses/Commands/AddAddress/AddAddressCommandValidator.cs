using FluentValidation;

namespace Ecommerce.Application.Features.Addresses.Commands.AddAddress
{
    public class AddAddressCommandValidator : AbstractValidator<AddAddressCommand>
    {
        public AddAddressCommandValidator()
        {
            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("CountryId must be greater than zero.");

            RuleFor(x => x.Address1)
                .NotEmpty().WithMessage("Address1 is required.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required.")
                .MaximumLength(50).WithMessage("PostalCode cannot exceed 10 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.")
                .MaximumLength(50).WithMessage("City cannot exceed 100 characters.");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required.")
                .MaximumLength(50).WithMessage("State cannot exceed 100 characters.");
        }
    }
}
