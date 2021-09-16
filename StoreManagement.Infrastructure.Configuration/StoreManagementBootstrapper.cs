using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StoreManagement.Application;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Domain.CategoryAgg;
using StoreManagement.Domain.StoreAgg;
using StoreManagement.Infrastructure.EfCore;
using StoreManagement.Infrastructure.EfCore.Repository;

namespace StoreManagement.Infrastructure.Configuration
{
    public class StoreManagementBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region Config Context

            service.AddDbContext<StoreContext>(options => options.UseSqlServer(connectionString));

            #endregion

            service.AddTransient<IStoreRepository, StoreRepository>();
            service.AddTransient<IStoreApplication, StoreApplication>();

            service.AddTransient<ICategoryRepository, CategoryRepository>();
            service.AddTransient<ICategoryApplication, CategoryApplication>();
        }
    }
}