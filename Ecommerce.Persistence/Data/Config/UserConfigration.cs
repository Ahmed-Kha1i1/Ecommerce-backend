using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class UserConfigration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName)
                    .HasMaxLength(50);

            builder.Property(p => p.LastName)
                    .HasMaxLength(50);

            builder.Property(p => p.Gender)
                    .HasColumnType("bit")
                    .HasComment("0-Male,1-Female")
                    .HasConversion(
                        g => g == null ? (bool?)null : g == enGender.Female, // Convert enum to nullable bool
                        g => g == null ? null : g.Value ? enGender.Female : enGender.Male // Convert nullable bool to enum
                    );



            builder.Property(p => p.CreatedDate)
                    .HasDefaultValueSql("GETUTCDATE()");

            builder.HasQueryFilter(u => !u.IsDeleted);
            builder.UseTptMappingStrategy();
        }
    }
}
