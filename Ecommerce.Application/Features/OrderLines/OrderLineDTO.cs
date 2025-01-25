using Ecommerce.Application.Features.ProductItems;

namespace Ecommerce.Application.Features.OrderLines
{
    public class OrderLineDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageName { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsCanceled { get; set; }
        public ICollection<VariationDTO> Variations { get; set; }
    }
}
