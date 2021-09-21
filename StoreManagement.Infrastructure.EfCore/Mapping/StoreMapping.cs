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

            builder.Property(p => p.UniqueCode).HasMaxLength(8);
            builder.Property(p => p.Name).HasMaxLength(85).IsRequired();
            builder.Property(p => p.AccountNumber).HasMaxLength(16).IsRequired();
            builder.Property(p => p.ShabaNumber).HasMaxLength(26).IsRequired();
            builder.Property(p => p.CardNumber).HasMaxLength(16).IsRequired();
            builder.Property(p => p.MobileNumber).HasMaxLength(11).IsRequired();
            builder.Property(p => p.PhoneNumber).IsRequired();
            builder.Property(p => p.City).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Province).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Address).HasMaxLength(500).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(1500);
            builder.Property(p => p.StoreGivenStatusReason).HasMaxLength(250);
        }
    }
}