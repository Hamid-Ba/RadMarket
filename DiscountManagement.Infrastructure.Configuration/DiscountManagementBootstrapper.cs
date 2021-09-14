using DiscountManagement.Application;
using DiscountManagement.Application.Contract.DiscountAgg;
using DiscountManagement.Domain.DiscountAgg;
using DiscountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Infrastructure.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region Config Context

            service.AddDbContext<DiscountContext>(options => options.UseSqlServer(connectionString));

            #endregion

            service.AddTransient<IDiscountRepository, DiscountRepository>();
            service.AddTransient<IDiscountApplication, DisocuntApplication>();
        }
    }
}
