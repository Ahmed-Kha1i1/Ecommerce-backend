using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OrderLines.Commands.RemoveOrderLine
{
    public class RemoveOrderLineCommand : IRequest<Response<bool>>
    {
        public int OrderLineId { get; set; }

        public RemoveOrderLineCommand(int orderLineId)
        {
            OrderLineId = orderLineId;
        }
    }
}
