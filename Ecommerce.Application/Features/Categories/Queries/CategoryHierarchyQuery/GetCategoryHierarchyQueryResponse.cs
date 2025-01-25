namespace Ecommerce.Application.Features.Categories.Queries.CategoryHierarchyQuery
{
    public class GetCategoryHierarchyQueryResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<CategoryOverviewDTO> ParentHierarchy { get; set; }
        public IReadOnlyCollection<CategoryOverviewDTO> Children { get; set; }
    }

}
