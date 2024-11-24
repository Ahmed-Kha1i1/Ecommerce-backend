using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class VariationConfigration : IEntityTypeConfiguration<Variation>
    {
        public void Configure(EntityTypeBuilder<Variation> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Name)
                   .HasMaxLength(100);
        }
    }
}
