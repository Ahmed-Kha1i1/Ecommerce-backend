using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Features.OrderLines.Commands.AddOrderLine
{
    public class AddOrderLineCommandValidator : AbstractValidator<AddOrderLineCommand>
    {
        public AddOrderLineCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThanZero();

            RuleFor(x => x.ProductItemId)
                .GreaterThanZero();

            RuleFor(x => x.Quantity)
                .GreaterThanZero();

            RuleFor(x => x.Price)
                .GreaterThanZero();
        }
    }
}
