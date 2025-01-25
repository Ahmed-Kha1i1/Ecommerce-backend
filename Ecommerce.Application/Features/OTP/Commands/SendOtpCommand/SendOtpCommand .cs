using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OTP.Commands.SendOtpCommand
{
    public class SendOtpCommand : IRequest<Response<SendOtpCommandResponse>>
    {
        public string Email { get; set; }

        public SendOtpCommand(string email)
        {
            Email = email;
        }
    }
}
