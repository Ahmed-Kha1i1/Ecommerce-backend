using FluentValidation;
using System.Text.RegularExpressions;

namespace Ecommerce.Application.Common.Validators
{
    public class PhoneValidator : AbstractValidator<string>
    {

        public PhoneValidator()
        {
            RuleFor(p => p)
                .MinimumLength(10).WithMessage("PhoneNumber must not be less than 10 characters.")
                .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
                .Matches(new Regex(@"^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$")).WithMessage("PhoneNumber not valid");
        }
    }
}
