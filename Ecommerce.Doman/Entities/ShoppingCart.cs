
using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class ShoppingCart : BaseEntity
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
