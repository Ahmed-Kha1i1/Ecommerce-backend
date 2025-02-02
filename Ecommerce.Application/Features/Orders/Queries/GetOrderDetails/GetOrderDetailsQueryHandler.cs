using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : ResponseHandler, IRequestHandler<GetOrderDetailsQuery, Response<GetOrderDetailsQueryResponse>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<GetOrderDetailsQueryResponse>> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            var order = await _orderRepository.GetOrderIncludeAddress(request.OrderId);
            if (order == null || order.CustomerId != userId)
            {
                return NotFound<GetOrderDetailsQueryResponse>("Order not found or does not belong to the user");
            }

            var orderResponse = _mapper.Map<GetOrderDetailsQueryResponse>(order);

            orderResponse.Lines = await _orderLineRepository.GetOrderLinesPerOrder(orderResponse.OrderId);

            return Success(orderResponse);
        }
    }
}
