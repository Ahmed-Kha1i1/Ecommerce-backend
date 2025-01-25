using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public DateTime? DeliveryDate { get; set; }
        public enOrderStatus Status { get; set; }
        public int OrderAddressId { get; set; }
        public OrderAddress OrderAddress { get; set; }
        public enPaymentMethod PaymentMethod { get; set; }
        public decimal DeliveryFees { get; set; }
        public decimal PaymentFees { get; set; }
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
