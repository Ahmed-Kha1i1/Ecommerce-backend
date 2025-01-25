using Ecommerce.Application.Common.Response;
using Ecommerce.Doman.Common.Enums;
using MediatR;

namespace Ecommerce.Application.Features.Brands.Queries.BrandsSearchQuery
{
    public class GetBrandsSearchQuery : IRequest<Response<IReadOnlyCollection<GetBrandsSearchQueryResponse>>>
    {
        public string? SearchQuery { get; set; }
        public int? CategoryId { get; set; }
        public enCondition? Condition { get; set; }
        public decimal? Stars { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }
    }
}
