using AdminManagement.Domain.BannerAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdminManagement.Infrastructure.EfCore.Mapping
{
    public class BannerMapping : IEntityTypeConfiguration<Banner>
    {
        public void Configure(EntityTypeBuilder<Banner> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Picture).IsRequired();
            builder.Property(p => p.PictuerAlt).HasMaxLength(85);
            builder.Property(p => p.PictureTitle).HasMaxLength(125).IsRequired();
            builder.Property(p => p.ColSize).HasMaxLength(250).IsRequired();
        }
    }
}
