using Ecommerce.Application.Common.Response;
using Ecommerce.Application.Contracts.Persistence;
using MediatR;

namespace Ecommerce.Application.Features.Brands.Queries.BrandsSearchQuery
{
    public class GetBrandsSearchQueryHandler(IBrandRepository brandRepository) : ResponseHandler, IRequestHandler<GetBrandsSearchQuery, Response<IReadOnlyCollection<GetBrandsSearchQueryResponse>>>
    {
        public async Task<Response<IReadOnlyCollection<GetBrandsSearchQueryResponse>>> Handle(GetBrandsSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await brandRepository.GetProductsSearchBrands(request);

            return Success(result);
        }
    }
}
