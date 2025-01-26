using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Addresses.Queries.GetAddresses
{
    public class GetAddressesQueryHandler : ResponseHandler, IRequestHandler<GetAddressesQuery, Response<List<AddressSummaryDTO>>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;

        public GetAddressesQueryHandler(IAddressRepository addressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<AddressSummaryDTO>>> Handle(GetAddressesQuery request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            var addresses = await _addressRepository.GetAddressesByCustomerIdAsync(userId);

            if (addresses == null || addresses.Count == 0)
            {
                return Success(new List<AddressSummaryDTO>());
            }

            return Success(_mapper.Map<List<AddressSummaryDTO>>(addresses));
        }
    }
}
