using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;

namespace Ecommerce.Application.Features.OrderLines.Commands.AddOrderLine
{
    public class AddOrderLineCommandHandler : ResponseHandler, IRequestHandler<AddOrderLineCommand, Response<int?>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;

        public AddOrderLineCommandHandler(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository)
        {
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
        }

        public async Task<Response<int?>> Handle(AddOrderLineCommand request, CancellationToken cancellationToken)
        {
            var existingOrder = await _orderRepository
                .FindOpenedOrderAsync(request.CustomerId);

            if (existingOrder == null)
            {
                existingOrder = new Order
                {
                    CustomerId = request.CustomerId,
                    TotalPrice = 0m,
                    OrderDate = DateTime.UtcNow,
                    Status = enOrderStatus.Placed,
                };

                await _orderRepository.AddAsync(existingOrder);
                await _orderRepository.SaveChangesAsync();
            }

            var orderLine = new OrderLine
            {
                ProductItemId = request.ProductItemId,
                OrderId = existingOrder.Id,
                Quantity = request.Quantity,
                Price = request.Price,
                CreatedDate = DateTime.UtcNow
            };

            await _orderLineRepository.AddAsync(orderLine);

            existingOrder.TotalPrice += request.Price * request.Quantity;
            await _orderRepository.SaveChangesAsync();

            return Success<int?>(orderLine.Id);
        }
    }
}
