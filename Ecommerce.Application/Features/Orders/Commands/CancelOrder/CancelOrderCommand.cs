using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.CancelOrder
{
    public class CancelOrderCommand : IRequest<Response<bool>>
    {
        public int OrderId { get; set; }

        public CancelOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
