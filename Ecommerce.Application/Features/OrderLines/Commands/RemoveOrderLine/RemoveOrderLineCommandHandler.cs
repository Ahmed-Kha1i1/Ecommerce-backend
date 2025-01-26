using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.OrderLines.Commands.RemoveOrderLine
{
    public class RemoveOrderLineCommandHandler : ResponseHandler, IRequestHandler<RemoveOrderLineCommand, Response<bool>>
    {
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RemoveOrderLineCommandHandler(IOrderLineRepository orderLineRepository, IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderLineRepository = orderLineRepository;
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(RemoveOrderLineCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();


            var orderLine = await _orderLineRepository.GetByIdIncludeItemAsync(request.OrderLineId);
            if (orderLine == null)
            {
                return NotFound<bool>($"OrderLine with ID {request.OrderLineId} not found.");
            }

            var order = await _orderRepository.GetByIdAsync(orderLine.OrderId);
            if (order == null || order.CustomerId != userId)
            {
                return NotFound<bool>("Order not found or does not belong to the user.");
            }

            var modifiableStatuses = new[] { enOrderStatus.Placed, enOrderStatus.Confirmed, enOrderStatus.Processing };
            if (!modifiableStatuses.Contains(order.Status))
            {
                return BadRequest<bool>("Order cannot be modified at its current status.");
            }

            _orderLineRepository.Delete(orderLine);
            orderLine.ProductItem.StockQuantity += orderLine.Quantity;
            await _orderRepository.SaveChangesAsync();

            await _orderRepository.UpdateOrderPriceAsync(order.Id);
            return Success<bool>(true, "Order line removed successfully.");
        }
    }
}
