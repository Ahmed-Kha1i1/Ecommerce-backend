using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OrderLines.Commands.ChangeOrderLineQuantity
{
    public class ChangeOrderLineQuantityCommand : IRequest<Response<bool>>
    {
        public int OrderLineId { get; set; }
        public int NewQuantity { get; set; }

        public ChangeOrderLineQuantityCommand(int orderLineId, int newQuantity)
        {
            OrderLineId = orderLineId;
            NewQuantity = newQuantity;
        }
    }
}
