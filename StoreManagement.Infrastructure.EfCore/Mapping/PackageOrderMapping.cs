using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.PackageOrderAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class PackageOrderMapping : IEntityTypeConfiguration<PackageOrder>
    {
        public void Configure(EntityTypeBuilder<PackageOrder> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.MobileNumber).HasMaxLength(11).IsRequired();

            builder.HasOne(w => w.Store)
                .WithMany(o => o.PackageOrders)
                .HasForeignKey(f => f.StoreId);
        }
    }
}
