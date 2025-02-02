using Ecommerce.API.Base;
using Ecommerce.Application.Features.ProductItems.GetProductItemsDetails;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/ProductItems")]
    [ApiController]
    public class ProductItemsController : AppControllerBase
    {

        [HttpPost("GetDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductItemsDetails(GetProductItemsDetailsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }
    }
}
