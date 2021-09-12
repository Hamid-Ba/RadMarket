using AccountManagement.Application.Contract.AdminUserAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AdminUserAgg
{
    public interface IAdminUserRepository : IRepository<AdminUser>
    {
        Task<EditAdminUserVM> GetDetailForEditBy(long id);
    }
}
