using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class ProductImageConfigration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ProductItem)
                   .WithMany(x => x.Images)
                   .HasForeignKey(x => x.ProductItemId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(b => b.ImageName)
                   .HasMaxLength(255);
        }
    }
}
