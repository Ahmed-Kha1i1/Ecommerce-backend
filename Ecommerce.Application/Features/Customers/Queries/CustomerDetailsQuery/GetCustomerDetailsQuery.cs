using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Customers.Queries.CustomerDetailsQuery
{
    public class GetCustomerDetailsQuery : IRequest<Response<GetCustomerDetailsQueryResponse>>
    {

    }
}
