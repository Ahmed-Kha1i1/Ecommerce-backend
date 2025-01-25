using FluentValidation;

namespace Ecommerce.Application.Features.Auth.Commands.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(l => l.Email).EmailAddress();
            RuleFor(l => l.PasswordHash).NotEmpty();
        }
    }
}
