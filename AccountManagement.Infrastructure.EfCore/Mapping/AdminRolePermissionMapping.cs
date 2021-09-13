using AccountManagement.Domain.AdminRolePermissionAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    class AdminRolePermissionMapping : IEntityTypeConfiguration<AdminRolePermission>
    {
        public void Configure(EntityTypeBuilder<AdminRolePermission> builder)
        {
            builder.HasKey(k => new { k.AdminRoleId, k.AdminPermissionId });

            builder.HasOne(r => r.AdminRole)
                .WithMany(p => p.Permissions)
                .HasForeignKey(f => f.AdminRoleId);

            builder.HasOne(p => p.AdminPermission)
                .WithMany(r => r.Roles)
                .HasForeignKey(f => f.AdminPermissionId);
        }
    }
}
