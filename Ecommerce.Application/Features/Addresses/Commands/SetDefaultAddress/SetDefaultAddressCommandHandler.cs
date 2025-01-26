using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Commands.SetDefaultAddress
{
    public class SetDefaultAddressCommandHandler : ResponseHandler, IRequestHandler<SetDefaultAddressCommand, Response<bool>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;

        public SetDefaultAddressCommandHandler(IAddressRepository addressRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
        }

        public async Task<Response<bool>> Handle(SetDefaultAddressCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            // Find the address to set as default  
            Address? newDefaultAddress = await _addressRepository.GetByIdAsync(request.AddressId);
            if (newDefaultAddress == null || newDefaultAddress.CustomerId != userId)
            {
                return BadRequest<bool>("Address not found or does not belong to the user.");
            }

            if (newDefaultAddress.IsDefault)
            {
                return Success<bool>(true, "Address is already default.");
            }
            // Find the existing default address  
            Address? oldDefaultAddress = await _addressRepository.GetDefaultAddressByCustomerIdAsync(userId);


            if (oldDefaultAddress != null)
            {
                oldDefaultAddress.IsDefault = false;
            }

            // Set the new default address  
            newDefaultAddress.IsDefault = true;
            await _addressRepository.SaveChangesAsync();
            return Success<bool>(true, "Default address updated successfully.");
        }
    }
}
