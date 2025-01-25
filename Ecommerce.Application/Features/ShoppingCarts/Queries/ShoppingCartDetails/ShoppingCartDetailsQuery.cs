using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.ShoppingCarts.Queries.ShoppingCartDetails
{
    public class ShoppingCartDetailsQuery : IRequest<Response<ShoppingCartDTO?>>
    {

    }
}
