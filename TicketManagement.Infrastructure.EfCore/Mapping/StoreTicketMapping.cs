using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain.StoreTicketAgg;

namespace TicketManagement.Infrastructure.EfCore.Mapping
{
    public class StoreTicketMapping : IEntityTypeConfiguration<StoreTicket>
    {
        public void Configure(EntityTypeBuilder<StoreTicket> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(85).IsRequired();

            builder.OwnsMany(m => m.Messages, cloneBuilder =>
            {
                cloneBuilder.HasKey(k => k.Id);
                cloneBuilder.Property(p => p.Message).IsRequired();
                cloneBuilder.WithOwner(o => o.StoreTicket).HasForeignKey(f => f.StoreTicketId);
            });
        }
    }
}
