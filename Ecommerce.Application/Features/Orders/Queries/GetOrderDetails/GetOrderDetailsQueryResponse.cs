using Ecommerce.Application.Features.OrderLines;
using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryResponse
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public enPaymentMethod PaymentMethod { get; set; }
        public int CustomerId { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public enOrderStatus? Status { get; set; }
        public decimal DeliveryFees { get; set; }
        public decimal PaymentFees { get; set; }
        public OrderAddressDTO OrderAddress { get; set; }
        public List<OrderLineDTO> Lines { get; set; }
    }
}
