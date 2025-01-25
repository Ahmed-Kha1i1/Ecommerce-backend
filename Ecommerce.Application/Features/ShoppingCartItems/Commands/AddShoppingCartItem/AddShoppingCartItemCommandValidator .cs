using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.AddShoppingCartItem
{
    public class AddShoppingCartItemCommandValidator : AbstractValidator<AddShoppingCartItemCommand>
    {
        public AddShoppingCartItemCommandValidator()
        {

            RuleFor(x => x.ProductItemId)
                .GreaterThanZero();

            RuleFor(x => x.Quantity)
                .GreaterThanZero();
        }
    }
}
