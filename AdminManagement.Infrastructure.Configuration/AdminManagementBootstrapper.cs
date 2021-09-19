using AdminManagement.Application;
using AdminManagement.Application.Contract.BannerAgg;
using AdminManagement.Application.Contract.ProvinceAgg;
using AdminManagement.Domain.BannerAgg;
using AdminManagement.Domain.ProvinceAgg;
using AdminManagement.Infrastructure.EfCore;
using AdminManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadMarket.Query.Contracts.ProvinceAgg;
using RadMarket.Query.Queries;

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

            service.AddTransient<IProvinceRepository, ProvinceRepository>();
            service.AddTransient<IProvinceApplication, ProvinceApplication>();

            service.AddTransient<IProvinceQuery, ProvinceQuery>();
        }
    }
}