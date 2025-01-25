using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class OrderLineConfigration : IEntityTypeConfiguration<OrderLine>
    {
        public void Configure(EntityTypeBuilder<OrderLine> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ProductItem)
                    .WithMany(x => x.OrderLines)
                    .HasForeignKey(x => x.ProductItemId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Order)
                    .WithMany(x => x.OrderLines)
                    .HasForeignKey(x => x.OrderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

            builder.Property(p => p.Price)
                   .HasPrecision(10, 2);

            builder.Property(p => p.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasQueryFilter(u => !u.IsDeleted);
            builder.ToTable(tb => tb.UseSqlOutputClause(false));
        }
    }
}
