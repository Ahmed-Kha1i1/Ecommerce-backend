using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Queries.GetDefaultAddress
{
    public class GetDefaultAddressQuery : IRequest<Response<AddressSummaryDTO>>
    {
    }
}
