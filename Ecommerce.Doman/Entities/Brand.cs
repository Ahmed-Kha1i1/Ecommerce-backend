using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string LogoName { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
