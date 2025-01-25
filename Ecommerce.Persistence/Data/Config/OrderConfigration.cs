using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.TotalPrice)
                   .HasPrecision(18, 2);

            builder.Property(p => p.Status)
                   .IsRequired()
                   .HasColumnType("TINYINT")
                   .HasDefaultValue(enOrderStatus.Placed)
                   .HasComment("1-Placed,2-Confirmed,3-Processing,4-Shipped,5-InTransit,6-OutForDelivery,7-Delivered,8-Canceled,9-AttemptedDelivery,10-Lost")
                   .HasConversion(
                       v => (byte)v,
                       v => (enOrderStatus)v
                   );

            builder.HasOne(x => x.OrderAddress)
                   .WithOne(x => x.Order)
                   .HasForeignKey<Order>(x => x.OrderAddressId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
