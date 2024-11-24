using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class DiscountConfigration : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(p => p.Name)
                   .HasMaxLength(50);

            builder.Property(p => p.Rate)
                   .HasColumnType("tinyint");

            builder.ToTable(b => b.HasCheckConstraint("CK_Rate_MaxValue", "[Rate] <= 100 AND [Rate] <= 100"));

            builder.HasOne(x => x.ProductItem)
                   .WithMany(x => x.Discounts)
                   .HasForeignKey(x => x.ProductItemId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
