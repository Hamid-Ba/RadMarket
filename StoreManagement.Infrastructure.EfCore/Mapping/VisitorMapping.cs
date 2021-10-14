using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.VisitorAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class VisitorMapping : IEntityTypeConfiguration<Visitor>
    {
        public void Configure(EntityTypeBuilder<Visitor> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.UniqueCode).HasMaxLength(7);
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.FullName).HasMaxLength(125).IsRequired();
        }
    }
}