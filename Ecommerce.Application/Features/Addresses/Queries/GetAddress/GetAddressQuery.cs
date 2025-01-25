using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Queries.GetAddress
{
    public class GetAddressQuery : IRequest<Response<AddressSummaryDTO>>
    {
        public int AddressId { get; set; }

        public GetAddressQuery(int addressId)
        {
            AddressId = addressId;
        }
    }
}
