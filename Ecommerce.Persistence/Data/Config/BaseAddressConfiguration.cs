using Ecommerce.Doman.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public abstract class BaseAddressConfiguration : IEntityTypeConfiguration<BaseAddress>
    {
        public virtual void Configure(EntityTypeBuilder<BaseAddress> builder)
        {
            //builder.HasKey(x => x.Id);

            //builder.Property(p => p.CreatedDate)
            //    .HasDefaultValueSql("GETUTCDATE()");

            //builder.Property(x => x.PostalCode)
            //    .HasMaxLength(10);

            //builder.Property(x => x.State)
            //    .HasMaxLength(100);

            //builder.Property(x => x.City)
            //    .HasMaxLength(100);

            builder.UseTpcMappingStrategy();
        }
    }
}
