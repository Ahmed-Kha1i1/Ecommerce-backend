using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    internal class BrandConfigration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(b => b.Name)
                   .HasMaxLength(100);

            builder.Property(b => b.LogoName)
                   .HasMaxLength(255);
        }
    }
}
