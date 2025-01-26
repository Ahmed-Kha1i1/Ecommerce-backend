using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests;
using Ecommerce.Application.Features.Orders.Commands.AddOrder;
using Ecommerce.Application.Features.Orders.Commands.CancelOrder;
using Ecommerce.Application.Features.Orders.Queries.GetOrderDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Orders")]
    [ApiController]
    [Authorize]
    public class OrderController : AppControllerBase
    {
        [HttpGet("{Id}")] //OrderId
        public async Task<IActionResult> GetOrder([FromRoute] IdRequest request)
        {
            var result = await _mediator.Send(new GetOrderDetailsQuery(request.Id));
            return CreateResult(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpDelete("Cancel/{Id}")] //orderId
        public async Task<IActionResult> CancelOrder([FromRoute] IdRequest request)
        {
            var command = new CancelOrderCommand(request.Id);
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }
    }
}
