using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Commands.UpdateAddress
{
    public class UpdateAddressCommandHandler : ResponseHandler, IRequestHandler<UpdateAddressCommand, Response<bool>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;

        public UpdateAddressCommandHandler(IAddressRepository addressRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
        }

        public async Task<Response<bool>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            var address = await _addressRepository.GetByIdAsync(request.AddressId);
            if (address == null || address.CustomerId != userId)
            {
                return BadRequest<bool>("Address not found or does not belong to the user.");
            }

            // Update address properties  
            address.CountryId = request.CountryId;
            address.Address1 = request.Address1;
            address.Address2 = request.Address2;
            address.PostalCode = request.PostalCode;
            address.City = request.City;
            address.State = request.State;

            await _addressRepository.SaveChangesAsync();
            return Success<bool>(true, "Address updated successfully.");
        }
    }
}
