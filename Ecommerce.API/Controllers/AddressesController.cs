﻿using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests;
using Ecommerce.Application.Features.Addresses.Commands.AddAddress;
using Ecommerce.Application.Features.Addresses.Commands.DeleteAddress;
using Ecommerce.Application.Features.Addresses.Commands.SetDefaultAddress;
using Ecommerce.Application.Features.Addresses.Commands.UpdateAddress;
using Ecommerce.Application.Features.Addresses.Queries.GetAddress;
using Ecommerce.Application.Features.Addresses.Queries.GetAddresses;
using Ecommerce.Application.Features.Addresses.Queries.GetDefaultAddress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Addresses")]
    [ApiController]
    [Authorize]
    public class AddressesController : AppControllerBase
    {
        [HttpGet("")]
        public async Task<IActionResult> GetAddresses()
        {
            var result = await _mediator.Send(new GetAddressesQuery());
            return CreateResult(result);
        }

        [HttpGet("DefaultAddress")]
        public async Task<IActionResult> GetDefaultAddress()
        {
            var result = await _mediator.Send(new GetDefaultAddressQuery());
            return CreateResult(result);
        }

        [HttpGet("{Id}")]// addressId
        public async Task<IActionResult> GetAddress([FromRoute] IdRequest request)
        {
            var result = await _mediator.Send(new GetAddressQuery(request.Id));
            return CreateResult(result);
        }

        [HttpDelete("{Id}")]// addressId
        public async Task<IActionResult> DeleteAddress([FromRoute] IdRequest request)
        {
            var result = await _mediator.Send(new DeleteAddressCommand(request.Id));
            return CreateResult(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAddress([FromBody] UpdateAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPut("SetDefault")]
        public async Task<IActionResult> SetDefaultAddress([FromBody] SetDefaultAddressCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }
    }
}
