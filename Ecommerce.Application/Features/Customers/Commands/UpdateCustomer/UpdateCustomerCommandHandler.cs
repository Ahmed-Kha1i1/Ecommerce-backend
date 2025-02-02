using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Customers.Commands.EditCustomer
{
    public class UpdateCustomerCommandHandler : ResponseHandler, IRequestHandler<UpdateCustomerCommand, Response<bool>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.GetUserId();
            var customer = await _customerRepository.GetByIdAsync(userId);

            if (customer == null)
            {
                return NotFound<bool>("User not found");
            }

            _mapper.Map(request, customer);
            await _customerRepository.SaveChangesAsync();
            return Success(true, "User updated successfully");
        }
    }
}
