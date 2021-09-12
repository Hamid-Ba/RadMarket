using AccountManagement.Domain.UserAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AccountManagement.Infrastructure.EfCore.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.FirstName).HasMaxLength(85).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(100).IsRequired();
            builder.Property(p => p.MarketerCode).HasMaxLength(10);
            builder.Property(p => p.ActivationCode).HasMaxLength(7);
            builder.Property(p => p.Address).HasMaxLength(300).IsRequired();
            builder.Property(p => p.City).HasMaxLength(85).IsRequired();
            builder.Property(p => p.Province).HasMaxLength(25).IsRequired();
            builder.Property(p => p.Mobile).HasMaxLength(11).IsRequired();
            builder.Property(p => p.Password).HasMaxLength(500).IsRequired();
        }
    }
}
