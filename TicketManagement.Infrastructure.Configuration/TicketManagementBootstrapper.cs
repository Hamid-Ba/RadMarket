using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TicketManagement.Application;
using TicketManagement.Application.Contract.StoreTicketAgg;
using TicketManagement.Domain.StoreTicketAgg;
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
        }
    }
}
