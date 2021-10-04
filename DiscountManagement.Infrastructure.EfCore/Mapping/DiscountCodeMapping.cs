using DiscountManagement.Domain.DiscountCodeAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mapping
{
    public class DiscountCodeMapping : IEntityTypeConfiguration<DiscountCode>
    {
        public void Configure(EntityTypeBuilder<DiscountCode> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(p => p.Code).HasMaxLength(7).IsRequired();
            builder.Property(c => c.Reason).HasMaxLength(500);
        }
    }
}
