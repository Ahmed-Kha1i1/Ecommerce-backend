using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Commands.DeleteAddress
{
    public class DeleteAddressCommand : IRequest<Response<bool>>
    {
        public int AddressId { get; set; }

        public DeleteAddressCommand(int addressId)
        {
            AddressId = addressId;
        }
    }
}
