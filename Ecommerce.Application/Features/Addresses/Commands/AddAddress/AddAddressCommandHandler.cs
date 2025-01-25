using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Commands.AddAddress
{
    public class AddAddressCommandHandler : ResponseHandler, IRequestHandler<AddAddressCommand, Response<int?>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;

        public AddAddressCommandHandler(IAddressRepository addressRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
        }

        public async Task<Response<int?>> Handle(AddAddressCommand request, CancellationToken cancellationToken)
        {
            int? userId = _httpContextAccessor.GetUserId();
            if (userId is null)
            {
                return Unauthorized<int?>("User is not authenticated.");
            }

            bool hasDefault = await _addressRepository.HasDefaultAddress(userId.Value);


            var address = new Address
            {
                CustomerId = userId.Value,
                Address1 = request.Address1,
                Address2 = request.Address2,
                PostalCode = request.PostalCode,
                CountryId = request.CountryId,
                City = request.City,
                State = request.State,
                IsDefault = !hasDefault,
                CreatedDate = DateTime.UtcNow
            };

            await _addressRepository.AddAsync(address);
            await _addressRepository.SaveChangesAsync();
            return Success<int?>(address.Id, "Address added successfully.");
        }
    }
}
