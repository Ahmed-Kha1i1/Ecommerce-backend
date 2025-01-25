using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class OrderLine : BaseEntity, ISoftDeleteable
    {
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public DateTime? DateDeleted { get; set; }
    }
}
