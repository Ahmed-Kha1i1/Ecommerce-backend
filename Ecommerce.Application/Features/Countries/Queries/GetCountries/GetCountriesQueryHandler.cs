using AutoMapper;
using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Countries.Queries.GetCountries
{
    public class GetCountriesQueryHandler : ResponseHandler, IRequestHandler<GetCountriesQuery, Response<List<CountryDTO>>>
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;
        public GetCountriesQueryHandler(ICountryRepository countryRepository, IMapper mapper)
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }
        public async Task<Response<List<CountryDTO>>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
        {

            var countries = await _countryRepository.GetAllAsNoTracking();
            return Success(_mapper.Map<List<CountryDTO>>(countries));
        }
    }
}
