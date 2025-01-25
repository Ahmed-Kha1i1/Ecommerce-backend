using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.OrderLines.Commands.AddOrderLine
{
    public class AddOrderLineCommand : IRequest<Response<int?>>
    {
        public int CustomerId { get; set; }
        public int ProductItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
