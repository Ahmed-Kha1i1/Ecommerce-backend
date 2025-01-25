using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.ShoppingCarts.Queries.ShoppingCartDetails
{
    public class ShoppingCartDetailsQueryHandler : ResponseHandler, IRequestHandler<ShoppingCartDetailsQuery, Response<ShoppingCartDTO?>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IShoppingCartRepository _shoppingCartRepository;


        public ShoppingCartDetailsQueryHandler(IShoppingCartRepository shoppingCartRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Response<ShoppingCartDTO?>> Handle(ShoppingCartDetailsQuery request, CancellationToken cancellationToken)
        {
            int? userId = _httpContextAccessor.GetUserId();
            if (userId is null)
            {
                return Unauthorized<ShoppingCartDTO?>("User is not authenticated.");
            }
            var shoppingCard = await _shoppingCartRepository.GetCartItemsAsync(userId .Value);
            return Success(shoppingCard);
        }
    }
}
