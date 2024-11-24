using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class ProductVariationConfigration : IEntityTypeConfiguration<ProductVariation>
    {
        public void Configure(EntityTypeBuilder<ProductVariation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ProductItem)
                   .WithMany(x => x.ProductVariations)
                   .HasForeignKey(x => x.ProductItemId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.VariationOption)
                   .WithMany(x => x.ProductVariations)
                   .HasForeignKey(x => x.VariationOptionId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
