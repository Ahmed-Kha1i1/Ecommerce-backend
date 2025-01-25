using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.SetDefaultAddress
{
    public class SetDefaultAddressCommand : IRequest<Response<bool>>
    {
        public int AddressId { get; set; }

        public SetDefaultAddressCommand(int addressId)
        {
            AddressId = addressId;
        }
    }
}
