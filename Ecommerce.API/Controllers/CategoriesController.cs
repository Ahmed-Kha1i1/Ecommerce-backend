using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests;
using Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery;
using Ecommerce.Application.Features.Categories.Queries.MainCategoriesQuery;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Categories")]
    [ApiController]
    public class CategoriesController : AppControllerBase
    {
        [HttpGet("Main", Name = "GetMainCategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMainCategories()
        {
            var result = await _mediator.Send(new GetMainCategoriesQuery());
            return CreateResult(result);
        }

        [HttpGet("{Id}", Name = "GetCategoryHierarchy")] // CategoryId
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryHierarchy([FromRoute] IdRequest request)
        {
            var result = await _mediator.Send(new GetCategoryHierarchyQuery(request.Id));
            return CreateResult(result);
        }
    }
}
