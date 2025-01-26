using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Application.Features.OrderLines.Queries.GetPaginatedOrderLines
{
    public class GetPaginatedOrderLinesQueryHandler : ResponseHandler, IRequestHandler<GetPaginatedOrderLinesQuery, Response<PaginatedResult<OrderLineDetailsDTO>>>
    {
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetPaginatedOrderLinesQueryHandler(IOrderLineRepository orderLineRepository, IHttpContextAccessor httpContextAccessor)
        {
            _orderLineRepository = orderLineRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<PaginatedResult<OrderLineDetailsDTO>>> Handle(GetPaginatedOrderLinesQuery request, CancellationToken cancellationToken)
        {
            int userId = _httpContextAccessor.GetUserId();


            var response = await _orderLineRepository.GetPaginatedOrderLines(userId, request.PageNumber, request.PageSize, request.IsCanceled);
            if (response == null)
            {
                return NotFound<PaginatedResult<OrderLineDetailsDTO>>("No order lines found for the specified criteria.");
            }

            return Success(response);
        }
    }
}
