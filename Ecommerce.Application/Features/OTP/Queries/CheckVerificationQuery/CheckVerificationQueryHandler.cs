using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.OTP.Queries.CheckVerificationQuery
{
    public class CheckVerificationQueryHandler : ResponseHandler, IRequestHandler<CheckVerificationQuery, Response<bool>>
    {
        private readonly IOTPRepository _otpRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CheckVerificationQueryHandler(IOTPRepository otpRepository, IHttpContextAccessor httpContextAccessor)
        {
            _otpRepository = otpRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<Response<bool>> Handle(CheckVerificationQuery request, CancellationToken cancellationToken)
        {
            string? ipAddress = _httpContextAccessor.GetUserIpAddress();
            bool isVerified = _otpRepository.IsVerified(request.Email, ipAddress);

            if (isVerified)
            {
                return Task.FromResult(Success(true, "Email is verified."));
            }
            else
            {
                return Task.FromResult(Success(false, "Email is not verified."));
            }
        }
    }
}
