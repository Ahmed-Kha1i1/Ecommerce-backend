namespace Ecommerce.Application.Features.ProductItems
{
    public class ProductItemDTO
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string SKU { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte DiscountRate { get; set; }
        public ICollection<ImageDTO> Images { get; set; } = new List<ImageDTO>();
        public IDictionary<string, string> Variations { get; set; } = new Dictionary<string, string>();
    }
}
