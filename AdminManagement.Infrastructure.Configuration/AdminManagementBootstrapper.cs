using AdminManagement.Application;
using AdminManagement.Application.Contract.BannerAgg;
using AdminManagement.Domain.BannerAgg;
using AdminManagement.Infrastructure.EfCore;
using AdminManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdminManagement.Infrastructure.Configuration
{
    public class AdminManagementBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region Config Context

            service.AddDbContext<AdminContext>(options => options.UseSqlServer(connectionString));

            #endregion

            service.AddTransient<IBannerRepository, BannerRepository>();
            service.AddTransient<IBannerApplication, BannerApplication>();
        }
    }
}