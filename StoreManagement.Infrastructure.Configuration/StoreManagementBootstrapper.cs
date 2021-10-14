using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadMarket.Query.Contracts.CategoryAgg;
using RadMarket.Query.Contracts.OrderAgg;
using RadMarket.Query.Contracts.PackageOrderAgg;
using RadMarket.Query.Contracts.ProductAgg;
using RadMarket.Query.Queries;
using StoreManagement.Application;
using StoreManagement.Application.Contract.AdtPackageAgg;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Application.Contract.PackageAgg;
using StoreManagement.Application.Contract.PackageOrderAgg;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Application.Contract.StoreAgg;
using StoreManagement.Application.Contract.VisitorAgg;
using StoreManagement.Domain.AdtPackageAgg;
using StoreManagement.Domain.CategoryAgg;
using StoreManagement.Domain.OrderAgg;
using StoreManagement.Domain.PackageAgg;
using StoreManagement.Domain.PackageOrderAgg;
using StoreManagement.Domain.ProductAgg;
using StoreManagement.Domain.StoreAgg;
using StoreManagement.Domain.VisitorAgg;
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

            service.AddTransient<IPackageOrderRepository, PackageOrderRepository>();
            service.AddTransient<IPackageOrderApplication, PackageOrderApplication>();

            service.AddTransient<IOrderRepository, OrderRepository>();
            service.AddTransient<IOrderApplication, OrderApplication>();
            service.AddTransient<IOrderItemRepository, OrderItemRepository>();

            service.AddTransient<IVisitorRepository, VisitorRepository>();
            service.AddTransient<IVisitorApplication, VisitorApplication>();
            #region Query

            service.AddTransient<IOrderQuery, OrderQuery>();
            service.AddTransient<IProductQuery, ProductQuery>();
            service.AddTransient<ICategoryQuery, CategoryQuery>();
            service.AddTransient<ICalculatePackageOrderCart, CalculatePackageOrderCart>();

            #endregion
        }
    }
}