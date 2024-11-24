using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Ecommerce.Persistence.Data.Config
{
    internal class CustomerConfigration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {

            builder.Property(c => c.Username)
                   .HasMaxLength(50);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(c => c.Email)
                .HasMaxLength(100);

            builder.Property(c => c.PasswordHash)
                .HasMaxLength(255);
        }
    }
}
