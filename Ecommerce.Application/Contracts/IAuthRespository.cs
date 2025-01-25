using Ecommerce.Application.Features.Auth;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Contracts
{
    public interface IAuthRespository
    {
        TokenResult GenerateAccessToken(Customer customer);
    }
}
