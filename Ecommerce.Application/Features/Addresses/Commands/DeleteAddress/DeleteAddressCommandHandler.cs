using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommandHandler : ResponseHandler, IRequestHandler<DeleteAddressCommand, Response<bool>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;

        public DeleteAddressCommandHandler(IAddressRepository addressRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
        }

        public async Task<Response<bool>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            Address? address = await _addressRepository.GetByIdAsync(request.AddressId);

            if (address == null || address.CustomerId != userId)
            {
                return BadRequest<bool>("Address not found or does not belong to the user.");
            }

            if (address.IsDefault)
            {
                return BadRequest<bool>("Can't delete the default address.");
            }
            _addressRepository.Delete(address);
            await _addressRepository.SaveChangesAsync();
            return Success<bool>(true, "Address deleted successfully.");
        }
    }
}
