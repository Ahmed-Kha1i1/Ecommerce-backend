using Ecommerce.Doman.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Persistence.Data.Config
{
    internal class CatagoryConfigration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ParentCategory)
                   .WithMany(x => x.ChildCategories)
                   .HasForeignKey(x => x.ParentCategoryId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(b => b.PictureName)
                   .HasMaxLength(255);
        }
    }
}
