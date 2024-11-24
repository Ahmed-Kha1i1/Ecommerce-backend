using Ecommerce.API.Base;
using Ecommerce.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Controllers
{
    [Route("api/Countries")]
    [ApiController]
    public class CountriesController(ICountryRepository countryRepository) : AppControllerBase
    {
        [HttpGet("All", Name = "GetAllCountries")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await countryRepository.GetAllAsNoTracking().ToListAsync());
        }
    }
}
