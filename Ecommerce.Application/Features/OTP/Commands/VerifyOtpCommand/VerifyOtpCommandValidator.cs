using FluentValidation;

namespace Ecommerce.Application.Features.OTP.Commands.VerifyOtpCommand
{
    public class VerifyOtpCommandValidator : AbstractValidator<VerifyOtpCommand>
    {
        public VerifyOtpCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required.")
                .Length(6).WithMessage("OTP must be 6 digits long.")
                .Matches(@"^\d{6}$").WithMessage("OTP must be numeric.");
        }
    }
}
