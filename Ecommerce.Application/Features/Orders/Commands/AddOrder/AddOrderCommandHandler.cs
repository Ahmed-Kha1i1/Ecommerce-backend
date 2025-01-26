using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using Ecommerce.Doman.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Transactions; // Add this using directive  

namespace Ecommerce.Application.Features.Orders.Commands.AddOrder
{
    public class AddOrderCommandHandler : ResponseHandler, IRequestHandler<AddOrderCommand, Response<int?>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderAddressRepository _orderAddressRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly OrderSettings _orderSettings;
        private readonly IMapper _mapper;

        public AddOrderCommandHandler(IShoppingCartRepository shoppingCartRepository, IAddressRepository addressRepository, IOrderRepository orderRepository, IOrderAddressRepository orderAddressRepository, IHttpContextAccessor httpContextAccessor, IOptionsSnapshot<OrderSettings> orderSettings, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _orderAddressRepository = orderAddressRepository;
            _httpContextAccessor = httpContextAccessor;
            _orderSettings = orderSettings.Value;
            _mapper = mapper;
        }

        public async Task<Response<int?>> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();

            var defaultAddress = await _addressRepository.GetDefaultAddressByCustomerIdAsync(userId);
            if (defaultAddress is null)
            {
                return BadRequest<int?>("No default address found for the customer");
            }

            // Use a transaction scope to ensure atomicity  
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var orderAddress = _mapper.Map<OrderAddress>(defaultAddress);
                await _orderAddressRepository.AddAsync(orderAddress);
                await _orderAddressRepository.SaveChangesAsync();

                // Retrieve cart items for the user
                var cart = await _shoppingCartRepository.GetCartItemsAsync(userId);
                if (cart == null || cart.Items.Count == 0)
                {
                    return BadRequest<int?>("No items in the cart.");
                }

                // Initialize total price  
                decimal totalPrice = 0;


                // Create a new order with order lines  
                var order = new Order
                {
                    CustomerId = userId,
                    OrderDate = DateTime.UtcNow,
                    Status = enOrderStatus.Placed,
                    DeliveryDate = DateTime.UtcNow.AddDays(3),
                    OrderAddressId = orderAddress.Id,
                    PaymentMethod = request.PaymentMethod,
                    DeliveryFees = _orderSettings.DeliveryFees,
                    PaymentFees = request.PaymentMethod == Doman.Common.Enums.enPaymentMethod.onDoor ? _orderSettings.DoorPaymentFees : 0,
                    OrderLines = cart.Items.Where(item => item.Quantity <= item.ProductItemDetails.StockQuantity)
                    .Select(item =>
                    {
                        // Calculate price after discount  
                        var itemPrice = item.ProductItemDetails.Price * (1 - item.ProductItemDetails.DiscountRate / 100);

                        // Add item price to total  
                        totalPrice += itemPrice * item.Quantity;

                        return new OrderLine
                        {
                            ProductItemId = item.ProductItemDetails.Id,
                            Quantity = (int)item.Quantity,
                            Price = itemPrice
                        };
                    }).ToList()
                };

                order.TotalPrice = totalPrice;

                if (order.OrderLines.Count > 0)
                {
                    // Save the order  
                    await _orderRepository.AddAsync(order);
                    await _orderRepository.SaveChangesAsync();

                    // Clear the cart  
                    await _shoppingCartRepository.ClearCartAsync(userId);

                    // Complete the transaction  
                    transaction.Complete();

                    // Return the order ID  
                    return Success<int?>(order.Id);
                }
                else
                {
                    return BadRequest<int?>("No items have proper quantity in the cart.");
                }
            }
        }
    }
}