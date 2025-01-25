using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string ImageName { get; set; }
        public string? Material { get; set; }
        public enCondition Condition { get; set; } = enCondition.New;
        public int? BrandId { get; set; }
        public Brand? Brand { get; set; }
        public int? CountryOfOriginId { get; set; }
        public Country? CountryOfOrigin { get; set; }
        public decimal? Stars { get; set; }
        public int Reviews { get; set; }
        public ICollection<ProductItem> ProductItems { get; set; } = new List<ProductItem>();
    }
}
