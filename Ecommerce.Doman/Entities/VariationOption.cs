using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class VariationOption : BaseEntity
    {
        public string Value { get; set; }
        public int VariationId { get; set; }
        public Variation Variation { get; set; }

        public ICollection<ProductVariation> ProductVariations { get; set; } = new List<ProductVariation>();
    }
}
