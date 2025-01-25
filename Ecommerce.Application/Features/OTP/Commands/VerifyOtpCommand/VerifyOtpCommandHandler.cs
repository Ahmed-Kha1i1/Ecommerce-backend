using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.OTP.Commands.VerifyOtpCommand
{
    public class VerifyOtpCommandHandler : ResponseHandler, IRequestHandler<VerifyOtpCommand, Response<bool>>
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VerifyOtpCommandHandler(IOTPRepository otpRepository, IHttpContextAccessor httpContextAccessor)
        {
            _otpRepository = otpRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Response<bool>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
        {
            string? ipAddress = _httpContextAccessor.GetUserIpAddress();
            bool isVerified = _otpRepository.VerifyOtp(request.Email, request.Otp, ipAddress);

            if (isVerified)
            {
                return Task.FromResult(Success(true, "OTP verified successfully."));
            }
            else
            {
                return Task.FromResult(BadRequest<bool>("OTP verification failed. Please try again."));
            }
        }
    }
}
