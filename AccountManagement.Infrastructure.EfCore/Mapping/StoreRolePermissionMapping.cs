using AccountManagement.Domain.StoreRolePermissionAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class StoreRolePermissionMapping : IEntityTypeConfiguration<StoreRolePermission>
    {
        public void Configure(EntityTypeBuilder<StoreRolePermission> builder)
        {
            builder.HasKey(k => new { k.StoreRoleId, k.StorePermissionId });

            builder.HasOne(r => r.StoreRole)
                .WithMany(p => p.Permissions)
                .HasForeignKey(f => f.StoreRoleId);

            builder.HasOne(p => p.StorePermission)
                .WithMany(r => r.Roles)
                .HasForeignKey(f => f.StorePermissionId);
        }
    }
}
