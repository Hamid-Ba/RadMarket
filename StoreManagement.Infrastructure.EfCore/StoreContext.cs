﻿using Microsoft.EntityFrameworkCore;
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

        }

        public DbSet<Store> Stores { get; set; }
    }
}