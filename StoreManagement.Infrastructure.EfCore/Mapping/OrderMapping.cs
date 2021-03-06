using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.OrderAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasMany(o => o.OrderItems).WithOne(o => o.Order).HasForeignKey(f => f.OrderId);
        }
    }
}
