using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests;
using Ecommerce.Application.Features.Products.Queries.ProductDetailsQuery;
using Ecommerce.Application.Features.Products.Queries.ProductsDetailsQuery;
using Ecommerce.Application.Features.Products.Queries.ProductSearchQuery;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Products")]
    [ApiController]
    public class ProductsController : AppControllerBase
    {
        [HttpGet("", Name = "SearchProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchProducts([FromQuery] GetProductSearchQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }

        [HttpGet("{Id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProduct([FromRoute] IdRequest request)
        {
            var result = await _mediator.Send(new GetProductDetailsQuery(request.Id));
            return CreateResult(result);
        }

        [HttpPost("GetDetails")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetProductItemsDetails(GetProductsDetailsQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }
    }
}
