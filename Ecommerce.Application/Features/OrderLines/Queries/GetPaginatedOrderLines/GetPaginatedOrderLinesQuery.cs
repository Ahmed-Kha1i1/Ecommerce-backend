using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OrderLines.Queries.GetPaginatedOrderLines
{
    public class GetPaginatedOrderLinesQuery : PaginatedQueryBase, IRequest<Response<PaginatedResult<OrderLineDetailsDTO>>>
    {
        protected override short MaxPageSize { get; set; } = 7;
        public bool IsCanceled { get; set; } = false;
    }
}
