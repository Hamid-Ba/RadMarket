using AccountManagement.Domain.AdminUserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class AdminUserMapping : IEntityTypeConfiguration<AdminUser>
    {
        public void Configure(EntityTypeBuilder<AdminUser> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FirstName).HasMaxLength(85).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(500).IsRequired();
        }
    }
}
