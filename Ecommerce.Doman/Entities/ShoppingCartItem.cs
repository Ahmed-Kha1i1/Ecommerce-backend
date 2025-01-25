using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class ShoppingCartItem : BaseEntity
    {
        public int ShoppingCartId { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
