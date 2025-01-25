using FluentValidation;

namespace Ecommerce.Application.Features.Products.Queries.ProductSearchQuery
{
    public class GetProductSearchQueryValidator : AbstractValidator<GetProductSearchQuery>
    {
        public GetProductSearchQueryValidator()
        {
            RuleFor(q => q.Stars)
                .InclusiveBetween(1, 5);

            RuleFor(q => q.Condition)
                .IsInEnum();

            RuleFor(q => q.SearchQuery)
                .MaximumLength(250);

            RuleFor(query => query.MinPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MinPrice must be greater than or equal to 0.")
                .LessThanOrEqualTo(99999999.99M)
                .WithMessage("MinPrice must be less than or equal to 99999999.99.");

            RuleFor(query => query.MaxPrice)
                .GreaterThanOrEqualTo(0)
                .WithMessage("MaxPrice must be greater than or equal to 0.")
                .LessThanOrEqualTo(99999999.99M)
                .WithMessage("MaxPrice must be less than or equal to 99999999.99.");

            RuleFor(query => query.MinPrice)
                .LessThanOrEqualTo(query => query.MaxPrice)
                .When(query => query.MinPrice.HasValue && query.MaxPrice.HasValue)
                .WithMessage("MinPrice must be less than or equal to MaxPrice.");

        }
    }
}
