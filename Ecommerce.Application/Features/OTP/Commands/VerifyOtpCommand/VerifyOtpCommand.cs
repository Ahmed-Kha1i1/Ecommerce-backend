using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OTP.Commands.VerifyOtpCommand
{
    public class VerifyOtpCommand : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public string Otp { get; set; }

        public VerifyOtpCommand(string email, string otp)
        {
            Email = email;
            Otp = otp;
        }
    }
}
