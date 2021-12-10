using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketManagement.Domain.TicketAgg;

namespace TicketManagement.Infrastructure.EfCore.Mapping
{
    public class TicketMapping : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title).HasMaxLength(150).IsRequired();

            builder.OwnsMany(m => m.Messages, cloneBuilder =>
            {
                cloneBuilder.HasKey(k => k.Id);
                cloneBuilder.Property(p => p.Text).IsRequired();
                cloneBuilder.WithOwner(o => o.Ticket).HasForeignKey(f => f.TicketId);
            });
        }
    }
}