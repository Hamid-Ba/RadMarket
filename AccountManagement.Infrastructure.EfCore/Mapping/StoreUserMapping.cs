using AccountManagement.Domain.StoreUserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class StoreUserMapping : IEntityTypeConfiguration<StoreUser>
    {
        public void Configure(EntityTypeBuilder<StoreUser> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FirstName).HasMaxLength(85).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Address).HasMaxLength(300).IsRequired();
            builder.Property(p => p.City).HasMaxLength(85).IsRequired();
            builder.Property(p => p.StoreCode).HasMaxLength(8).IsRequired();
            builder.Property(p => p.Province).HasMaxLength(25).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(500).IsRequired();

            builder.HasOne(r => r.StoreRole)
                .WithMany(u => u.Users)
                .HasForeignKey(f => f.StoreRoleId);
        }
    }
}
