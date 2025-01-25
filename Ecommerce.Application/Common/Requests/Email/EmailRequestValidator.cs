using FluentValidation;

namespace Ecommerce.Application.Common.Requests.Email
{
    public class EmailRequestValidator : AbstractValidator<EmailRequest>
    {
        public EmailRequestValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().EmailAddress();
        }
    }
}
