using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Variation : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<CategoryVariation> CategoryVariations { get; set; } = new List<CategoryVariation>();
        public ICollection<VariationOption> VariationOptions { get; set; } = new List<VariationOption>();
    }
}
