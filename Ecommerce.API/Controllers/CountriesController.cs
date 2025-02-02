using Ecommerce.API.Base;
using Ecommerce.Application.Features.Countries.Queries.GetCountries;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Countries")]
    [ApiController]
    public class CountriesController : AppControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetCountriesQuery());
            return CreateResult(result);
        }
    }
}
