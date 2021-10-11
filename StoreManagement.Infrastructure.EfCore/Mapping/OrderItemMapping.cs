using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.OrderAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class OrderItemMapping : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(k => k.Id);

            builder.HasOne(o => o.Order).WithMany(i => i.OrderItems).HasForeignKey(f => f.OrderId);
        }
    }
}
