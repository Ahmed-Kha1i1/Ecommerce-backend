using Ecommerce.Application.Common.Models;

namespace Ecommerce.Application.Features.Products.Queries.ProductSearchQuery
{
    public class PaginatedProductSearchResult<T> : PaginatedResult<T> where T : class
    {
        public PaginatedProductSearchResult(IList<T> data, int currentPage, int pageSize, int count, decimal minPrice, decimal maxPrice) : base(data, currentPage, pageSize, count)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
    }
}
