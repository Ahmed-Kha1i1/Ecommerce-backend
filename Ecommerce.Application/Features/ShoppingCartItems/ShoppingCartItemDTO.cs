using Ecommerce.Application.Features.ProductItems;

namespace Ecommerce.Application.Features.ShoppingCartItems
{

    public class ShoppingCartItemDTO
    {
        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public ProductItemDetailsDTO ProductItemDetails { get; set; }
    }
}