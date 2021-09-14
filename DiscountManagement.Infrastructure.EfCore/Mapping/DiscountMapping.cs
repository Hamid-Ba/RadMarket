using DiscountManagement.Domain.DiscountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiscountManagement.Infrastructure.EfCore.Mapping
{
    public class DiscountMapping : IEntityTypeConfiguration<Discount>
    {
        public void Configure(EntityTypeBuilder<Discount> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Reason).HasMaxLength(500);
        }
    }
}
