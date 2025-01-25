using Ecommerce.API.Base;
using Ecommerce.Application.Features.ShoppingCarts.Queries.ShoppingCartDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Carts")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : AppControllerBase
    {
        [HttpGet("Info", Name = "GetShoppingCart")]
        public async Task<IActionResult> GetShoppingCart()
        {
            var result = await _mediator.Send(new ShoppingCartDetailsQuery());
            return CreateResult(result);
        }
    }
}
