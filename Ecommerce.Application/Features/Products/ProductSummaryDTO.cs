namespace Ecommerce.Application.Features.Products
{
    public class ProductSummaryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Stars { get; set; }
        public int Reviews { get; set; }
        public string ImageURL { get; set; }
        public int ProductItemId { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public byte? Rate { get; set; }
    }
}
