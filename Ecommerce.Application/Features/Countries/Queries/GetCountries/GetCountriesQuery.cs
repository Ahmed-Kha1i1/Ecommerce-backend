using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQuery : IRequest<Response<List<CountryDTO>>>
    {
    }
}
