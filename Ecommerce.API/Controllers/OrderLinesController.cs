using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests;
using Ecommerce.Application.Features.OrderLines.Commands.RemoveOrderLine;
using Ecommerce.Application.Features.OrderLines.Queries.GetPaginatedOrderLines;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Controllers
{
    [Route("api/OrderLines")]
    [ApiController]
    [Authorize]
    public class OrderLinesController : AppControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginatedOrderLines([FromQuery] GetPaginatedOrderLinesQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }

        [HttpDelete("Cancel/{Id}")] //orderLineId
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CancelOrderLine([FromRoute] IdRequest request)
        {
            var command = new RemoveOrderLineCommand(request.Id);
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

    }
}
