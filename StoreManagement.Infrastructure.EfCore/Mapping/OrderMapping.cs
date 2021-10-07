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

            builder.OwnsMany(o => o.OrderItems, modelBuilder =>
            {
                modelBuilder.HasKey(i => i.Id);
                modelBuilder.WithOwner(i => i.Order).HasForeignKey(f => f.OrderId);
                modelBuilder.HasOne(p => p.Product).WithMany(o => o.OrderItems).HasForeignKey(f => f.ProductId);
            });
        }
    }
}
