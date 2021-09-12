using AccountManagement.Application;
using AccountManagement.Application.Contract.AdminUserAgg;
using AccountManagement.Application.Contract.UserAgg;
using AccountManagement.Domain.AdminUserAgg;
using AccountManagement.Domain.UserAgg;
using AccountManagement.Infrastructure.EfCore;
using AccountManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region ConfigContext
            
            service.AddDbContext<AccountContext>(o => o.UseSqlServer(connectionString));

            #endregion

            service.AddTransient<IUserRepository, UserRepository>();
            service.AddTransient<IUserApplication, UserApplication>();

            service.AddTransient<IAdminUserRepository, AdminUserRepository>();
            service.AddTransient<IAdminUserApplication, AdminUserApplication>();
        }
    }
}
