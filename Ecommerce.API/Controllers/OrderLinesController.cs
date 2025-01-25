using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests;
using Ecommerce.Application.Features.OrderLines.Commands.AddOrderLine;
using Ecommerce.Application.Features.OrderLines.Commands.ChangeOrderLineQuantity;
using Ecommerce.Application.Features.OrderLines.Commands.RemoveOrderLine;
using Ecommerce.Application.Features.OrderLines.Queries.GetPaginatedOrderLines;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/OrderLines")]
    [ApiController]
    public class OrderLinesController : AppControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPaginatedOrderLines([FromQuery] GetPaginatedOrderLinesQuery query)
        {
            var result = await _mediator.Send(query);
            return CreateResult(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddOrderLine([FromBody] AddOrderLineCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpDelete("Cancel/{Id}")] //orderLineId
        public async Task<IActionResult> CancelOrderLine([FromRoute] IdRequest request)
        {
            var command = new RemoveOrderLineCommand(request.Id);
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> ChangeOrderLineQuantity(ChangeOrderLineQuantityCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }
    }
}
