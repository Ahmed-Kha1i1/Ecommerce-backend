using FluentValidation;

namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress
{

    public class DeleteAddressCommandValidator : AbstractValidator<DeleteAddressCommand>
    {
        public DeleteAddressCommandValidator()
        {
            RuleFor(x => x.AddressId)
                .GreaterThan(0).WithMessage("AddressId must be greater than zero.");
        }
    }
}
