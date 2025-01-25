﻿using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Auth.Commands.RevokeToken
{
    public class RevokeTokenCommandHandler(IRefreshTokenRepository refreshTokenRepository) : ResponseHandler, IRequestHandler<RevokeTokenCommand, Response<bool>>
    {
        public async Task<Response<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Token))
            {
                return BadRequest<bool>("Token is invalid or already revoked.");
            }

            var refreshToken = await refreshTokenRepository.Get(request.Token);

            if (refreshToken is null)
            {
                return NotFound<bool>("Token not found.");
            }

            if (!refreshToken.IsActive)
            {
                return BadRequest<bool>("Token is already revoked.");
            }

            // Revoke the token
            refreshToken.RevokedOn = DateTime.UtcNow;
            await refreshTokenRepository.SaveChangesAsync();

            return Success(true, "Token revoked successfully.");
        }
    }
}
