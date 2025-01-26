using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Customers.Queries.CustomerDetailsQuery
{
    public class GetCustomerDetailsQueryHandler : ResponseHandler, IRequestHandler<GetCustomerDetailsQuery, Response<GetCustomerDetailsQueryResponse>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;


        public GetCustomerDetailsQueryHandler(ICustomerRepository customerRepository, IAddressRepository addressRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _mapper = mapper;
        }
        public async Task<Response<GetCustomerDetailsQueryResponse>> Handle(GetCustomerDetailsQuery request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            Customer? customer = await _customerRepository.GetByIdAsync(userId);

            if (customer == null)
            {
                return BadRequest<GetCustomerDetailsQueryResponse>("Customer not found.");
            }
            var response = _mapper.Map<GetCustomerDetailsQueryResponse>(customer);
            response.HasDefaultAddress = await _addressRepository.HasDefaultAddress(userId);
            return Success(response);
        }
    }
}