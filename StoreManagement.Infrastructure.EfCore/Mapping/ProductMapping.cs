using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.ProductAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
            builder.Property(p => p.Code).HasMaxLength(15);
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Keywords).HasMaxLength(200).IsRequired();
            builder.Property(p => p.MetaDescription).HasMaxLength(1000).IsRequired();
            builder.Property(p => p.Picture).HasMaxLength(200).IsRequired();
            builder.Property(p => p.PictureAlt).HasMaxLength(150);
            builder.Property(p => p.PictureTitle).HasMaxLength(250);
            builder.Property(p => p.Slug).HasMaxLength(400).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.ProductAcceptanceState).HasMaxLength(250).IsRequired();

            builder.HasOne(p => p.Category).
                WithMany(c => c.Products).
                HasForeignKey(f => f.CategoryId);

            builder.HasOne(p => p.Store).
                WithMany(s => s.Products).
                HasForeignKey(f => f.StoreId);
        }
    }
}
