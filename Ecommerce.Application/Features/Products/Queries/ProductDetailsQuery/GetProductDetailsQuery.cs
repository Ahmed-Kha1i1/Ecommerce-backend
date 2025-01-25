using Ecommerce.Application.Common.Response;
using MediatR;

namespace Ecommerce.Application.Features.Products.Queries.ProductDetailsQuery
{
    public class GetProductDetailsQuery : IRequest<Response<GetProductDetailsQueryReponse>>
    {
        public int Id { get; set; }

        public GetProductDetailsQuery(int id)
        {
            Id = id;
        }
    }
}
