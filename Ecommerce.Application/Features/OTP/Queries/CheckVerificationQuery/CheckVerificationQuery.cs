using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OTP.Queries.CheckVerificationQuery
{
    public class CheckVerificationQuery : IRequest<Response<bool>>
    {
        public string Email { get; set; }
        public CheckVerificationQuery(string email)
        {
            Email = email;
        }
    }

}
