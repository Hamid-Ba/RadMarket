using AccountManagement.Domain.StoreRoleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class StoreRoleMapping : IEntityTypeConfiguration<StoreRole>
    {
        public void Configure(EntityTypeBuilder<StoreRole> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).HasMaxLength(25).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(225);
        }
    }
}
