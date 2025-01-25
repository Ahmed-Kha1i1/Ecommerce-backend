using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.RemoveShoppingCartItem
{
    public class RemoveShoppingCartItemCommandValidator : AbstractValidator<RemoveShoppingCartItemCommand>
    {
        public RemoveShoppingCartItemCommandValidator()
        {

            RuleFor(x => x.ProductItemId)
                .GreaterThanZero();
        }
    }
}
