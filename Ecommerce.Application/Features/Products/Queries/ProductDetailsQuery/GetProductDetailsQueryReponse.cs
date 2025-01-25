using Ecommerce.Application.Features.Brands;
using Ecommerce.Application.Features.Categories;
using Ecommerce.Application.Features.ProductItems;

namespace Ecommerce.Application.Features.Products.Queries.ProductDetailsQuery
{
    public class GetProductDetailsQueryReponse
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Material { get; set; }
        public string Condition { get; set; }
        public string CountryName { get; set; }
        public decimal? Stars { get; set; }
        public int Reviews { get; set; }
        public CategoryOverviewDTO Category { get; set; }
        public BrandOverviewDTO Brand { get; set; }
        public ICollection<ProductItemDTO> ProductItems { get; set; } = new List<ProductItemDTO>();
    }
}
