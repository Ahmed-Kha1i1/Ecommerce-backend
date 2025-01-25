using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Features.ShoppingCartItems.Commands.UpdateShoppingCartItemQuantity
{
    public class UpdateShoppingCartItemQuantityCommandValidator : AbstractValidator<UpdateShoppingCartItemQuantityCommand>
    {
        public UpdateShoppingCartItemQuantityCommandValidator()
        {

            RuleFor(x => x.ProductItemId)
                .GreaterThanZero();

            RuleFor(x => x.NewQuantity)
                .GreaterThanZero();
        }
    }
}
