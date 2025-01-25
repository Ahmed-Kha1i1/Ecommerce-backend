using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class RefreshTokenConfigration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {

            //builder.HasOne(x => x.Customer)
            //      .WithMany(x => x.RefreshTokens)
            //      .HasForeignKey(x => x.CustomerId)
            //      .IsRequired(true)
            //      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
