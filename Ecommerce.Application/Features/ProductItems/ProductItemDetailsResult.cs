namespace Ecommerce.Application.Features.ProductItems
{
    public class ProductItemDetailsResult
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public byte DiscountRate { get; set; }
        public int StockQuantity { get; set; }
        public string? ImageName { get; set; }
        public bool IsDefault { get; set; }
        public string? VariationName { get; set; }
        public string? VariationValue { get; set; }
    }
}
