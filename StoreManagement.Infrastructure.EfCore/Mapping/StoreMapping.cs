using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreManagement.Domain.StoreAgg;

namespace StoreManagement.Infrastructure.EfCore.Mapping
{
    public class StoreMapping : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.UniqueCode).HasMaxLength(8).IsRequired();
            builder.Property(p => p.Name).HasMaxLength(85).IsRequired();
            builder.Property(p => p.MobileNumber).HasMaxLength(11).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.Address).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(1500);
            builder.Property(p => p.StoreGivenStatusReason).HasMaxLength(250);
        }
    }
}