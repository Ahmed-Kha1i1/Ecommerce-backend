using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class ProductItem : BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();
        public ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
    }
}
