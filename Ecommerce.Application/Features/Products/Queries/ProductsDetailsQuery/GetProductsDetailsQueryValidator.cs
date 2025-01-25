using FluentValidation;

namespace Ecommerce.Application.Features.Products.Queries.ProductsDetailsQuery
{
    public class GetProductsDetailsQueryValidator : AbstractValidator<GetProductsDetailsQuery>
    {
        public GetProductsDetailsQueryValidator()
        {
            RuleFor(x => x.Ids)
                .Must(ids => ids.All(id => id > 0)).WithMessage("All ids must be greater than zero.");
        }
    }
}
