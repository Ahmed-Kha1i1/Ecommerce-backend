using Ecommerce.Doman.Common.Enums;

namespace Ecommerce.Application.Features.OTP.Commands.SendOtpCommand
{
    public class SendOtpCommandResponse
    {
        public enEmailCondition Condition { get; set; }
        public int? RetryAfterSeconds { get; set; }

        public SendOtpCommandResponse(enEmailCondition condition, int? retryAfterSeconds = null)
        {
            Condition = condition;
            RetryAfterSeconds = retryAfterSeconds;
        }
    }
}
