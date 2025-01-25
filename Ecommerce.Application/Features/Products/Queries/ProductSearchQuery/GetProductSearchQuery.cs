using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Response;
using Ecommerce.Doman.Common.Enums;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductSearchQuery
{
    public class GetProductSearchQuery : PaginatedQueryBase, IRequest<Response<PaginatedProductSearchResult<ProductSummaryDTO>>>
    {
        public string? SearchQuery { get; set; }
        public int? BrandId { get; set; }
        public int? CategoryId { get; set; }
        public enCondition? Condition { get; set; }
        public decimal? Stars { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
        public string OrderBy { get; set; } = "Stars";
        public string OrderDirection { get; set; } = "Desc";
    }
}
