using Ecommerce.Doman.Common.Enums;
using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    public class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(p => p.Name)
            .HasMaxLength(250);

            builder.Property(b => b.ImageName)
                   .HasMaxLength(255);

            builder.HasOne(p => p.Category)
                   .WithMany(Sub => Sub.Products)
                   .HasForeignKey(p => p.CategoryId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.Material)
                   .IsRequired(false)
                   .HasMaxLength(100);

            builder.Property(p => p.Condition)
                   .IsRequired()
                   .HasColumnType("bit")
                   .HasDefaultValue(enCondition.New)
                   .HasComment("0-New,1-Used")
                   .HasConversion(
                       g => g == enCondition.Used,
                       g => g ? enCondition.Used : enCondition.New
                   );

            builder.Property(p => p.Stars)
                   .HasPrecision(2, 1);

            builder.HasIndex(p => p.Stars)
                   .IsClustered(false)
                   .IsUnique(false);

            builder.HasOne(p => p.Brand)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.BrandId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(p => p.CountryOfOrigin)
                   .WithMany(b => b.Products)
                   .HasForeignKey(p => p.CountryOfOriginId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
