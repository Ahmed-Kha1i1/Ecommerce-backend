using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
