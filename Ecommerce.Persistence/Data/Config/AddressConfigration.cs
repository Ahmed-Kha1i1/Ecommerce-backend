using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class AddressConfigration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
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



            builder.HasOne(x => x.Country)
                   .WithMany(x => x.Addresses)
                   .HasForeignKey(x => x.CountryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Customer)
                   .WithMany(x => x.Addresses)
                   .HasForeignKey(x => x.CustomerId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
