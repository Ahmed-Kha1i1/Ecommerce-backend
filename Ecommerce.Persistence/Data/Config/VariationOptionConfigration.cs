using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class VariationOptionConfigration : IEntityTypeConfiguration<VariationOption>
    {
        public void Configure(EntityTypeBuilder<VariationOption> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Variation)
                   .WithMany(x => x.VariationOptions)
                   .HasForeignKey(x => x.VariationId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
