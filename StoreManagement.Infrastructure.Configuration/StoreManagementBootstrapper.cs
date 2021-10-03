using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadMarket.Query.Contracts.CategoryAgg;
using RadMarket.Query.Contracts.ProductAgg;
using RadMarket.Query.Queries;
using StoreManagement.Application;
using StoreManagement.Application.Contract.AdtPackageAgg;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Application.Contract.PackageAgg;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Domain.AdtPackageAgg;
using StoreManagement.Domain.CategoryAgg;
using StoreManagement.Domain.PackageAgg;
using StoreManagement.Domain.ProductAgg;
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

            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IProductApplication, ProductApplication>();

            service.AddTransient<IPackageRepository, PackageRepository>();
            service.AddTransient<IPackageApplication, PackageApplication>();

            service.AddTransient<IAdtPackageRepository, AdtPackageRepository>();
            service.AddTransient<IAdtPackageApplication, AdtPackageApplication>();

            #region Query

            service.AddTransient<IProductQuery, ProductQuery>();
            service.AddTransient<ICategoryQuery, CategoryQuery>();

            #endregion
        }
    }
}