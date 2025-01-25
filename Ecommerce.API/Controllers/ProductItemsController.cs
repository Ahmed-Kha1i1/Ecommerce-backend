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
        public async Task<IActionResult> GetProductItemsDetails(GetProductItemsDetailsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }
    }
}
