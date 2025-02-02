using Ecommerce.API.Base;
using Ecommerce.Application.Features.Brands.Queries.BrandsSearchQuery;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Brands")]
    [ApiController]
    public class BrandsController : AppControllerBase
    {
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BrandsSearch([FromQuery] GetBrandsSearchQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }
    }
}
