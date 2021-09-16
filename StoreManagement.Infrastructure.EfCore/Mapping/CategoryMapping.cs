using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.CategoryAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.KeyWords).HasMaxLength(100);
            //builder.Property(c => c.PictureAlt).HasMaxLength(150);
            //builder.Property(c => c.PictureTitle).HasMaxLength(250);
            builder.Property(c => c.MetaDescription).HasMaxLength(500);
            builder.Property(c => c.Slug).HasMaxLength(150).IsRequired();
        }
    }
}
