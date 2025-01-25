using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Common.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.OTP.Commands.SendOtpCommand
{
    public class SendOtpCommandHandler : ResponseHandler, IRequestHandler<SendOtpCommand, Response<SendOtpCommandResponse>>
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SendOtpCommandHandler(IOTPRepository otpRepository, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _otpRepository = otpRepository;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<SendOtpCommandResponse>> Handle(SendOtpCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.IsEmailExists(request.Email))
            {
                return BadRequest("Email is already in use. Please use a different email.", new SendOtpCommandResponse(enEmailCondition.Used));
            }

            string? ipAddress = _httpContextAccessor.GetUserIpAddress();
            if (_otpRepository.IsVerified(request.Email, ipAddress))
            {
                return Success(new SendOtpCommandResponse(enEmailCondition.Verified), "This email has already been verified. Please proceed to complete your registration.");
            }

            if (_otpRepository.HasOtp(request.Email, out int? remainingSeconds))
            {
                return Success(new SendOtpCommandResponse(enEmailCondition.HasOtp, remainingSeconds), "OTP has already been sent to this email. Please check your email and enter the OTP to verify.");
            }

            await _otpRepository.SendOtp(request.Email);
            return Success(new SendOtpCommandResponse(enEmailCondition.ReceiveOtp), "OTP has been sent successfully.");
        }
    }
}
