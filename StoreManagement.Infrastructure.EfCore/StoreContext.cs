﻿using Microsoft.EntityFrameworkCore;
using StoreManagement.Domain.AdtPackageAgg;
using StoreManagement.Domain.CategoryAgg;
using StoreManagement.Domain.OrderAgg;
using StoreManagement.Domain.PackageAgg;
using StoreManagement.Domain.PackageOrderAgg;
using StoreManagement.Domain.ProductAgg;
using StoreManagement.Domain.StoreAgg;
using StoreManagement.Infrastructure.EfCore.Mapping;
using System.Linq;

namespace StoreManagement.Infrastructure.EfCore
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var assembly = typeof(StoreMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.Entity<Store>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Package>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Category>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<AdtPackage>().HasQueryFilter(u => !u.IsDelete);
            modelBuilder.Entity<Product>().HasQueryFilter(u => !u.IsDelete || u.Category == null);
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AdtPackage> AdtPackages { get; set; }
        public DbSet<PackageOrder> PackageOrders { get; set; }

    }
}