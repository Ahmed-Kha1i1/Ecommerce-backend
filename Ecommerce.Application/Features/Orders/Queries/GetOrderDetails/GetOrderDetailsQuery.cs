using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQuery : IRequest<Response<GetOrderDetailsQueryResponse>>
    {
        public int OrderId { get; set; }

        public GetOrderDetailsQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
