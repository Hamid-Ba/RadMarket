using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.BrandAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class BrandMapping : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(s => s.Store)
                .WithMany(b => b.Brands)
                .HasForeignKey(f => f.StoreId);

            builder.HasMany(p => p.Products)
                .WithOne(b => b.Brand)
                .HasForeignKey(f => f.BrandId);
        }
    }
}
