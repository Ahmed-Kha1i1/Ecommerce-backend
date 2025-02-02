using Ecommerce.API.Base;
using Ecommerce.Application.Features.ShoppingCartItems.Commands.AddMultipleShoppingCartItems;
using Ecommerce.Application.Features.ShoppingCartItems.Commands.AddShoppingCartItem;
using Ecommerce.Application.Features.ShoppingCartItems.Commands.RemoveShoppingCartItem;
using Ecommerce.Application.Features.ShoppingCartItems.Commands.UpdateShoppingCartItemQuantity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Controllers
{
    [Route("api/CartItems")]
    [ApiController]
    [Authorize]
    public class ShoppingCartItemController : AppControllerBase
    {
        [HttpPost("AddItem")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddShoppingCartItem([FromBody] AddShoppingCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveShoppingCartItem([FromBody] RemoveShoppingCartItemCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPut("UpdateQuantity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShoppingCartItemQuantity([FromBody] UpdateShoppingCartItemQuantityCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }


        [HttpPost("AddMultipleItems")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMultipleItems([FromBody] AddMultipleShoppingCartItemsCommand command)
        {
            var response = await _mediator.Send(command);

            return CreateResult(response);
        }
    }
}
