using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class CategoryVariation : BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int VariationId { get; set; }
        public Variation Variation { get; set; }
    }
}
