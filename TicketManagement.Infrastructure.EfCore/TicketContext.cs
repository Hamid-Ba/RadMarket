using Microsoft.EntityFrameworkCore;
using System.Linq;
using TicketManagement.Domain.StoreTicketAgg;
using TicketManagement.Infrastructure.EfCore.Mapping;

namespace TicketManagement.Infrastructure.EfCore
{
    public class TicketContext : DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var assembly = typeof(StoreTicketMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        public DbSet<StoreTicket> StoreTickets { get; set; }

    }
}
