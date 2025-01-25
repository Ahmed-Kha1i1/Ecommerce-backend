using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class ProductImage : BaseEntity
    {
        public string ImageName { get; set; }
        public int ProductItemId { get; set; }
        public bool IsDefault { get; set; } = false;
        public ProductItem ProductItem { get; set; }
    }
}
