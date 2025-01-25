using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class ShoppingCartItemConfiguration : IEntityTypeConfiguration<ShoppingCartItem>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ShoppingCart)
                   .WithMany(x => x.Items)
                   .HasForeignKey(x => x.ShoppingCartId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.ProductItem)
                   .WithMany()
                   .HasForeignKey(x => x.ProductItemId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
