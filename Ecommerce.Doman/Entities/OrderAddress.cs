using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class OrderAddress : BaseAddress
    {
        public Order Order { get; set; }
    }
}
