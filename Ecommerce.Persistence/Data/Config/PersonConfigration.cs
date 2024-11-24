using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    internal class PersonConfigration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.FirstName)
                   .HasMaxLength(50);

            builder.Property(p => p.LastName)
                   .HasMaxLength(50);

            builder.Property(b => b.ImageName)
                   .HasMaxLength(255);

            builder.Property(p => p.Gender)
                   .IsRequired()
                   .HasColumnType("bit")
                   .HasDefaultValue(enGender.Male)
                   .HasComment("0-Male,1-Female")
                   .HasConversion(
                       g => g == enGender.Female,
                       g => g ? enGender.Female : enGender.Male
                   );


            builder.Property(p => p.CreatedDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.UseTptMappingStrategy();
        }
    }
}
