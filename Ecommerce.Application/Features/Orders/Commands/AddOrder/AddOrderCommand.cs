using Ecommerce.Application.Common.Response;
using Ecommerce.Doman.Common.Enums;
using MediatR;

namespace Ecommerce.Application.Features.Orders.Commands.AddOrder
{
    public class AddOrderCommand : IRequest<Response<int?>>
    {
        public enPaymentMethod PaymentMethod { get; set; } = enPaymentMethod.onDoor;
    }
}
