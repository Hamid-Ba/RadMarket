using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadMarket.Query.Contracts.StoreTicketAgg;
using RadMarket.Query.Contracts.Tickets;
using RadMarket.Query.Queries;
using TicketManagement.Application;
using TicketManagement.Application.Contract.StoreTicketAgg;
using TicketManagement.Application.Contract.TicketAgg;
using TicketManagement.Domain.StoreTicketAgg;
using TicketManagement.Domain.TicketAgg;
using TicketManagement.Infrastructure.EfCore;
using TicketManagement.Infrastructure.EfCore.Repository;

namespace TicketManagement.Infrastructure.Configuration
{
    public class TicketManagementBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region Config Context

            service.AddDbContext<TicketContext>(options => options.UseSqlServer(connectionString));

            #endregion

            service.AddTransient<IStoreTicketRepository, StoreTicketRepository>();
            service.AddTransient<IStoreTicketApplication, StoreTicketApplication>();

            service.AddTransient<ITicketRepository, TicketRepository>();
            service.AddTransient<ITicketApplication, TicketApplication>();

            #region Query

            service.AddTransient<ITicketQuery, TicketQuery>();
            service.AddTransient<IStoreTicketQuery, StoreTicketQuery>();

            #endregion
        }
    }
}
