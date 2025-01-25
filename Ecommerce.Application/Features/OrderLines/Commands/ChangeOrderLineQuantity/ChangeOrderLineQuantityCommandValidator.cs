using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Features.OrderLines.Commands.ChangeOrderLineQuantity
{
    public class ChangeOrderLineQuantityCommandValidator : AbstractValidator<ChangeOrderLineQuantityCommand>
    {
        public ChangeOrderLineQuantityCommandValidator()
        {
            RuleFor(x => x.OrderLineId).GreaterThanZero();

            RuleFor(x => x.NewQuantity).GreaterThanZero();
        }
    }
}
