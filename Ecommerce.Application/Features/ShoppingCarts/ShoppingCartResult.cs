namespace Ecommerce.Application.Features.ShoppingCarts
{
    public class ShoppingCartResult
    {
        public int ShoppingCartItemId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductItemId { get; set; }
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
