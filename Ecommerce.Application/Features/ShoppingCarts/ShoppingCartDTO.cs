using Ecommerce.Application.Features.ShoppingCartItems;

namespace Ecommerce.Application.Features.ShoppingCarts
{
    public class ShoppingCartDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public ICollection<ShoppingCartItemDTO> Items { get; set; }
    }
}
