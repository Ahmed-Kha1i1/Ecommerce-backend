using FluentValidation;

namespace Ecommerce.Application.Features.ProductItems.GetProductItemsDetails
{
    public class GetProductItemsDetailsQueryValidator : AbstractValidator<GetProductItemsDetailsQuery>
    {
        public GetProductItemsDetailsQueryValidator()
        {
            RuleFor(x => x.ProductItemIds)
                .Must(ids => ids.All(id => id > 0)).WithMessage("All ProductItemIds must be greater than zero.");
        }
    }
}
