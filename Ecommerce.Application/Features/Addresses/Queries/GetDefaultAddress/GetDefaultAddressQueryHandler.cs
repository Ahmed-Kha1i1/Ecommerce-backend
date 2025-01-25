using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Queries.GetDefaultAddress
{
    public class GetDefaultAddressQueryHandler
        : ResponseHandler, IRequestHandler<GetDefaultAddressQuery, Response<AddressSummaryDTO>>
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public GetDefaultAddressQueryHandler(IAddressRepository addressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Response<AddressSummaryDTO>> Handle(GetDefaultAddressQuery request, CancellationToken cancellationToken)
        {
            int? userId = _httpContextAccessor.GetUserId();
            if (userId is null)
            {
                return Unauthorized<AddressSummaryDTO>("User is not authenticated.");
            }

            Address? address = await _addressRepository.GetDefaultAddressByCustomerIdAsync(userId.Value);

            if (address == null || address.CustomerId != userId)
            {
                return BadRequest<AddressSummaryDTO>("Address not found or does not belong to the user.");
            }

            return Success(_mapper.Map<AddressSummaryDTO>(address));
        }
    }
}
