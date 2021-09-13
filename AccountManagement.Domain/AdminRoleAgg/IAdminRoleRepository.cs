using AccountManagement.Application.Contract.AdminRoleAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace AccountManagement.Domain.AdminRoleAgg
{
    public interface IAdminRoleRepository : IRepository<AdminRole>
    {
        Task<EditAdminRoleVM> GetDetailForEditBy(long id);
    }
}
