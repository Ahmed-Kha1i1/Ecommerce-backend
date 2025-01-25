namespace Ecommerce.Application.Features.ProductItems
{
    public class ProductItemDetailsDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public byte DiscountRate { get; set; }
        public int StockQuantity { get; set; }
        public string ImageName { get; set; }
        public ICollection<VariationDTO> Variations { get; set; }
    }

}
