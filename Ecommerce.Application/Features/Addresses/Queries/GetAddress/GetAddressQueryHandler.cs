using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Queries.GetAddress
{
    public class GetAddressQueryHandler : ResponseHandler, IRequestHandler<GetAddressQuery, Response<AddressSummaryDTO>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public GetAddressQueryHandler(IAddressRepository addressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Response<AddressSummaryDTO>> Handle(GetAddressQuery request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            Address? address = await _addressRepository.GetByIdIncludeCountryAsync(request.AddressId);

            if (address == null || address.CustomerId != userId)
            {
                return BadRequest<AddressSummaryDTO>("Address not found or does not belong to the user.");
            }

            return Success(_mapper.Map<AddressSummaryDTO>(address));
        }
    }
}
