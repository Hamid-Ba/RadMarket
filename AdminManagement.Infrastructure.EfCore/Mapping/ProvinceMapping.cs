using AdminManagement.Domain.ProvinceAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminManagement.Infrastructure.EfCore.Mapping
{
    public class ProvinceMapping : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).HasMaxLength(25).IsRequired();
        }
    }
}
