using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.AddMultipleShoppingCartItems
{
    public class AddMultipleShoppingCartItemsCommandValidator : AbstractValidator<AddMultipleShoppingCartItemsCommand>
    {
        public AddMultipleShoppingCartItemsCommandValidator()
        {

            RuleFor(x => x.ProductItems)
                .NotEmpty()
                .WithMessage("ProductItems list cannot be empty.");

            RuleForEach(x => x.ProductItems).SetValidator(new ProductItemDtoValidator());
        }

        // Nested Validator for ProductItemDto
        public class ProductItemDtoValidator : AbstractValidator<AddMultipleShoppingCartItemsCommand.ProductItemDto>
        {
            public ProductItemDtoValidator()
            {
                RuleFor(x => x.Id).GreaterThanZero();

                RuleFor(x => x.Quantity).GreaterThanZero();

                RuleFor(x => x.CreatedDate).LessThanOrEqualTo(DateTime.UtcNow);
            }
        }
    }
}
