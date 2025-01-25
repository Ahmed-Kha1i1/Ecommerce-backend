using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Addresses.Queries.GetAddresses
{
    public class GetAddressesQuery : IRequest<Response<List<AddressSummaryDTO>>>
    {

    }
}
