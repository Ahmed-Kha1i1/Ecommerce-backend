namespace Ecommerce.Application.Features.Categories
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string PictureName { get; set; }
        public int? ParentCategoryId { get; set; }
    }
}
