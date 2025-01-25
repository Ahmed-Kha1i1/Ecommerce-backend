using Ecommerce.Application.Common.Validators;
using FluentValidation;

namespace Ecommerce.Application.Common.Requests
{
    public class IdValidator : AbstractValidator<IdRequest>
    {
        public IdValidator()
        {
            RuleFor(obj => obj.Id).GreaterThanZero();
        }
    }
}
