using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.PackageAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class PackageMapping : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(20).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500).IsRequired();
            builder.Property(p => p.ImageName).HasMaxLength(125);
        }
    }
}
