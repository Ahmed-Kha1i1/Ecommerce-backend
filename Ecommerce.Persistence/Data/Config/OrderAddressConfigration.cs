using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class OrderAddressConfigration : IEntityTypeConfiguration<OrderAddress>
    {
        public void Configure(EntityTypeBuilder<OrderAddress> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(x => x.PostalCode)
                .HasMaxLength(10);

            builder.Property(x => x.State)
                .HasMaxLength(100);

            builder.Property(x => x.City)
            .HasMaxLength(100);


        }
    }
}
