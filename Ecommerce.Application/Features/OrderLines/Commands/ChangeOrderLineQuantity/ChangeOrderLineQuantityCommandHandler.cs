using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.OrderLines.Commands.ChangeOrderLineQuantity
{
    public class ChangeOrderLineQuantityCommandHandler : ResponseHandler, IRequestHandler<ChangeOrderLineQuantityCommand, Response<bool>>
    {
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IProductItemRepository _productItemRepository;
        private readonly IOrderRepository _orderRepository;

        public ChangeOrderLineQuantityCommandHandler(
            IOrderLineRepository orderLineRepository,
            IProductItemRepository productItemRepository,
            IOrderRepository orderRepository)
        {
            _orderLineRepository = orderLineRepository;
            _productItemRepository = productItemRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Response<bool>> Handle(ChangeOrderLineQuantityCommand request, CancellationToken cancellationToken)
        {
            // Retrieve the OrderLine
            var orderLine = await _orderLineRepository.GetByIdAsync(request.OrderLineId);
            if (orderLine == null)
            {
                return NotFound<bool>($"OrderLine with ID {request.OrderLineId} not found.");
            }

            // Retrieve the ProductItem
            var productItem = await _productItemRepository.GetByIdAsync(orderLine.ProductItemId);
            if (productItem == null)
            {
                return NotFound<bool>($"ProductItem with ID {orderLine.ProductItemId} not found.");
            }

            var totalOrderedQuantity = orderLine.Quantity;
            var availableStock = productItem.StockQuantity - totalOrderedQuantity;

            if (request.NewQuantity > availableStock)
            {
                return BadRequest<bool>($"Requested quantity exceeds available stock. Available stock: {availableStock}");
            }

            int quantityDifference = request.NewQuantity - orderLine.Quantity;
            orderLine.Quantity = request.NewQuantity;


            var order = await _orderRepository.GetByIdAsync(orderLine.OrderId);
            if (order != null)
            {
                order.TotalPrice += orderLine.Price * quantityDifference;
            }
            await _orderRepository.SaveChangesAsync();
            return Success<bool>(true, "Order line quantity updated successfully.");
        }
    }
}
