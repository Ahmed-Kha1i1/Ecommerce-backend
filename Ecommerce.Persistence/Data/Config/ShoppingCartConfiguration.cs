using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Customer)
                  .WithOne(x => x.ShoppingCart)
                  .HasForeignKey<ShoppingCart>(x => x.CustomerId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
