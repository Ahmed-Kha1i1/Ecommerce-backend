﻿using Ecommerce.API.Base;
using Ecommerce.Application.Common.Requests.Email;
using Ecommerce.Application.Features.OTP.Commands.SendOtpCommand;
using Ecommerce.Application.Features.OTP.Commands.VerifyOtpCommand;
using Ecommerce.Application.Features.OTP.Queries.CheckVerificationQuery;
using Ecommerce.Application.Features.Users.Commands.DeleteUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : AppControllerBase
    {
        //Send an OTP to the email for the registration process.
        [HttpPost("send-otp")]
        public async Task<IActionResult> SendOtp([FromBody] EmailRequest request)
        {
            var result = await _mediator.Send(new SendOtpCommand(request.Email));
            return CreateResult(result);
        }
        //Verify an OTP that has been sended to the email for the registration process.
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }

        [HttpPost("check-verification")]
        //Check is the email verified
        public async Task<IActionResult> CheckVerification([FromBody] EmailRequest request)
        {
            var result = await _mediator.Send(new CheckVerificationQuery(request.Email));
            return CreateResult(result);
        }

        [HttpDelete("")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(DeleteUserCommand command)
        {
            var result = await _mediator.Send(command);
            return CreateResult(result);
        }
    }
}

