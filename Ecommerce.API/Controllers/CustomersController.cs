using Ecommerce.API.Base;
using Ecommerce.Application.Features.Customers.Commands.AddCustomer;
using Ecommerce.Application.Features.Customers.Commands.EditCustomer;
using Ecommerce.Application.Features.Customers.Queries.CustomerDetailsQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.API.Controllers
{
    [Route("api/Customers")]
    [ApiController]
    public class CustomersController : AppControllerBase
    {
        [Authorize()]
        [HttpGet("Info")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomer()
        {
            var result = await _mediator.Send(new GetCustomerDetailsQuery());
            return CreateResult(result);
        }

        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddNewCustomer(AddCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }
    }
}
