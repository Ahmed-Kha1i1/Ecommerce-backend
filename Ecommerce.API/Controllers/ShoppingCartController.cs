using Ecommerce.API.Base;
using Ecommerce.Application.Features.ShoppingCarts.Queries.ShoppingCartDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Controllers
{
    [Route("api/Carts")]
    [ApiController]
    [Authorize]
    public class ShoppingCartController : AppControllerBase
    {
        [HttpGet("Info", Name = "GetShoppingCart")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetShoppingCart()
        {
            var result = await _mediator.Send(new ShoppingCartDetailsQuery());
            return CreateResult(result);
        }
    }
}
