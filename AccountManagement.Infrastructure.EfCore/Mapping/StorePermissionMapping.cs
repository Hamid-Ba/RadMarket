using AccountManagement.Domain.StorePermissionAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class StorePermissionMapping : IEntityTypeConfiguration<StorePermission>
    {
        public void Configure(EntityTypeBuilder<StorePermission> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(100).IsRequired();
        }
    }
}
