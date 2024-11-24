using Ecommerce.Doman.Entities.Base;

namespace Ecommerce.Doman.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string PictureName { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<CategoryVariation> CategoryVariations { get; set; } = new List<CategoryVariation>();
        public ICollection<Category> ChildCategories { get; set; } = new List<Category>();
    }
}
