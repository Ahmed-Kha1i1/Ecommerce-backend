using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class CategoryVariationConfigration : IEntityTypeConfiguration<CategoryVariation>
    {
        public void Configure(EntityTypeBuilder<CategoryVariation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.CategoryVariations)
                   .HasForeignKey(x => x.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Variation)
                   .WithMany(x => x.CategoryVariations)
                   .HasForeignKey(x => x.VariationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
