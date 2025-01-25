using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class ProductItemConfigration : IEntityTypeConfiguration<ProductItem>
    {
        public void Configure(EntityTypeBuilder<ProductItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Product)
                  .WithMany(x => x.ProductItems)
                  .HasForeignKey(x => x.ProductId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Price)
                   .HasPrecision(10, 2);

            builder.HasIndex(p => p.Price)
                   .IsClustered(false)
                   .IsUnique(false);

            builder.Property(p => p.SKU)
                   .HasMaxLength(50);

            builder.Property(p => p.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
