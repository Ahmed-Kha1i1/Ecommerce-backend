using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Transactions;

namespace Ecommerce.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommandHandler : ResponseHandler, IRequestHandler<CancelOrderCommand, Response<bool>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            if (order == null)
            {
                return NotFound<bool>("Order not found.");
            }

            var cancellableStatuses = new List<enOrderStatus>() { enOrderStatus.Placed, enOrderStatus.Confirmed, enOrderStatus.Processing };

            // Check if the current order status allows cancellation
            if (!cancellableStatuses.Contains(order.Status))
            {
                return BadRequest<bool>("Order cannot be canceled at its current status.");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                order.Status = enOrderStatus.Canceled;
                order.TotalPrice = await _orderLineRepository.GetOrderPriceAsync(request.OrderId);
                await _orderRepository.SaveChangesAsync();
                await _orderLineRepository.DeleteRangePerOrderAsync(request.OrderId);
                transaction.Complete();
            }

            return Success(true, "Order canceled successfully.");
        }
    }
}
