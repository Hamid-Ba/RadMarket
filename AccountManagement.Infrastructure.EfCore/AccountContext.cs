using AccountManagement.Domain.AdminPermissionAgg;
using AccountManagement.Domain.AdminRoleAgg;
using AccountManagement.Domain.AdminRolePermissionAgg;
using AccountManagement.Domain.AdminUserAgg;
using AccountManagement.Domain.StorePermissionAgg;
using AccountManagement.Domain.StoreRoleAgg;
using AccountManagement.Domain.StoreRolePermissionAgg;
using AccountManagement.Domain.StoreUserAgg;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AccountManagement.Infrastructure.EfCore
{
    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var assembly = typeof(UserMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<StoreUser>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<AdminUser>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<AdminRole>().HasQueryFilter(u => !u.IsDelete);
        }

        public DbSet<User> User { get; set; }
        public DbSet<StoreUser> StoreUser { get; set; }
        public DbSet<StoreRole> StoreRole { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<AdminRole> AdminRoles { get; set; }
        public DbSet<StorePermission> StorePermissions { get; set; }
        public DbSet<AdminPermission> AdminPermissions { get; set; }
        public DbSet<StoreRolePermission> StoreRolePermissions { get; set; }
        public DbSet<AdminRolePermission> AdminRolePermissions { get; set; }

    }
}
