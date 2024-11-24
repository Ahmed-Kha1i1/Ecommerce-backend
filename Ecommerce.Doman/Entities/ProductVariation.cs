using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class ProductVariation : BaseEntity
    {
        public int ProductItemId { get; set; }
        public ProductItem ProductItem { get; set; }
        public int VariationOptionId { get; set; }
        public VariationOption VariationOption { get; set; }
    }
}
